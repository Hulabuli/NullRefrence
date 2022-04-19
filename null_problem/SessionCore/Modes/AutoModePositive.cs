namespace SessionCore.Modes;

internal class AutoModePositive : AutoMode
{
    public AutoModePositive(double startTemperature, double turnPoint, double stopTemperature)
        : base(startTemperature, turnPoint, stopTemperature) { }

    public override bool IsStartConditionMet(DateTime dateTime)
    {
        return false;
    }

    public override bool IsStopConditionMet(DateTime dateTime)
    {
        if (base.IsStopConditionMet(dateTime))
        {
            return true;
        }
        
        return false;
    }
}
