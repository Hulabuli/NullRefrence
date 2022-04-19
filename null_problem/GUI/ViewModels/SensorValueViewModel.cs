using GUI.Interfaces;
using GUI.Models;
using GUI.ViewModels.UserControls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using SampleCore.EventArguments;
using SampleCore.Interfaces.Generators;
using System.Collections.ObjectModel;

namespace GUI.ViewModels;

public class SensorValueViewModel : ObservableRecipient, IViewModel
{
    private readonly IUserControlFactory _userControlFactory;
    private readonly ISampleGenerator _generator;
    private static DispatcherQueue _dispatcherQueue;

    private Dictionary<byte, ChannelBlockModel> _dataDict = new();

    private ObservableCollection<UserControl> _array1 = new();
    public ObservableCollection<UserControl> Array1 { get => _array1; set => SetProperty(ref _array1, value); }

    private ObservableCollection<UserControl> _array2 = new();
    public ObservableCollection<UserControl> Array2 { get => _array2; set => SetProperty(ref _array2, value); }

    private ObservableCollection<UserControl> _array3 = new();
    public ObservableCollection<UserControl> Array3 { get => _array3; set => SetProperty(ref _array3, value); }

    private ObservableCollection<UserControl> _arrayX = new();
    public ObservableCollection<UserControl> ArrayX { get => _arrayX; set => SetProperty(ref _arrayX, value); }

    public SensorValueViewModel(IUserControlFactory userControlFactory, ISampleGenerator generator)
    {
        _userControlFactory = userControlFactory;
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        _dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        GenerateDataDict();
    }

    public void Initialize(object model)
    {
        _generator.LiveSensors += NewData;
    }

    private void NewData(object sender, ILiveSensorArgs e)
    {
        foreach (KeyValuePair<byte, ChannelBlockModel> entry in _dataDict)
        {
            try
            {
                _dispatcherQueue.TryEnqueue(() => {

                    entry.Value.Data = e.Sensors[entry.Key].DataStore[e.LatestSampleTimeStamp].Data;
                    entry.Value.Unit = e.Sensors[entry.Key].Unit;
                });
            }
            catch (Exception)
            {
                //TODO: Log a thing
            }
        }
    }

    private void GenerateDataDict()
    {
        for (byte i = 1; i <= 40; i++)
        {
            ChannelBlockModel channelBlockModel = new()
            {
                Id = i
            };

            var newChannelBlock = _userControlFactory.CreateUserControl<ChannelBlockViewModel>(channelBlockModel);

            _dataDict.Add(i, channelBlockModel);

            if (i <= 12)
            {
                _array1.Add(newChannelBlock);
            }
            else if (i <= 24)
            {
                _array2.Add(newChannelBlock);
            }
            else if (i <= 36)
            {
                _array3.Add(newChannelBlock);
            }
            else
            {
                _arrayX.Add(newChannelBlock);
            }
        }
    }
}

