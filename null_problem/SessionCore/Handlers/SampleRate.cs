using SessionCore.Interfaces;

namespace SessionCore.Handlers;

internal class SampleRate : ISampleRate
{
    private readonly TimeSpan _fastSampleRate = TimeSpan.FromSeconds(1);
    private readonly TimeSpan _sampleRate;
    public TimeSpan Value { get => GetValue(); }

    public bool UseFastSample { get; set; }

    public SampleRate(TimeSpan value)
    {
        _sampleRate = value;
    }

    private TimeSpan GetValue()
    {
        return UseFastSample ? _fastSampleRate : _sampleRate;
    }
}
