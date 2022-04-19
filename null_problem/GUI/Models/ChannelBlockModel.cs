using Common.StandardUnits;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace GUI.Models;

public class ChannelBlockModel : ObservableObject
{
    private int _id;
    private double _data;
    private StandardUnit _unit;
    public int Id { get => _id; set => SetProperty(ref _id, value); }
    public double Data { get => _data; set => SetProperty(ref _data, value); }
    public StandardUnit Unit { get => _unit; set => SetProperty(ref _unit, value); }
}
