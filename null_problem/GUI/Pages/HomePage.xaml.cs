using GUI.UserControls;
using GUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Charts;

namespace GUI.Pages;

public sealed partial class HomePage : Page
{
    public HomePage(HomePageViewModel vm)
    {
        this.InitializeComponent();
        DataContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }

    private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var gridView = sender as GridView;

        foreach (ChartUC item in gridView.Items)
        {
            item.Height = gridView.ActualHeight - 20;
            item.Width = gridView.ActualWidth;
        }
    }
}
