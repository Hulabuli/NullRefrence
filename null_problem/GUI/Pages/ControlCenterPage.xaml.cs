using GUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace GUI.Pages;

public sealed partial class ControlCenterPage : Page
{
    public ControlCenterPage(ControlCenterViewModel vm)
    {
        this.InitializeComponent();
        DataContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}
