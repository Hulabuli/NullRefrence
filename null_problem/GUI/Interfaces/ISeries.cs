using Common.DataPoints;
using System.Collections.ObjectModel;

namespace GUI.Interfaces;

public interface ISeries
{
    ObservableCollection<DataPoint> Data { get; set; }
}
