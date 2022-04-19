using GUI.Interfaces;
using GUI.Models;
using GUI.ViewModels.SessionControl;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using SampleCore.Interfaces.Generators;
using SessionCore.Interfaces;
using Syncfusion.UI.Xaml.Core;
using System.Diagnostics;
using System.Windows.Input;

namespace GUI.ViewModels;

public class SessionControlViewModel : ObservableRecipient, IViewModel
{
    ISessionControlNavigationService _navigator;
    Frame _mainFrame;
    CurrentSession _sessionFocus;

    ISessionManager _sessionManager;
    ISetupModeFactory _setupFactory;
    ISampleGenerator _sampleGenerator;

    ISession _session;

    DateTime _startTime;
    DateTime _endTime;

    public SessionControlViewModel(CurrentSession sessionFocus,
        ISessionControlNavigationService navigator,
        ISessionManager sessionManager, ISetupModeFactory setupModeFactory,
        ISampleGenerator sampleGenerator)
    {
        _sessionFocus = sessionFocus;

        _navigator = navigator;
        _sessionManager = sessionManager;
        _setupFactory = setupModeFactory;
        _sampleGenerator = sampleGenerator;

        _sessionManager.SessionStarted += SessionManager_SessionStarted;
        _sessionManager.SessionSampled += SessionManager_SessionSampled;
        _sessionManager.SessionStopped += SessionManager_SessionStopped;

    }
    public void Initialize(object model)
    {
        if (model is Frame)
        {
            _mainFrame = model as Frame;
        }
    }

    private DelegateCommand advSession;
    public ICommand AdvSession => advSession ??= new DelegateCommand(PerformAdvSession);

    private void PerformAdvSession(object commandParameter)
    {
        _navigator.NavigateTo(typeof(AdvancedSessionViewModel), _mainFrame, (object)GoBack);
    }




    private void GoBack()
    {
        _navigator.NavigateTo(typeof(SessionControlViewModel), _mainFrame);
    }

    private DelegateCommand defaultStart;
    public ICommand DefaultStart => defaultStart ??= new DelegateCommand(PerformDefaultStart);

    private void PerformDefaultStart(object commandParameter)
    {
        var sampelRate = TimeSpan.FromMilliseconds(2000);
        var startTime = _setupFactory.CreateTimeStartMode(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second + 5));
        var endTime = _setupFactory.CreateTimeStopMode(new DateTime(9999, 01, 01));
        var Sensors = _sampleGenerator.Sensors;

        _ = _sessionManager.CreateSession(sampelRate, startTime, endTime, Sensors);
    }
    private DelegateCommand stopSession;
    public ICommand StopSession => stopSession ??= new DelegateCommand(PerformStopSession);

    private void PerformStopSession(object commandParameter)
    {
        _sessionManager.Sessions[_sessionFocus.RunID].StopSetup.Stop();
    }

    #region Event exampels

    private void SessionManager_SessionStopped(object sender, SessionCore.EventArguments.IStoppedArgs e)
    {
        Debug.WriteLine($"Session {e.Session.RunID} Stopped {e.Session.StopTime}");
    }

    private void SessionManager_SessionSampled(object sender, SessionCore.EventArguments.ISampledArgs e)
    {
        var sample = e.Session.Sensors[1].DataStore[e.LatestSampleTimeStamp];
        Debug.WriteLine($"Session {e.Session.RunID} Sampled {e.LatestSampleTimeStamp}, {sample}");
    }

    private void SessionManager_SessionStarted(object sender, SessionCore.EventArguments.IStartedArgs e)
    {
        Debug.WriteLine($"Session {e.Session.RunID} Started {e.Session.StartTime}");
        _sessionFocus.RunID = e.Session.RunID;
    }
    #endregion
}