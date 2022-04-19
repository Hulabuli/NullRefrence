using Common.DataPoints;
using Common.StandardUnits;
using GUI.Interfaces;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GUI.Models;

public class SeriesModel : ObservableObject, ISeries
{
    public uint SerialNumber { get; set; }
    public byte Channel { get; set; }
    private ObservableCollection<DataPoint> _data;
    public ObservableCollection<DataPoint> Data { get => _data; set => SetProperty(ref _data, value); }
    public StandardUnitType DataType { get; set; }
}