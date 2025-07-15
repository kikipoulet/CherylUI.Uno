using Windows.UI.ViewManagement;
using Microsoft.UI.Xaml.Input;
using Uno.Toolkit.UI;

namespace Cheryl.Uno.Controls.Pickers.MobileTextBox;


public class MobileTextBox : Control
{
    private TextBlock? _textPart;

    public MobileTextBox()
    {
        DefaultStyleKey = typeof(MobileTextBox);
        Tapped += OnControlTapped;
        UpdateDisplayedText();
    }

    #region Dependency Properties

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(MobileTextBox), new PropertyMetadata(null, OnTextChanged));

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((MobileTextBox)d).UpdateDisplayedText();
    }

    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register(nameof(Header), typeof(object), typeof(MobileTextBox), new PropertyMetadata(null));

    public object? Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty PlaceholderTextProperty =
        DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(MobileTextBox), new PropertyMetadata(string.Empty));

    public string PlaceholderText
    {
        get => (string)GetValue(PlaceholderTextProperty);
        set => SetValue(PlaceholderTextProperty, value);
    }

    public static readonly DependencyProperty PopupTitleProperty =
        DependencyProperty.Register(nameof(PopupTitle), typeof(string), typeof(MobileTextBox), new PropertyMetadata(string.Empty));

    public string PopupTitle
    {
        get => (string)GetValue(PopupTitleProperty);
        set => SetValue(PopupTitleProperty, value);
    }

    #endregion

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _textPart = GetTemplateChild("TextPart") as TextBlock;
        UpdateDisplayedText();
    }

    private async void OnControlTapped(object sender, TappedRoutedEventArgs e)
    {
        InputPane.GetForCurrentView().TryHide();
        var panel = new MobileTextBoxDialog
        {
            InitialText = Text,
            Title = PopupTitle
        };

        var result = await InteractiveContainer.ShowBottomDialog(panel);

        if (result is string text)
        {
            Text = text;
        }
    }

    private void UpdateDisplayedText()
    {
        if (_textPart == null) return;

        if (!string.IsNullOrEmpty(Text))
        {
            _textPart.Text = Text;
            _textPart.Opacity = 1.0;
        }
        else
        {
            _textPart.Text = PlaceholderText;
            _textPart.Opacity = 0.6;
        }
    }
}

public class MobileTextBoxDialog : Control
{
    private TextBox? _textBoxPart;
    private Button? _confirmButtonPart;

    public MobileTextBoxDialog()
    {
        DefaultStyleKey = typeof(MobileTextBoxDialog);
    }

    #region Dependency Properties

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(MobileTextBoxDialog), new PropertyMetadata(string.Empty));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty InitialTextProperty =
        DependencyProperty.Register(nameof(InitialText), typeof(string), typeof(MobileTextBoxDialog), new PropertyMetadata(string.Empty));

    public string InitialText
    {
        get => (string)GetValue(InitialTextProperty);
        set => SetValue(InitialTextProperty, value);
    }

    #endregion

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _textBoxPart = GetTemplateChild("TextBoxPart") as TextBox;
        _confirmButtonPart = GetTemplateChild("ConfirmButtonPart") as Button;

      //  if (_textBoxPart != null)
      //      _textBoxPart.Text = InitialText;

        if (_confirmButtonPart != null)
            _confirmButtonPart.Click += OnConfirmClick;

       // _textBoxPart.Focus(FocusState.Programmatic);
    }

    private void OnConfirmClick(object sender, RoutedEventArgs e)
    {
        var text = _textBoxPart?.Text ?? string.Empty;
        InteractiveContainer.CloseBottomDialog(text);
        InputPane.GetForCurrentView().TryHide();
    }
}
