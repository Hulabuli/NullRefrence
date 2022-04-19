using Microsoft.UI.Xaml.Data;

namespace GUI.Converters;

internal class IdConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return null;

        if (value is int @int)
        {
            if (@int <= 36)
                return @int.ToString("00:");
            return (@int - 36).ToString("X0:");
        }

        if (value is byte @byte)
        {
            if (@byte <= 36)
                return @byte.ToString("00:");
            return (@byte - 36).ToString("X0:");
        }

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}