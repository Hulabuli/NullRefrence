using SampleCore.Interfaces;
using SessionCore.EventArguments;
using SessionCore.Interfaces;

namespace SessionCore.Handlers;

internal class Session : ISession
{
    private SessionState _state;

    public uint RunID { get; }
    public ISampleRate SampleRate { get; }
    public IStartSetup StartSetup { get; }
    public IStopSetup StopSetup { get; }
    public DateTime StartTime { get; private set; }
    public DateTime LatestSampleTime { get; private set; }
    public DateTime NextSampleTime { get; private set; }
    public DateTime StopTime { get; private set; }
    public bool IsWaiting { get => _state == SessionState.Waiting; }
    public bool IsRunning { get => _state == SessionState.Running; }
    public bool IsStopped { get => _state == SessionState.Stopped; }
    public IReadOnlyDictionary<byte, ISensor> Sensors { get; }

    public event EventHandler<IStartedArgs>? Started;
    public event EventHandler<ISampledArgs>? Sampled;
    public event EventHandler<IStoppedArgs>? Stopped;

    public Session(uint runID, ISampleRate sampleRate, IStartSetup startSetup, IStopSetup stopSetup, IReadOnlyDictionary<byte, ISensor> sensors)
    {
        _state = SessionState.Waiting;

        RunID = runID;
        SampleRate = sampleRate;
        StartSetup = startSetup;
        StopSetup = stopSetup;
        Sensors = sensors;
    }

    public void SamlpesToProcess(DateTime dateTime)
    {
        switch (_state)
        {
            case SessionState.Waiting:
                CheckStartCondition(dateTime);
                break;
            case SessionState.Running:
                ProcessSamples(dateTime);
                CheckStopCondition(dateTime);
                break;
            case SessionState.Stopped:
                break;
        }
    }

    private void CheckStartCondition(DateTime dateTime)
    {
        if (StartSetup.IsStartConditionMet(dateTime))
        {
            StartTime = dateTime;
            NextSampleTime = dateTime;
            _state = SessionState.Running;

            System.Diagnostics.Debug.WriteLine($"{RunID} is started {dateTime}");

            Started?.Invoke(this, new StartedArgs(this));
            ProcessSamples(dateTime);
        }
    }

    private void ProcessSamples(DateTime dateTime)
    {
        if (NextSampleTime <= dateTime)
        {
            //TODO: Sample Processing.

            System.Diagnostics.Debug.WriteLine($"{RunID} took a sample {dateTime}");

            LatestSampleTime = dateTime;
            NextSampleTime = dateTime.Add(SampleRate.Value);

            Sampled?.Invoke(this, new SampledArgs(this, dateTime));
        }
    }

    private void CheckStopCondition(DateTime dateTime)
    {
        if (StopSetup.IsStopConditionMet(dateTime))
        {
            StopTime = dateTime;
            _state = SessionState.Stopped;

            System.Diagnostics.Debug.WriteLine($"{RunID} is stopped {dateTime}");
            Stopped?.Invoke(this, new StoppedArgs(this));
        }
    }

    enum SessionState
    {
        Waiting,
        Running,
        Stopped
    }
}