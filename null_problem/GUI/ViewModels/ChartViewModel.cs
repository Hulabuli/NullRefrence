using GUI.Interfaces;
using GUI.Models;
using GUI.UserControls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GUI.ViewModels;

public class ChartViewModel : ObservableRecipient, IViewModel
{
    private ChartModel _chartControl;
    private ChartUC _chartUC;

    private ObservableCollection<ChartUC> _chart = new();
    public ObservableCollection<ChartUC> Chart { get => _chart; set => SetProperty(ref _chart, value); }

    CurrentSession _focusSession;

    public ChartViewModel(ChartModel chartmodel, ChartUC chart, CurrentSession focusSession)
    {
        _chartControl = chartmodel ?? throw new ArgumentNullException(nameof(chartmodel));
        _focusSession = focusSession ?? throw new ArgumentNullException("asdasdasd");
        _chartUC = chart;
        _chart.Add(_chartUC);
        _chartControl.ReloadChart(1);
    }

    public void Initialize(object model = null)
    {
        _chart.Clear();
        _chart.Add(_chartUC);
        _chartControl.ReloadChart(_focusSession.RunID);
    }
}