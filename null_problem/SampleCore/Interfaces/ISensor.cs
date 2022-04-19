using Common.StandardUnits;
using SampleCore.Sensors.SensorEnums;

namespace SampleCore.Interfaces;

public interface ISensor
{
    public uint SerialNumber { get; }
    public byte ChannelNumber { get; }
    public StandardUnit Unit { get; }
    public bool IsActive { get; }
    public bool HasErrors { get; }
    public IReadOnlyList<SensorError> Errors { get; }
    public ISensorDataStore DataStore { get; }


}
