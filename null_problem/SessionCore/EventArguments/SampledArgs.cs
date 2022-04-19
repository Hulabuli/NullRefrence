using SessionCore.Interfaces;

namespace SessionCore.EventArguments;

public interface ISampledArgs
{
    public ISession Session { get; }
    public DateTime LatestSampleTimeStamp { get; }
}

internal class SampledArgs: SessionArgs, ISampledArgs
{
    public DateTime LatestSampleTimeStamp { get; }

    public SampledArgs(ISession session, DateTime latestSampleTimeStamp)
        : base(session)
    {
        LatestSampleTimeStamp = latestSampleTimeStamp;
    }
}