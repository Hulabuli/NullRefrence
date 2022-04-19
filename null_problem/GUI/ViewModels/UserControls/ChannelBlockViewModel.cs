using GUI.Interfaces;
using GUI.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace GUI.ViewModels.UserControls;

public class ChannelBlockViewModel : ObservableRecipient, IViewModel
{
    private ChannelBlockModel _channelData;
    public ChannelBlockModel ChannelData { get => _channelData; set => SetProperty(ref _channelData, value); }

    public void Initialize(object model)
    {
        if (!(model is ChannelBlockModel))
        {
            throw new ArgumentException(nameof(model) + " Needs to be DataBridge");
        }

        ChannelData = (ChannelBlockModel)model;
    }
}