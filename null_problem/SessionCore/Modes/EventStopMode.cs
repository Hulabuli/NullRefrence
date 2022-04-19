using SessionCore.Interfaces;
using Utility.Extensions.DateTimes;

namespace SessionCore.Modes;

internal class EventStopMode : IStopSetup
{
    private DateTime _stopTime = DateTime.MaxValue;

    public void Stop()
    {
        _stopTime = DateTime.UtcNow.TruncateToSeconds();
    }

    public virtual bool IsStopConditionMet(DateTime dateTime)
    {
        return dateTime > _stopTime;
    }
}
