namespace SessionCore.Interfaces;

public interface ISampleRate
{
    TimeSpan Value { get; }
    bool UseFastSample { get; set; }
}