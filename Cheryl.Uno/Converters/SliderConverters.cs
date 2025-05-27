using Microsoft.UI.Xaml.Data;

namespace Cheryl.Uno.Converters;

public class SliderLengthToCRConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {

        return new Thickness(10); 
    }
    

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
