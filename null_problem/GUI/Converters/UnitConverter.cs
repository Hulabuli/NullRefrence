using Common.StandardUnits;
using Microsoft.UI.Xaml.Data;

namespace GUI.Converters;

internal class UnitConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return null;

        if (value is StandardUnit standardUnit)
        {
            if (standardUnit.IsUserType)
            {
                return standardUnit.UserText;
            }

            if (standardUnit.Type == StandardUnitType.Temperature)
                return "°C";

            if (standardUnit.Type == StandardUnitType.Pressure)
                return "bar";
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}