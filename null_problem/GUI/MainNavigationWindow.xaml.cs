using GUI.Interfaces;
using GUI.Models;
using GUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GUI;

public sealed partial class MainNavigationWindow : Window, IWindow
{
    private readonly INavigationService _navigator;
    private readonly ChartModel _chartModel;

    public MainNavigationWindow(INavigationService navigator, ChartModel chartModel)
    {
        this.InitializeComponent();
        _navigator = navigator;
        _chartModel = chartModel;
    }

    private void MainNavigation_Loaded(object sender, RoutedEventArgs e)
    {
        _navigator.NavigateTo<HomePageViewModel>(ContentFrame);
    }

    private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        _chartModel.UnloadChart();
        var tag = (args.SelectedItem as NavigationViewItem).Tag.ToString();
        if (tag == "HomePage")
        {
            _navigator.NavigateTo(typeof(HomePageViewModel), ContentFrame);
        }
        else if (tag == "SessionControl")
        {
            _navigator.NavigateTo<SessionControlViewModel>(ContentFrame, ContentFrame);
        }
        else if (tag == "ControlCenter")
        {
            _navigator.NavigateTo<ControlCenterViewModel>(ContentFrame);
        }
        else if (tag == "Sensorview")
        {
            _navigator.NavigateTo<SensorValueViewModel>(ContentFrame);
        }
        else if (tag == "Chart")
        {
            _navigator.NavigateTo<ChartViewModel>(ContentFrame);
        }
        else if (tag == "Statistics")
        {
            _navigator.NavigateTo<GroupStatisticsViewModel>(ContentFrame);
        }
        else
        {
            throw new NotImplementedException("Unknown viewmodel:" + tag);
        }
    }
}
