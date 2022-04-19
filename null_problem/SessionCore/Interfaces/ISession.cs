using SampleCore.Interfaces;

namespace SessionCore.Interfaces;

public interface ISession
{
    uint RunID { get; }
    ISampleRate SampleRate { get; }
    IStartSetup StartSetup { get; }
    IStopSetup StopSetup { get; }
    DateTime StartTime { get; }
    DateTime LatestSampleTime { get; }
    DateTime NextSampleTime { get; }
    DateTime StopTime { get; }
    bool IsWaiting { get; }
    bool IsRunning { get; }
    bool IsStopped { get; }
    IReadOnlyDictionary<byte, ISensor> Sensors { get; }
    void SamlpesToProcess(DateTime dateTime);
}