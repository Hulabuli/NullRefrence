using Common.Interfaces.StandardUnits;
using Common.StandardUnits;
using SampleCore.EventArguments;
using SampleCore.Interfaces;
using SampleCore.Interfaces.Generators;
using SampleCore.Sensors;
using Utility.Extensions.DateTimes;

namespace SampleCore.Simulator;

internal class SampleSimulator : ISampleGenerator
{
    private const int _channelCount = 52;
    private const double _initailValue = -200;
    private const double _interval = 0.5;
    private const double _channelSpread = 2;
    private const double _maxValue = (_initailValue * -1) - (_channelCount * _channelSpread);

    private double CurrentValue { get; set; } = _initailValue;
    private double CurrentDirection { get; set; } = 1;
    private DateTime NewTimeStamp { get; set; } = DateTime.MinValue;
    private DateTime PrevTimeStamp { get; set; } = DateTime.MinValue;
    private readonly Dictionary<byte, Sensor> _internalSensors = new(_channelCount);
    private readonly Dictionary<byte, ISensor> _internalISensors = new(_channelCount);
    public IReadOnlyDictionary<byte, ISensor> Sensors => _internalISensors;

    public event EventHandler<ILatestSampleTimeStampArgs>? LatestSampleTimeStamp;
    public event EventHandler<ILiveSensorArgs>? LiveSensors;

    private readonly IStandardUnitTable _standardUnitTable;

    private void OnLatestSampleTimeStamp(ILatestSampleTimeStampArgs latestSampleTimeStampArgs)
    {
        LatestSampleTimeStamp?.Invoke(this, latestSampleTimeStampArgs);
    }
    private void OnLiveSensors(ILiveSensorArgs liveSensorArgs)
    {
        LiveSensors?.Invoke(this, liveSensorArgs);
    }

    public SampleSimulator(IStandardUnitTable standardUnitTable)
    {
        _standardUnitTable = standardUnitTable ?? throw new ArgumentNullException(nameof(standardUnitTable));

        GenerateSensors();
        Task.Run(async () => await SimulatorClock());
    }

    private async Task SimulatorClock()
    {
        while (true)
        {
            await Task.Delay(300);
            NewTimeStamp = DateTime.UtcNow.Truncate(TimeSpan.FromSeconds(1));
            if (NewTimeStamp > PrevTimeStamp)
            {
                GenerateSensorSamples();
                PrevTimeStamp = NewTimeStamp;
            }
        }
    }

    private void GenerateSensors()
    {
        for (byte i = 1; i <= _channelCount; i++)
        {
            var newSensor = GenerateSensor(i);
            _internalSensors[i] = newSensor;
            _internalISensors[i] = newSensor;
        }
    }

    private Sensor GenerateSensor(byte channelNumber)
    {
        return new Sensor()
        {
            SerialNumber = (uint)(1000 * channelNumber),
            ChannelNumber = channelNumber,
            Unit = _standardUnitTable[StandardUnitType.Temperature]
        };
    }

    private void GenerateSensorSamples()
    {
        foreach (var sensor in _internalSensors)
        {
            var sample = GenerateSensorSample(sensor.Value.ChannelNumber);
            sensor.Value.InternalDataStore.AddTestValue(sample, NewTimeStamp);
        }

        OnLatestSampleTimeStamp(new LatestSampleTimeStampArgs(NewTimeStamp));
        OnLiveSensors(new LiveSensorArgs(NewTimeStamp, Sensors));

        CurrentValue += _interval * CurrentDirection;
        if (CurrentValue > _maxValue)
        {
            CurrentValue = _maxValue;
            CurrentDirection *= -1;
        }

        if (CurrentValue < _initailValue)
        {
            CurrentValue = _initailValue;
            CurrentDirection *= -1;
        }
    }

    private double GenerateSensorSample(int channelNumber)
    {
        return CalculateSampleValue(channelNumber);
    }

    private double CalculateSampleValue(int channelNumber)
    {
        return CurrentValue + (_channelSpread * channelNumber);
    }
}