using SampleCore.Interfaces;
using SampleCore.Interfaces.Generators;
using SessionCore.EventArguments;
using SessionCore.Interfaces;
using System.Diagnostics;
using System.Threading.Channels;

namespace SessionCore.Handlers;

internal class SessionManager : ISessionManager
{
    private const byte _noOfSessions = 20;

    private readonly ISampleGenerator _sampleGenerator;
    private readonly ISessionFactory _sessionFactory;
    private readonly Dictionary<uint, Session> _internalSessions = new(capacity: _noOfSessions);
    private readonly Dictionary<uint, ISession> _internalISessions = new(capacity: _noOfSessions);

    private Channel<Action> EventChannel { get; } =
    Channel.CreateBounded<Action>(new BoundedChannelOptions(_noOfSessions * 3)
    {
        FullMode = BoundedChannelFullMode.DropOldest,
        SingleReader = true,
        SingleWriter = true
    }, (action) => System.Diagnostics.Debug.WriteLine($"{action} was dropped {DateTime.UtcNow}"));
    private Task EventChannelTask { get; }

    public byte MaxSessionCount => _noOfSessions;
    public IReadOnlyDictionary<uint, ISession> Sessions { get => _internalISessions; }

    public event EventHandler<IStartedArgs>? SessionStarted;
    public event EventHandler<ISampledArgs>? SessionSampled;
    public event EventHandler<IStoppedArgs>? SessionStopped;

    public SessionManager(ISampleGenerator sampleGenerator, ISessionFactory sessionFactory)
    {
        _sampleGenerator = sampleGenerator;
        _sampleGenerator.LatestSampleTimeStamp += SampleGenerator_LatestSampleTimeStamp;
        _sessionFactory = sessionFactory;

        EventChannelTask = Task.Run(async () => await ConsumeEventChannelAsync());
    }

    private async Task ConsumeEventChannelAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var action = await EventChannel.Reader.ReadAsync(cancellationToken);
                action();

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        Debug.WriteLine("ConsumeEventChannelAsync Completed: " + DateTime.Now.ToString("HH:mm:ss.fff"));
    }

    private void SampleGenerator_LatestSampleTimeStamp(object? sender, SampleCore.EventArguments.ILatestSampleTimeStampArgs e)
    {
        try
        {
            //TODO: Two core parallel processing.
            foreach (var session in _internalISessions)
            {
                session.Value.SamlpesToProcess(e.LatestSampleTimeStamp);
            }
        }
        catch (Exception)
        {
            // TODO: Logging
            throw;
        }
    }

    public ISession CreateSession(TimeSpan sampleRate, IStartSetup startSetup, IStopSetup stopSetup, IReadOnlyDictionary<byte, ISensor> sensors)
    {
        var newSession = _sessionFactory.CreateSession(sampleRate, startSetup, stopSetup, sensors);
        AddNewSession(newSession);
        return newSession;
    }

    private void AddNewSession(Session session)
    {
        if (session is null)
        {
            throw new ArgumentNullException(nameof(session));
        }

        if (_internalISessions.ContainsKey(session.RunID))
        {
            throw new ArgumentException($"A session with {nameof(session.RunID)}, {session.RunID}, already exist.", nameof(session.RunID));
        }

        _internalSessions[session.RunID] = session;
        _internalISessions[session.RunID] = session;
        SubscribeToSession(session);
    }

    #region Session Events
    private void SubscribeToSession(Session session)
    {
        session.Started += Session_Started;
        session.Sampled += Session_Sampled;
        session.Stopped += Session_Stopped;
    }

    private void Session_Started(object? sender, IStartedArgs e)
    {
        EventChannel.Writer.TryWrite(() => OnSessionStarted(e));
    }

    private void OnSessionStarted(IStartedArgs startedArgs)
    {
        SessionStarted?.Invoke(this, startedArgs);
    }

    private void Session_Sampled(object? sender, ISampledArgs e)
    {
        EventChannel.Writer.TryWrite(() => OnSessionSampled(e));
    }

    private void OnSessionSampled(ISampledArgs sampledArgs)
    {
        SessionSampled?.Invoke(this, sampledArgs);
    }

    private void Session_Stopped(object? sender, IStoppedArgs e)
    {
        UnSubscribeToSession(e.Session);

        EventChannel.Writer.TryWrite(() => OnSessionStopped(e));
    }

    private void OnSessionStopped(IStoppedArgs stoppedArgs)
    {
        SessionStopped?.Invoke(this, stoppedArgs);
    }

    private void UnSubscribeToSession(ISession session)
    {
        if (_internalSessions.ContainsKey(session.RunID))
        {
            var internalSession = _internalSessions[session.RunID];
            internalSession.Started -= Session_Started;
            internalSession.Sampled -= Session_Sampled;
            internalSession.Stopped -= Session_Stopped;
        }
    }
    #endregion
}