using SessionCore.Interfaces;
using Utility.Extensions.DateTimes;

namespace SessionCore.Modes;

internal class TimeStartMode : IStartSetup
{
    private readonly DateTime _startTime;

    public TimeStartMode(DateTime startTime)
    {
        _startTime = startTime.TruncateToSeconds();
    }

    public bool IsStartConditionMet(DateTime dateTime)
    {
        return dateTime >= _startTime;
    }
}
