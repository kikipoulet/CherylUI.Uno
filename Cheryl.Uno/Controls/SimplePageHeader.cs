namespace Cheryl.Uno.Controls;

public class SimplePageHeader : Control
{
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(SimplePageHeader),
            new PropertyMetadata("Header"));

    public string Header
    {
        get => (string)GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

}
