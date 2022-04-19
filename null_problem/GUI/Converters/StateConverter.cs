using Microsoft.UI.Xaml.Data;

namespace GUI.Converters;

internal class StateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return null;

        if (value is bool @bool)
        {
            if (@bool)
            {
                return 1;
            }
            else
            {
                return 0.5;
            }
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
