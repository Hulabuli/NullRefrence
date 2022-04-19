using SampleCore.Interfaces;
using SessionCore.Interfaces;

namespace SessionCore.Handlers;

internal class SessionFactory : ISessionFactory
{
    private uint _runID = 0;

    public Session CreateSession(TimeSpan sampleRate, IStartSetup startSetup, IStopSetup stopSetup, IReadOnlyDictionary<byte, ISensor> sensors)
    {
        return new Session(++_runID, new SampleRate(sampleRate), startSetup, stopSetup, sensors);
    }
}