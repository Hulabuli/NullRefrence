using SessionCore.Interfaces;

namespace SessionCore.EventArguments;

internal class SessionArgs : EventArgs
{
    public ISession Session { get; }

    public SessionArgs(ISession session)
    {
        Session = session;
    }
}