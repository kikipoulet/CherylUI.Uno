using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System;
using System.Globalization;
using Microsoft.UI.Xaml.Markup;

namespace Cheryl.Uno.Controls;

[ContentProperty(Name = nameof(Content))]
public class SliverPageLong : Control
{
    public SliverPageLong()
    {
        this.DefaultStyleKey = typeof(SliverPageLong);
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(SliverPageLong),
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
            typeof(SliverPageLong),
            new PropertyMetadata(null));

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    private void GoBack(object sender, RoutedEventArgs e)
    {
       // MobileNavigation.Pop();
    }
}

public class OffsetToHeightConverterLong : IValueConverter
{
    public static readonly OffsetToHeightConverterLong Instance = new OffsetToHeightConverterLong();

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        double offset = (double)value;

        if (offset >= 280) 
            return 90;

        return 370 - offset;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}

public class OffsetToOpacityConverterLong : IValueConverter
{
    public static readonly OffsetToOpacityConverterLong Instance = new OffsetToOpacityConverterLong();

    public object Convert(object value, Type targetType, object parameter, string language)
    {

        return (double)value > 180;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}

public class OffsetToInvertOpacityConverterLong : IValueConverter
{
    public static readonly OffsetToInvertOpacityConverterLong Instance = new OffsetToInvertOpacityConverterLong();

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        double offset = (double)value;
        double opacity = 0.2 - ((220 - offset) / 220);

        if (opacity < 0)
            return 0;
        if (opacity > 1)
            return 1;

        return opacity;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}

public class OffsetToFontSizeConverterLong : IValueConverter
{
    public static readonly OffsetToInvertOpacityConverterLong Instance = new OffsetToInvertOpacityConverterLong();

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if ((double)value >= 345)
            return 22;
        
        double Offset =  ((double)value) / 15;

        double fontsize = 45 - (Offset );

        if (fontsize < 22)
            return 22;

        return fontsize;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotSupportedException();
    }
}


