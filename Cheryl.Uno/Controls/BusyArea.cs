using Microsoft.UI.Xaml.Markup;

namespace Cheryl.Uno.Controls;

[ContentProperty(Name = nameof(Content))]
public sealed class BusyArea : Control
{
    public BusyArea()
    {
        this.DefaultStyleKey = typeof(BusyArea);
    }
    
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        UpdateVisualState(false);
    }

    private void UpdateVisualState(bool useTransitions)
    {
        VisualStateManager.GoToState(this, IsBusy ? "Busy" : "Idle", useTransitions);
    }

    private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (BusyArea)d;
        control.UpdateVisualState(true);
    }


    public static readonly DependencyProperty ContentProperty =
        DependencyProperty.Register(nameof(Content), typeof(object), typeof(BusyArea), new PropertyMetadata(null));

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }


    public static readonly DependencyProperty IsBusyProperty =
        DependencyProperty.Register(nameof(IsBusy), typeof(bool), typeof(BusyArea),
            new PropertyMetadata(false, OnIsBusyChanged));

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public static readonly DependencyProperty BusyTextProperty =
        DependencyProperty.Register(nameof(BusyText), typeof(string), typeof(BusyArea), new PropertyMetadata(""));

    public string BusyText
    {
        get => (string)GetValue(BusyTextProperty);
        set => SetValue(BusyTextProperty, value);
    }

    public static readonly DependencyProperty IsIndeterminateProperty =
        DependencyProperty.Register(nameof(IsIndeterminate), typeof(bool), typeof(BusyArea), new PropertyMetadata(true));

    public bool IsIndeterminate
    {
        get => (bool)GetValue(IsIndeterminateProperty);
        set => SetValue(IsIndeterminateProperty, value);
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(BusyArea), new PropertyMetadata(0.0));

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}
