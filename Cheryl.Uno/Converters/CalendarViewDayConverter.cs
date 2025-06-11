using System.Reflection;
using Microsoft.UI.Xaml.Data;

namespace Cheryl.Uno.Converters;

public class CalendarViewDayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null) return string.Empty;

        return ((string)value).Replace(".", "").ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

