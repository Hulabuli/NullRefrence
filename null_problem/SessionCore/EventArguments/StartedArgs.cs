using SessionCore.Interfaces;

namespace SessionCore.EventArguments;

public interface IStartedArgs
{
    public ISession Session { get; }
}

internal class StartedArgs : SessionArgs, IStartedArgs
{
    public StartedArgs(ISession session)
        : base(session)
    {
    }
}