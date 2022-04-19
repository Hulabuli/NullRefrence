using GUI.ViewModels.UserControls;
using Microsoft.UI.Xaml.Controls;

namespace GUI.UserControls;

public sealed partial class ChannelBlockUC : UserControl
{
    public ChannelBlockUC(ChannelBlockViewModel vm)
    {
        this.InitializeComponent();
        this.DataContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }
}

