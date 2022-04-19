using GUI.Interfaces;
using GUI.Models;
using GUI.UserControls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Core;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GUI.ViewModels;
public class HomePageViewModel : ObservableRecipient, IViewModel
{
    private ChartModel _chartControl;
    private ChartUC _chartUC;

    private ObservableCollection<Button> _buttonList = new();
    public ObservableCollection<Button> ButtonList { get => _buttonList; set => SetProperty(ref _buttonList, value); }

    private ObservableCollection<ChartUC> _chart = new();
    public ObservableCollection<ChartUC> Chart { get => _chart; set => SetProperty(ref _chart, value); }

    public HomePageViewModel(ChartModel chartmodel, ChartUC chart)
    {
        _chartControl = chartmodel ?? throw new ArgumentNullException(nameof(chartmodel));
        _chartUC = chart;
        _chart.Add(_chartUC);
        _chartControl.ReloadChart();
        _chartControl.SeriesAdded += AddButton;
    }

    public void Initialize(object model = null)
    {
        _chart.Clear();
        _chart.Add(_chartUC);
        _chartControl.ReloadChart();
    }

    private void AddButton(object sender, byte e)
    {
        _buttonList.Add(new Button() { Content = e.ToString("Ch: 00"), Tag = e, Height = 100, Width = 100, Command = ChannelSelect, CommandParameter = e});
        _buttonList.Sort(x => x.Tag);
    }


    private DelegateCommand _channelSelect;
    public ICommand ChannelSelect => _channelSelect ??= new DelegateCommand(PerformChannelSelect);
    private void PerformChannelSelect(object channel)
    {
        _chartControl.ChannelSelect((byte)channel);
    }


    //Temp code for testing
    private DelegateCommand addTimemarker;
    public ICommand AddTimemarker => addTimemarker ??= new DelegateCommand(PerformAddTimemarker);

    private void PerformAddTimemarker(object commandParameter)
    {
        byte timemarker = 1;
        DateTime time = DateTime.Now;
        time = time.AddHours(-2);
        time = time.AddSeconds(-0.5);

        _chartControl.NewTimemarker(timemarker, time);
    }
}


//Taken from stackoverflow:
//https://stackoverflow.com/questions/44303000/uwp-c-observablecollection-sort-in-place-w-o-scrolling
//Github project solution:
//https://github.com/JustinXinLiu/ListViewSortingRepo
public static class ObservableCollectionExtensions
{
    public static void Sort<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector)
    {
        var sortedSource = source.OrderBy(keySelector).ToList();

        for (var i = 0; i < sortedSource.Count; i++)
        {
            var itemToSort = sortedSource[i];

            // If the item is already at the right position, leave it and continue.
            if (source.IndexOf(itemToSort) == i)
            {
                continue;
            }

            source.Remove(itemToSort);
            source.Insert(i, itemToSort);
        }
    }
}