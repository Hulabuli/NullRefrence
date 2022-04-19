using GUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace GUI.Pages;

public sealed partial class SessionControlPage : Page
{
    public SessionControlPage(SessionControlViewModel vm)
    {
        this.InitializeComponent();
        DataContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}

