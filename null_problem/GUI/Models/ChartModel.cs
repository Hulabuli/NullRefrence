using Common.DataPoints;
using Common.StandardUnits;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Media;
using SampleCore.EventArguments;
using SampleCore.Interfaces;
using SampleCore.Interfaces.Generators;
using SessionCore.EventArguments;
using SessionCore.Interfaces;
using Syncfusion.UI.Xaml.Charts;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GUI.Models;

public class ChartModel : ObservableObject
{
    ISampleGenerator _generator;
    ISessionManager _sessionManager;
    private uint _currentActiveSession = 0;
    private static DispatcherQueue _dispatcherQueue;
    

    private CartesianSeriesCollection _seriesCollection = new();
    public CartesianSeriesCollection SeriesCollection { get => _seriesCollection; set => SetProperty(ref _seriesCollection, value); }

    private DateTimeAxis _primaryAxis = new() { Header = "TimeStamp", PlotOffsetEnd = 15 };
    public DateTimeAxis PrimaryAxis { get => _primaryAxis; set => SetProperty(ref _primaryAxis, value); }

    private Queue<TimemarkerModel> _timermarkerModels = new();
    private Dictionary<byte, SeriesModel> _seriesModels = new();
    private Dictionary<uint, Dictionary<byte, SeriesModel>> _sessions = new();

    Dictionary<StandardUnitType, NumericalAxis> _axisCollection = new()
    {
        { StandardUnitType.Temperature, new NumericalAxis() { Header = "°C", PlotOffset = 10 } },
        { StandardUnitType.Pressure, new NumericalAxis() { Header = "Bar", PlotOffset = 10 } }
    };
    private NumericalAxis _axisTimeMarkers = new()
    {
        Visibility = Microsoft.UI.Xaml.Visibility.Collapsed,
        ShowGridLines = false,
    };

    public event EventHandler<byte> SeriesAdded;
    public event EventHandler<byte> SeriesRemoved;

    public ChartModel(ISampleGenerator generator, ISessionManager sessionManager)
    {
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        _sessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
        _dispatcherQueue = DispatcherQueue.GetForCurrentThread();

        _sessions.Add(0, _seriesModels);

        _generator.LiveSensors += NewData;
        _sessionManager.SessionStarted += NewSession;
        _sessionManager.SessionSampled += NewSessionData;
        _sessionManager.SessionStopped += SessionEnded;
    }

    private void SessionEnded(object sender, IStoppedArgs e)
    {
        //throw new NotImplementedException();
    }

    private void NewSessionData(object sender, ISampledArgs e)
    {
        
        uint runID = e.Session.RunID;
        if (_sessions.ContainsKey(runID))
        {
            _dispatcherQueue.TryEnqueue(() =>
            {
                foreach (ISensor sensor in e.Session.Sensors.Values)
                {
                    if (!_sessions[e.Session.RunID].ContainsKey(sensor.ChannelNumber))
                    {
                        NewSeries(sensor, e.Session.RunID);
                    }
                    else
                    {
                        // To do: Get fisrst sampel included in data collection
                        _sessions[runID][sensor.ChannelNumber].Data.Add(sensor.DataStore[e.LatestSampleTimeStamp]);
                    }
                }
            });
        }
        
    }

    private void NewSession(object sender, IStartedArgs e)
    {
        _sessions.Add(e.Session.RunID, new Dictionary<byte, SeriesModel>());
    }

    private void NewData(object sender, ILiveSensorArgs e)
    {
        foreach (ISensor sensor in e.Sensors.Values)
        {
            _dispatcherQueue.TryEnqueue(() =>
            {
                if (!_seriesModels.ContainsKey(sensor.ChannelNumber))
                {
                    NewSeries(sensor, 0);
                }

                _seriesModels[sensor.ChannelNumber].Data.Add(sensor.DataStore[e.LatestSampleTimeStamp]);

                if (_seriesModels[sensor.ChannelNumber].Data.Count > 1000)
                {
                    var time = _seriesModels[sensor.ChannelNumber].Data[0].Timestamp;
                    if (_timermarkerModels.Count != 0 && _timermarkerModels.First().Data[0].Timestamp < time)
                    {
                        _seriesCollection.Remove(_seriesCollection["Tm: " + _timermarkerModels.First().Id]);
                        _timermarkerModels.Dequeue();
                    }
                    _seriesModels[sensor.ChannelNumber].Data.RemoveAt(0);
                }
            });
        }
    }

    #region Series
    private void NewSeries(ISensor sensor, uint session)
    {
        _sessions[session].Add(sensor.ChannelNumber, new SeriesModel()
        {
            SerialNumber = sensor.SerialNumber,
            Channel = sensor.ChannelNumber,
            Data = new(),
            DataType = sensor.Unit.Type,
        });
        OnSeriesAdded(sensor.ChannelNumber);

        _seriesCollection.Add(BuildSeries(_sessions[session][sensor.ChannelNumber]));        
    }

    private CartesianSeries BuildSeries(SeriesModel series)
    {
        var newSeries = new FastLineBitmapSeries()
        {
            Name = series.Channel.ToString(),
            Label = series.Channel.ToString("Ch: 00"),
            YAxis = _axisCollection[series.DataType],
            StrokeThickness = 1,
            ItemsSource = series.Data,
            YBindingPath = "Data",
            XBindingPath = "Timestamp"
        };
        return newSeries;
        //SeriesCollection.Add(newSeries);
    }
    #endregion

    #region TimeMarkers
    public void NewTimemarker(byte timemarkerType, DateTime timestamp)
    {
        var data = new ObservableCollection<DataPoint>()
        {
            new DataPoint(0, timestamp),
            new DataPoint(1, timestamp)
        };

        var marker = new TimemarkerModel()
        {
            Id = timemarkerType,
            Number = Convert.ToUInt32(_timermarkerModels.Count),
            Data = data,
        };

        _timermarkerModels.Enqueue(marker);
        BuildTimermarker(marker);
    }

    private void BuildTimermarker(TimemarkerModel series)
    {
        DoubleCollection dashLine = new DoubleCollection();
        dashLine.Add(5);
        dashLine.Add(3);
        LineSeries newSeries = new()
        {
            Name = "Tm: " + series.Id,
            Label = series.Number.ToString("00"),
            YAxis = _axisTimeMarkers,
            StrokeThickness = 2.5,
            StrokeDashArray = dashLine,
            ItemsSource = series.Data,
            YBindingPath = "Data",
            XBindingPath = "Timestamp"
        };
        SeriesCollection.Add(newSeries);
    }
    #endregion

    public void ChannelSelect(byte channel)
    {
        foreach (FastLineBitmapSeries series in _seriesCollection)
        {
            if (series.StrokeThickness != 1)
            {
                //UnSelect(Series)
                series.StrokeThickness = 1;
            }
            else if (series.Name == channel.ToString())
            {
                //Select(series)
                series.StrokeThickness = 5;
            }
        }
    }

    public void UnloadChart()
    {
        var collection = SeriesCollection.ToList<CartesianSeries>();
        SeriesCollection.Clear();

        foreach (var item in collection)
        {
            if (item is FastLineBitmapSeries)
            {
                ((FastLineBitmapSeries)item).ItemsSource = null;
            }
            if (item is LineSeries)
            {
                ((LineSeries)item).ItemsSource = null;
            }
        }
        SeriesCollection.Clear();
    }

    public void ReloadChart(uint session = 0)
    {
        var collection = new CartesianSeriesCollection();

        if (SeriesCollection.Count > 0)
        {
            UnloadChart();
        }
        
        if (_sessions.ContainsKey(session))
        {
            foreach (SeriesModel series in _sessions[session].Values)
            {
                collection.Add(BuildSeries(series));
            }
        }
        else
        {
            Debug.WriteLine(session.ToString("Session to reload (00) not found"));
        }

        SeriesCollection = collection;
    }

    #region Event Invokes
    private void OnSeriesAdded(byte channel)
    {
        SeriesAdded?.Invoke(this, channel);
    }

    private void OnSeriesRemoved(byte channel)
    {
        SeriesRemoved?.Invoke(this, channel);
    }
    #endregion
}