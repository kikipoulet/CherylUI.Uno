using System.Collections;

namespace Cheryl.Uno.Controls.Pickers.MobilePicker;

public class MobilePickerPopup : Control
{
    public static readonly object Dismissed = new object(); 

    public ListView ItemsPart;
    
    public MobilePickerPopup()
    {
        this.DefaultStyleKey = typeof(MobilePickerPopup);
    }

    public event Action TemplateApplied;
    
    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        ItemsPart =  (GetTemplateChild("ItemsListViewPart") as ListView);
        ItemsPart.SelectionChanged += ((sender, args) => InteractiveContainer.CloseBottomSheet());
        TemplateApplied?.Invoke();
    }

    #region Properties

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MobilePickerPopup),
            new PropertyMetadata(null));

    public IEnumerable ItemsSource
    {
        get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly DependencyProperty CurrentSelectedItemProperty =
        DependencyProperty.Register("CurrentSelectedItem", typeof(object), typeof(MobilePickerPopup),
            new PropertyMetadata(null));

    public object CurrentSelectedItem
    {
        get { return (object)GetValue(CurrentSelectedItemProperty); }
        set { SetValue(CurrentSelectedItemProperty, value); }
    }

    public static readonly DependencyProperty DisplayMemberPathProperty =
        DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(MobilePickerPopup),
            new PropertyMetadata(string.Empty));

    public string DisplayMemberPath
    {
        get { return (string)GetValue(DisplayMemberPathProperty); }
        set { SetValue(DisplayMemberPathProperty, value); }
    }

    public static readonly DependencyProperty PopupTitleProperty =
        DependencyProperty.Register("PopupTitle", typeof(string), typeof(MobilePickerPopup),
            new PropertyMetadata("Sélectionnez une option"));

    public string PopupTitle
    {
        get { return (string)GetValue(PopupTitleProperty); }
        set { SetValue(PopupTitleProperty, value); }
    }

    #endregion

}
