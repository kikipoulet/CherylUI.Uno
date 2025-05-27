using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;

namespace Cheryl.Uno.Controls;

[ContentProperty(Name = nameof(Content))]
public class SliverPage : Control
{
    public SliverPage()
    {
        this.DefaultStyleKey = typeof(SliverPage);
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(SliverPage),
            new PropertyMetadata("Header"));

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(
            nameof(Content),
            typeof(object),
            typeof(SliverPage),
            new PropertyMetadata(null));

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

}

public class OffsetToHeightConverter: IValueConverter
{
    public static readonly OffsetToHeightConverter Instance = new OffsetToHeightConverter();

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        double offset = (double)value;

        return offset > 160 ? 90 : 250 - offset;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}

public class OffsetToOpacityConverter : IValueConverter
{
    public static readonly OffsetToOpacityConverter Instance = new OffsetToOpacityConverter();

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if ((double)value > 80)
            return 0;
        
        return 1 - (((double)value) / 80);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}

public class OffsetToInvertOpacityConverter : IValueConverter
{
    public static readonly OffsetToInvertOpacityConverter Instance = new OffsetToInvertOpacityConverter();

   
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if ((double) value > 270)
            return 1;
        
        if ((double) value < 160)
            return 0;
        
        return -1.6 + (((double)value) / 100);

    }


    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}

