using GUI.Interfaces;
using GUI.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using SessionCore.EventArguments;
using SessionCore.Interfaces;
using Syncfusion.UI.Xaml.Core;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace GUI.ViewModels;

public class ControlCenterViewModel : ObservableRecipient, IViewModel
{
    ISessionManager _sessionManager;

    private uint _session;
    CurrentSession _focusSession;
    public uint Session { get => _session; set => SetProperty(ref _session, value); }
    private ObservableCollection<Button> _buttonList;
    public ObservableCollection<Button> ButtonList { get => _buttonList; set => SetProperty(ref _buttonList, value); }

    public ControlCenterViewModel(ISessionManager sessionManager, CurrentSession focusSession)
    {
        _sessionManager = sessionManager;
        _focusSession = focusSession;

        //_sessionManager.SessionStarted += NewSession;
    }

    public void Initialize(object model)
    {

    }

    private void NewSession(object sender, IStartedArgs e)
    {
        
    }

    private DelegateCommand _sessionSwitch;
    public ICommand SessionSwitch => _sessionSwitch ??= new DelegateCommand(PerformSessionSwitch);

    private void PerformSessionSwitch(object commandParameter)
    {
        if (_sessionManager.Sessions.ContainsKey(_session))
        {
            _focusSession.RunID = _session;
        }
        else
        {
            Debug.WriteLine("Session not found");
            _focusSession.RunID = 0;
        }
    }
}

