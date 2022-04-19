namespace SessionCore.Interfaces;

public interface IStopSetup
{
    void Stop();
    bool IsStopConditionMet(DateTime dateTime);
}
