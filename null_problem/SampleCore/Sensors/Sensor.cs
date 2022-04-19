using Common.StandardUnits;
using SampleCore.Interfaces;
using SampleCore.Sensors.DataStore;
using SampleCore.Sensors.SensorEnums;

namespace SampleCore.Sensors;

internal class Sensor : ISensor
{
    public uint SerialNumber { get; set; }

    public byte ChannelNumber { get; set; }

    public StandardUnit Unit { get; set; } = null!;
    public bool IsActive { get => InternalDataStore.IsActive; set { InternalDataStore.IsActive = value; } }
    public bool HasErrors { get { return Errors.Count > 0; } }

    public IReadOnlyList<SensorError> Errors { get; } = new List<SensorError>(4);

    public ISensorDataStore DataStore => InternalDataStore;
    public SensorDataStore InternalDataStore { get; } = new(20);
}