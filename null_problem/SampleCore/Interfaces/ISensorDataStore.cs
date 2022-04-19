using Common.DataPoints;

namespace SampleCore.Interfaces;

public interface ISensorDataStore
{
    public bool IsActive { get; }
    DataPoint this[DateTime timeStamp] { get; }
}