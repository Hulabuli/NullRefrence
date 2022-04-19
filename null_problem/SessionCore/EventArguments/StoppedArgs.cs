using SessionCore.Interfaces;

namespace SessionCore.EventArguments;

public interface IStoppedArgs
{
    public ISession Session { get; }
}

internal class StoppedArgs : SessionArgs, IStoppedArgs
{
    public StoppedArgs(ISession session)
        : base(session)
    {
    }
}