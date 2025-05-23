using Microsoft.UI.Xaml.Data;

namespace Cheryl.Uno.Converters;

public class PaneOpenContentMarginConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        bool isPaneOpen = (bool)value;
        return isPaneOpen ? new Thickness(200, 0, 0, 0) : new Thickness(48, 0, 0, 0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

// Converter pour la visibilité
public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        bool isVisible = (bool)value;
        return isVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
