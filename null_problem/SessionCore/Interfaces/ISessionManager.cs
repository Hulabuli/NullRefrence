using SampleCore.Interfaces;
using SessionCore.EventArguments;

namespace SessionCore.Interfaces;

public interface ISessionManager
{
    byte MaxSessionCount { get; }
    IReadOnlyDictionary<uint, ISession> Sessions { get; }

    event EventHandler<IStartedArgs>? SessionStarted;
    event EventHandler<ISampledArgs>? SessionSampled;
    event EventHandler<IStoppedArgs>? SessionStopped;

    ISession CreateSession(TimeSpan sampleRate, IStartSetup startSetup, IStopSetup stopSetup, IReadOnlyDictionary<byte, ISensor> sensors);
}