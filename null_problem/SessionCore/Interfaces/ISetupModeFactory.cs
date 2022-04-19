namespace SessionCore.Interfaces;

public interface ISetupModeFactory
{
    IStartSetup CreateTimeStartMode(DateTime startTime);
    IStopSetup CreateTimeStopMode(DateTime stopTime);
}