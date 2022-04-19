using SampleCore.Interfaces;

namespace SampleCore.EventArguments;

public interface ILiveSensorArgs : ILatestSampleTimeStampArgs
{
    public IReadOnlyDictionary<byte, ISensor> Sensors { get; }
}

internal class LiveSensorArgs : LatestSampleTimeStampArgs, ILiveSensorArgs
{
    public IReadOnlyDictionary<byte, ISensor> Sensors { get; }

    public LiveSensorArgs(DateTime latestSampleTimeStamp, IReadOnlyDictionary<byte, ISensor> sensors)
        : base(latestSampleTimeStamp)
    {
        Sensors = sensors;
    }
}