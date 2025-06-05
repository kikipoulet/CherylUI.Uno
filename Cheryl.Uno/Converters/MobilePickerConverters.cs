using System.Reflection;
using Microsoft.UI.Xaml.Data;

namespace Cheryl.Uno.Converters;

public class ItemDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null) return string.Empty;
        string displayMemberPath = parameter as string;

        if (!string.IsNullOrEmpty(displayMemberPath))
        {
            try
            {
                PropertyInfo propInfo = value.GetType().GetProperty(displayMemberPath);
                if (propInfo != null)
                {
                    return propInfo.GetValue(value)?.ToString() ?? string.Empty;
                }
            }
            catch { /* Fallback to ToString() */ }
        }
        return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}



public class NotNullToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value != null ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
