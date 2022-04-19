using SampleCore.Interfaces;
using SessionCore.Handlers;

namespace SessionCore.Interfaces;

internal interface ISessionFactory
{
    Session CreateSession(TimeSpan sampleRate, IStartSetup startSetup, IStopSetup stopSetup, IReadOnlyDictionary<byte, ISensor> sensors);
}