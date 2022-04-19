namespace Utility.Extensions.DateTimes;

public static class DateTimeExtensions
{
    // https://stackoverflow.com/questions/1004698/how-to-truncate-milliseconds-off-of-a-net-datetime
    public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
    {
        if (timeSpan == TimeSpan.Zero) return dateTime; // Or could throw an ArgumentException
        if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue) return dateTime; // do not modify "guard" values
        return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
    }

    public static DateTime TruncateToSeconds(this DateTime dateTime)
    {
        return dateTime.Truncate(TimeSpan.FromSeconds(1));
    }
}