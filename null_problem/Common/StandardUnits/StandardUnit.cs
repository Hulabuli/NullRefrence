namespace Common.StandardUnits;

public class StandardUnit : IEquatable<StandardUnit>
{
    public bool IsUserType { get { return Type == StandardUnitType.UserText; } }
    public StandardUnitType Type { get; }
    public string UserText { get; }

    public StandardUnit(StandardUnitType type, string userText = default!)
    {
        Type = type;
        UserText = userText;
    }

    public bool Equals(StandardUnit? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.IsUserType && IsUserType)
        {
            return !string.IsNullOrEmpty(other.UserText) && !string.IsNullOrEmpty(UserText) && (other.UserText == UserText);
        }

        return other.Type == Type;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is StandardUnit standardUnit)
        {
            return Equals(standardUnit);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IsUserType, Type, UserText);
    }

    public override string ToString()
    {
        if (IsUserType)
            return UserText;
        return $"{Type}";
    }

    //Overloading - Equality 
    public static bool operator ==(StandardUnit standardUnit1, StandardUnit standardUnit2)
    {
        if (standardUnit1 is null || standardUnit2 is null)
        {
            return Equals(standardUnit1, standardUnit2);
        }

        return standardUnit1.Equals(standardUnit2);
    }

    //Overloading - InEquality 
    public static bool operator !=(StandardUnit standardUnit1, StandardUnit standardUnit2)
    {
        return !(standardUnit1 == standardUnit2);
    }
}

public enum StandardUnitType : byte
{
    UserText = 0,
    /// <summary>
    /// Temperature in Celsius
    /// </summary>
    Temperature = 1,
    /// <summary>
    /// Pressure in bar
    /// </summary>
    Pressure = 2,
    DigitalInput = 3,
    DigitalOutput = 4,
    Analog420mA = 5,
    Analog010V = 6,
    RelativeHumidity = 7,
    /// <summary>
    /// Deflection in mm
    /// </summary>
    Deflection = 8,
    /// <summary>
    /// Conductivity in uS/cm
    /// </summary>
    Conductivity = 9,
    CO2 = 10,

    TypeT = 21,
    TypeJ = 22,
    TypeK = 23,
    Reserved1 = 24,
    Reserved2 = 25,
    TypeN = 26,
    Reserved3 = 27,
    Reserved4 = 28
}