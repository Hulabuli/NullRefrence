using SessionCore.Interfaces;

namespace SessionCore.Modes;

internal abstract class AutoMode : EventStopMode, IStartSetup
{
    private readonly double _startTemperature;
    private readonly double _turnPoint;
    private readonly double _stopTemperature;

    public AutoMode(double startTemperature, double turnPoint, double stopTemperature)
    {
        _startTemperature = startTemperature;
        _turnPoint = turnPoint;
        _stopTemperature = stopTemperature;
    }

    public abstract bool IsStartConditionMet(DateTime dateTime);
}
