using Common.Interfaces.StandardUnits;

namespace Common.StandardUnits;

public class StandardUnitTable : IStandardUnitTable
{
    readonly Dictionary<StandardUnitType, StandardUnit> StandardUnitsByType = new();
    readonly Dictionary<string, StandardUnit> StandardUnitsByUserText = new();

    public StandardUnit this[StandardUnitType type] => GetStandardUnit(type);

    public StandardUnit this[string userText] => GetStandardUnit(userText);

    private StandardUnit GetStandardUnit(StandardUnitType type)
    {
        if (!StandardUnitsByType.ContainsKey(type))
        {
            StandardUnitsByType.Add(type, new StandardUnit(type));
        }

        return StandardUnitsByType[type];
    }

    private StandardUnit GetStandardUnit(string userText)
    {
        if (!StandardUnitsByUserText.ContainsKey(userText))
        {
            StandardUnitsByUserText.Add(userText, new StandardUnit(StandardUnitType.UserText, userText));
        }

        return StandardUnitsByUserText[userText];
    }
}