
using System;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

namespace Cheryl.Uno.Controls.Pickers.MobileTimePicker;

public class MobileTimePicker : Control
{
    private TextBlock? _timeTextPart;

    public MobileTimePicker()
    {
        DefaultStyleKey = typeof(MobileTimePicker);
        Tapped += OnControlTapped;
        UpdateDisplayedText();
    }

    #region Dependency Properties

    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register(nameof(Time), typeof(TimeSpan?), typeof(MobileTimePicker), new PropertyMetadata(null, OnTimeChanged));

    public TimeSpan? Time
    {
        get => (TimeSpan?)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }

    private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var picker = (MobileTimePicker)d;
        picker.UpdateDisplayedText();
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(MobileTimePicker), new PropertyMetadata(null));

    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty PlaceholderTextProperty =
        DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(MobileTimePicker), new PropertyMetadata("hh:mm"));

    public string PlaceholderText
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    public static readonly DependencyProperty PopupTitleProperty =
        DependencyProperty.Register(nameof(PopupTitle), typeof(string), typeof(MobileTimePicker), new PropertyMetadata("Sélectionnez une heure"));

    public string PopupTitle
    {
        get => (string)GetValue(PopupTitleProperty);
        set => SetValue(PopupTitleProperty, value);
    }

    #endregion

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _timeTextPart = GetTemplateChild("TimeTextPart") as TextBlock;
        UpdateDisplayedText();
    }

    private async void OnControlTapped(object sender, TappedRoutedEventArgs e)
    {
        var panel = new MobileTimePickerPanel
        {
            InitialTime = Time,
            Title = PopupTitle
        };

        var result = await InteractiveContainer.ShowBottomSheet(panel);

        if (result is TimeSpan newTime)
        {
            Time = newTime;
        }
    }

    private void UpdateDisplayedText()
    {
        if (_timeTextPart == null) return;

        if (Time.HasValue)
        {
            _timeTextPart.Text = FormatTime(Time.Value);
            _timeTextPart.Opacity = 1.0;
        }
        else
        {
            _timeTextPart.Text = PlaceholderText;
            _timeTextPart.Opacity = 0.6;
        }
    }

    private static string FormatTime(TimeSpan time) => time.ToString(@"hh\:mm");
}

public class MobileTimePickerPanel : Control
{
    private ListView? _hoursListViewPart;
    private ListView? _minutesListViewPart;
    private Button? _acceptButtonPart;

    public MobileTimePickerPanel()
    {
        DefaultStyleKey = typeof(MobileTimePickerPanel);
        Loaded += OnPanelLoaded;
    }

    #region Dependency Properties

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(MobileTimePickerPanel), new PropertyMetadata(string.Empty));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty InitialTimeProperty =
        DependencyProperty.Register(nameof(InitialTime), typeof(TimeSpan?), typeof(MobileTimePickerPanel), new PropertyMetadata(null));

    public TimeSpan? InitialTime
    {
        get => (TimeSpan?)GetValue(InitialTimeProperty);
        set => SetValue(InitialTimeProperty, value);
    }

    #endregion

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _hoursListViewPart = GetTemplateChild("HoursListViewPart") as ListView;
        _minutesListViewPart = GetTemplateChild("MinutesListViewPart") as ListView;
        _acceptButtonPart = GetTemplateChild("AcceptButtonPart") as Button;

        if (_acceptButtonPart != null)
            _acceptButtonPart.Click += OnAcceptClick;

        PopulateLists();
    }

    private void OnPanelLoaded(object sender, RoutedEventArgs e)
    {
        if (InitialTime.HasValue)
        {
            int h = InitialTime.Value.Hours;
            int m = InitialTime.Value.Minutes;
            _hoursListViewPart?.ScrollIntoView(h);
            _minutesListViewPart?.ScrollIntoView(m.ToString("D2"));
            if (_hoursListViewPart != null) _hoursListViewPart.SelectedItem = h;
            if (_minutesListViewPart != null) _minutesListViewPart.SelectedItem = m.ToString("D2");
        }
    }

    private void PopulateLists()
    {
        if (_hoursListViewPart != null)
        {
            _hoursListViewPart.ItemsSource = Enumerable.Range(0, 24).ToList();
        }

        if (_minutesListViewPart != null)
        {
            _minutesListViewPart.ItemsSource = Enumerable.Range(0, 60).Select(i => i.ToString("D2")).ToList();
        }
    }

    private void OnAcceptClick(object sender, RoutedEventArgs e)
    {
        if (_hoursListViewPart?.SelectedItem is int hour &&
            _minutesListViewPart?.SelectedItem is string minuteStr &&
            int.TryParse(minuteStr, out var minute))
        {
            var time = new TimeSpan(hour, minute, 0);
            InteractiveContainer.CloseBottomSheet(time);
        }
        else
        {
            InteractiveContainer.CloseBottomSheet(null);
        }
    }
}
