namespace SampleCore.EventArguments;

public interface ILatestSampleTimeStampArgs
{
    DateTime LatestSampleTimeStamp { get; }
}

internal class LatestSampleTimeStampArgs : EventArgs, ILatestSampleTimeStampArgs
{
    public DateTime LatestSampleTimeStamp { get; }

    public LatestSampleTimeStampArgs(DateTime latestSampleTimeStamp)
    {
        LatestSampleTimeStamp = latestSampleTimeStamp;
    }
}