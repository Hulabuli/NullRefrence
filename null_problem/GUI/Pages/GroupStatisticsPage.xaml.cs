using GUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace GUI.Pages;

public sealed partial class GroupStatisticsPage : Page
{
    public GroupStatisticsPage(GroupStatisticsViewModel vm)
    {
        this.InitializeComponent();
        this.DataContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}
