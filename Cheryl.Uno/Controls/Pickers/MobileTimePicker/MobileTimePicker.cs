
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

internal class MobileTimePickerPanel : Control
{
    private StackPanel? _hoursPanelPart;
    private StackPanel? _minutesPanelPart;
    private TextBlock? _hourAboveTwoPart;
    private TextBlock? _hourAboveOnePart;
    private TextBlock? _hourSelectedPart;
    private TextBlock? _hourBelowOnePart;
    private TextBlock? _hourBelowTwoPart;

    private TextBlock? _minuteAboveTwoPart;
    private TextBlock? _minuteAboveOnePart;
    private TextBlock? _minuteSelectedPart;
    private TextBlock? _minuteBelowOnePart;
    private TextBlock? _minuteBelowTwoPart;

    public Button? AcceptButtonPart;

    private int _selectedHour;
    private int _selectedMinute;

    private double _hourManipulationDelta;
    private double _minuteManipulationDelta;

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
        _hoursPanelPart = GetTemplateChild("HoursPanelPart") as StackPanel;
        _minutesPanelPart = GetTemplateChild("MinutesPanelPart") as StackPanel;
        _hourAboveTwoPart = GetTemplateChild("HourAboveTwoPart") as TextBlock;
        _hourAboveOnePart = GetTemplateChild("HourAboveOnePart") as TextBlock;
        _hourSelectedPart = GetTemplateChild("HourSelectedPart") as TextBlock;
        _hourBelowOnePart = GetTemplateChild("HourBelowOnePart") as TextBlock;
        _hourBelowTwoPart = GetTemplateChild("HourBelowTwoPart") as TextBlock;
        _minuteAboveTwoPart = GetTemplateChild("MinuteAboveTwoPart") as TextBlock;
        _minuteAboveOnePart = GetTemplateChild("MinuteAboveOnePart") as TextBlock;
        _minuteSelectedPart = GetTemplateChild("MinuteSelectedPart") as TextBlock;
        _minuteBelowOnePart = GetTemplateChild("MinuteBelowOnePart") as TextBlock;
        _minuteBelowTwoPart = GetTemplateChild("MinuteBelowTwoPart") as TextBlock;
        AcceptButtonPart = GetTemplateChild("AcceptButtonPart") as Button;

        if (AcceptButtonPart != null)
            AcceptButtonPart.Click += OnAcceptClick;

        if (_hoursPanelPart != null)
        {
            _hoursPanelPart.PointerWheelChanged += HoursWheelChanged;
            _hoursPanelPart.ManipulationMode = ManipulationModes.TranslateY;
            _hoursPanelPart.ManipulationDelta += HoursManipulationDelta;
        }

        if (_minutesPanelPart != null)
        {
            _minutesPanelPart.PointerWheelChanged += MinutesWheelChanged;
            _minutesPanelPart.ManipulationMode = ManipulationModes.TranslateY;
            _minutesPanelPart.ManipulationDelta += MinutesManipulationDelta;
        }

        UpdateHourTexts();
        UpdateMinuteTexts();
    }

    private void MinutesWheelChanged(object sender, PointerRoutedEventArgs e)
    {
        var delta = e.GetCurrentPoint((UIElement)sender).Properties.MouseWheelDelta;
        if (delta > 0) DecrementMinute();
        else if (delta < 0) IncrementMinute();
        e.Handled = true;
    }

    private void HoursManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
        _hourManipulationDelta += e.Delta.Translation.Y;
        if (Math.Abs(_hourManipulationDelta) > 30)
        {
 
            if (_hourManipulationDelta > 0) DecrementHour();
            else IncrementHour();
            _hourManipulationDelta = 0;
        }
       
    }
    
    private void MinutesManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
        _minuteManipulationDelta += e.Delta.Translation.Y;
        if (Math.Abs(_minuteManipulationDelta) > 30)
        {
            if (_minuteManipulationDelta > 0) 
                DecrementMinute();
            else 
                IncrementMinute();
            
            _minuteManipulationDelta = 0;
        }
    }
    
    private void OnPanelLoaded(object sender, RoutedEventArgs e)
    {
        if (InitialTime.HasValue)
        {
            _selectedHour = InitialTime.Value.Hours;
            _selectedMinute = InitialTime.Value.Minutes;
        }
        
        UpdateHourTexts();
        UpdateMinuteTexts();
    }


    private void OnAcceptClick(object sender, RoutedEventArgs e)
    {
        var time = new TimeSpan(_selectedHour, _selectedMinute, 0);
        InteractiveContainer.CloseBottomSheet(time);
    }
    
    private void HoursWheelChanged(object sender, PointerRoutedEventArgs e)
    {
        var delta = e.GetCurrentPoint((UIElement)sender).Properties.MouseWheelDelta;
        if (delta > 0) DecrementHour();
        else if (delta < 0) IncrementHour();
        e.Handled = true;
    }
    
    private void IncrementHour()
    {
        _selectedHour = (_selectedHour + 1) % 24;
        UpdateHourTexts();
    }

    private void DecrementHour()
    {
        _selectedHour = (_selectedHour + 23) % 24;
        UpdateHourTexts();
    }

    private void IncrementMinute()
    {
        _selectedMinute = (_selectedMinute + 1) % 60;
        UpdateMinuteTexts();
    }

    private void DecrementMinute()
    {
        _selectedMinute = (_selectedMinute + 59) % 60;
        UpdateMinuteTexts();
    }

    private void UpdateHourTexts()
    {
        if (_hourSelectedPart == null) return;

        _hourSelectedPart.Text = _selectedHour.ToString("D2");
        if (_hourAboveOnePart != null) _hourAboveOnePart.Text = ((_selectedHour + 23) % 24).ToString("D2");
        if (_hourAboveTwoPart != null) _hourAboveTwoPart.Text = ((_selectedHour + 22) % 24).ToString("D2");
        if (_hourBelowOnePart != null) _hourBelowOnePart.Text = ((_selectedHour + 1) % 24).ToString("D2");
        if (_hourBelowTwoPart != null) _hourBelowTwoPart.Text = ((_selectedHour + 2) % 24).ToString("D2");
    }

    private void UpdateMinuteTexts()
    {
        if (_minuteSelectedPart == null) return;

        _minuteSelectedPart.Text = _selectedMinute.ToString("D2");
        if (_minuteAboveOnePart != null) _minuteAboveOnePart.Text = ((_selectedMinute + 59) % 60).ToString("D2");
        if (_minuteAboveTwoPart != null) _minuteAboveTwoPart.Text = ((_selectedMinute + 58) % 60).ToString("D2");
        if (_minuteBelowOnePart != null) _minuteBelowOnePart.Text = ((_selectedMinute + 1) % 60).ToString("D2");
        if (_minuteBelowTwoPart != null) _minuteBelowTwoPart.Text = ((_selectedMinute + 2) % 60).ToString("D2");
    }
}
