using Common.DataPoints;
using GUI.Interfaces;
using System.Collections.ObjectModel;

namespace GUI.Models;

public class TimemarkerModel : ISeries
{
    public byte Id { get; set; }
    public uint Number { get; set; }
    public ObservableCollection<DataPoint> Data { get; set; }
}