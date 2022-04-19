using GUI.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using SampleCore.Interfaces.Generators;
using SessionCore.Interfaces;
using Syncfusion.UI.Xaml.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI.ViewModels.SessionControl;

public class AdvancedSessionViewModel : ObservableRecipient, IViewModel
{
    ISessionManager _sessionManager;
    ISetupModeFactory _setupFactory;
    ISampleGenerator _sampleGenerator;

    ISession _session;

    Action GoBack;

    private DateTimeOffset? _startTime;
    public DateTimeOffset? StartTime { get => _startTime; set => SetProperty(ref _startTime, value); }

    private DateTimeOffset? _endTime;
    public DateTimeOffset? EndTime { get => _endTime; set => SetProperty(ref _endTime, value); }

    private int _sampleRate = 1;
    public int SampleRate { get => _sampleRate; set => SetProperty(ref _sampleRate, value); }

    public AdvancedSessionViewModel(
        ISessionManager sessionManager, ISetupModeFactory setupModeFactory, ISampleGenerator sampleGenerator)
    {
        _sessionManager = sessionManager;
        _setupFactory = setupModeFactory;
        _sampleGenerator = sampleGenerator;
    }
    
    public void Initialize(object model = null)
    {
        GoBack = (Action)model;
    }

    private DelegateCommand status;
    public ICommand Status => status ??= new DelegateCommand(PerformStatus);

    private void PerformStatus(object commandParameter)
    {

        //Debug.WriteLine(DateTime.UtcNow.Hour.GetType());
        //Debug.WriteLine(_startTime.Value.Hour.GetType());

        if (_startTime != null)
            Debug.WriteLine("Timepicker start time: " + _startTime);
        else
            Debug.WriteLine("start NULL");

        if (_endTime != null)
            Debug.WriteLine("Timepicker start time: " + _endTime);
        else
            Debug.WriteLine("end NULL");

        if (_session != null)
        {
            Debug.WriteLine("Current time: " + DateTime.Now.ToString());
            Debug.WriteLine("To start: " + _session.StartTime);
            Debug.WriteLine("To end: " + _session.StopTime);
            Debug.WriteLine("Is stopped :" + _session.IsStopped);
            Debug.WriteLine("Is waiting :" + _session.IsWaiting);
            Debug.WriteLine("Is running :" + _session.IsRunning);
            Debug.WriteLine("next sample time: " + _session.NextSampleTime);
        }
    }


    private DelegateCommand startSession;
    public ICommand StartSession => startSession ??= new DelegateCommand(PerformStartSession);

    private void PerformStartSession(object commandParameter)
    {
        if (_startTime != null && _endTime != null)
        {
            Debug.WriteLine(DateTime.Now.ToString());

            var sampelRate = TimeSpan.FromSeconds(_sampleRate);
            var startTime = _setupFactory.CreateTimeStartMode(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, _startTime.Value.Hour - 2, _startTime.Value.Minute, _startTime.Value.Second));
            var endTime = _setupFactory.CreateTimeStopMode(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, _endTime.Value.Hour - 2, _endTime.Value.Minute, _endTime.Value.Second));
            var Sensors = _sampleGenerator.Sensors;

            _sessionManager.CreateSession(sampelRate, startTime, endTime, Sensors);


            
        }
        
        GoBack();
    }
}
