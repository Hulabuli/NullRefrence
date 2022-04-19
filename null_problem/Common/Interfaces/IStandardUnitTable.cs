using Common.StandardUnits;

namespace Common.Interfaces.StandardUnits;

public interface IStandardUnitTable
{
    StandardUnit this[StandardUnitType type] { get; }
    StandardUnit this[string userText] { get; }
}