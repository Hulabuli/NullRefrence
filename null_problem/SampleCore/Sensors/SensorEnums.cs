namespace SampleCore.Sensors.SensorEnums;

public enum SensorError
{
    Unknown = 0,
    SensorOverCurrent = 1,
    PT1000InValid = 2,
    PT1000NotConnected = 3,
}

public enum RawValueType
{
    Unknown = 0,
    PT1000AD = 1,
    TCAD = 2,
}