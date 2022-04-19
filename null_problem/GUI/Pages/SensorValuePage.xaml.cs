using GUI.UserControls;
using GUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GUI.Pages;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SensorValuePage : Page
{
    public SensorValuePage(SensorValueViewModel vm)
    {
        this.InitializeComponent();
        DataContext = vm ?? throw new ArgumentNullException(nameof(vm));
    }

    private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var _sender = sender as GridView;
        double _height = 0;
        double _width = 0;
        double _fontsize = 0;

        try
        {
            _height = (_sender.ActualHeight / (_sender.Items.Count / 4)) - 15;
            _width = (_sender.ActualWidth / (_sender.Items.Count / 3)) - 5;
            _fontsize = _sender.ActualHeight / (_sender.Items.Count / 4) / 2.2;

            foreach (var item in Array1.Items)
            {
                ((ChannelBlockUC)item).Height = _height;
                ((ChannelBlockUC)item).Width = _width;
                ((ChannelBlockUC)item).FontSize = _fontsize;
            }
            foreach (var item in Array2.Items)
            {
                ((ChannelBlockUC)item).Height = _height;
                ((ChannelBlockUC)item).Width = _width;
                ((ChannelBlockUC)item).FontSize = _fontsize;
            }
            foreach (var item in Array3.Items)
            {
                ((ChannelBlockUC)item).Height = _height;
                ((ChannelBlockUC)item).Width = _width;
                ((ChannelBlockUC)item).FontSize = _fontsize;
            }
            foreach (var item in ArrayX.Items)
            {
                ((ChannelBlockUC)item).Height = _height;
                ((ChannelBlockUC)item).Width = _width;
                ((ChannelBlockUC)item).FontSize = _fontsize;
            }
        }
        catch (Exception)
        {

        }
    }
}
