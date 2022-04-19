using SessionCore.Interfaces;

namespace SessionCore.Modes;

internal class SetupModeFactory : ISetupModeFactory
{
    public IStartSetup CreateTimeStartMode(DateTime startTime)
    {
        return new TimeStartMode(startTime);
    }

    public IStopSetup CreateTimeStopMode(DateTime stopTime)
    {
        return new TimeStopMode(stopTime);
    }
}