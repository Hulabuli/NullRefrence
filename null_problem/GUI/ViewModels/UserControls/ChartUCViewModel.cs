using Common.StandardUnits;
using GUI.Interfaces;
using GUI.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Dispatching;
using SampleCore.EventArguments;
using SampleCore.Interfaces;
using SampleCore.Interfaces.Generators;
using Syncfusion.UI.Xaml.Charts;
using System.Collections.ObjectModel;

namespace GUI.ViewModels.UserControls;

public class ChartUCViewModel : ObservableRecipient, IViewModel
{
    private ChartModel _chartControl;
    public ChartModel ChartModel { get => _chartControl; set => SetProperty(ref _chartControl, value); }

    public ChartUCViewModel(ChartModel chartmodel)
    {
        _chartControl = chartmodel ?? throw new ArgumentNullException(nameof(chartmodel));
    }

    public void Initialize(object model = null)
    {
        
    }
}
