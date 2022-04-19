using Utility.Extensions.DateTimes;

namespace SessionCore.Modes;

internal class TimeStopMode : EventStopMode
{
    private readonly DateTime _stopTime;

    public TimeStopMode(DateTime stopTime)
    {
        _stopTime = stopTime.TruncateToSeconds();
    }

    public override bool IsStopConditionMet(DateTime dateTime)
    {
        if (base.IsStopConditionMet(dateTime))
        {
            return true;
        }

        return dateTime >= _stopTime;
    }
}
