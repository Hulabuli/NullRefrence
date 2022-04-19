using SampleCore.EventArguments;

namespace SampleCore.Interfaces.Generators;

public interface ISampleGenerator
{
    event EventHandler<ILatestSampleTimeStampArgs>? LatestSampleTimeStamp;
    event EventHandler<ILiveSensorArgs>? LiveSensors;

    IReadOnlyDictionary<byte, ISensor> Sensors { get; }
}