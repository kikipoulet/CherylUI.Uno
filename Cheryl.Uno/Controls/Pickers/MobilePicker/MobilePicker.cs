using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;

namespace Cheryl.Uno.Controls.Pickers.MobilePicker;

 [ContentProperty(Name = "ItemsSource")]
    public class MobilePicker : Control, INotifyPropertyChanged
    {
        private TextBlock _selectedItemTextPart;

        public MobilePicker()
        {
            this.DefaultStyleKey = typeof(MobilePicker);
            this.Tapped += MobilePicker_Tapped;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MobilePicker), new PropertyMetadata(null));
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(MobilePicker), new PropertyMetadata(null, OnSelectedItemChanged));
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (MobilePicker)d;
            picker.UpdateDisplayedText();
            picker.OnPropertyChanged(nameof(SelectedItem));
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(MobilePicker), new PropertyMetadata(string.Empty, OnDisplayMemberPathChanged));
        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }
        private static void OnDisplayMemberPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MobilePicker)d).UpdateDisplayedText();
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(MobilePicker), new PropertyMetadata("Sélectionner..."));
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }
        
        
        public static readonly DependencyProperty PopupTitleProperty =
            DependencyProperty.Register("PopupTitle", typeof(string), typeof(MobilePicker), new PropertyMetadata("Sélectionnez une option"));
        public string PopupTitle
        {
            get { return (string)GetValue(PopupTitleProperty); }
            set { SetValue(PopupTitleProperty, value); }
        }

        #endregion

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _selectedItemTextPart = GetTemplateChild("SelectedItemTextPart") as TextBlock;
            UpdateDisplayedText();
        }

        private async void MobilePicker_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ItemsSource == null) return;

            var popupContent = new MobilePickerPopup
            {
                ItemsSource = this.ItemsSource, 
                DisplayMemberPath = this.DisplayMemberPath, CurrentSelectedItem = this.SelectedItem,
                PopupTitle = this.PopupTitle, 
               
            };
            
            var binding = new Binding
            {
                Source = popupContent,
                Path = new PropertyPath("CurrentSelectedItem"),
                Mode = BindingMode.TwoWay
            };

            this.SetBinding(MobilePicker.SelectedItemProperty, binding);
            
            
            // Little trick to be sure the itemlistview has calculated its height before animating it
            
            InteractiveContainer.Instance.BottomPresenter.Content = popupContent;

            popupContent.TemplateApplied += () =>
            {
                popupContent.ItemsPart.SizeChanged += (async (sender, args) =>
                {
                    if(((Control)sender).ActualHeight <10)
                        return;
                    
                    object result = await InteractiveContainer.ShowBottomSheet(popupContent);

                    if (result != null && result != MobilePickerPopup.Dismissed)
                    {
                        SelectedItem = result;
                    }
                });
            };
            
          
        }

        private void UpdateDisplayedText()
        {
            if (_selectedItemTextPart != null)
            {
                if (SelectedItem != null)
                {
                    string displayText;
                    if (!string.IsNullOrEmpty(DisplayMemberPath))
                    {
                        try
                        {
                            var property = SelectedItem.GetType().GetProperty(DisplayMemberPath);
                            displayText = property?.GetValue(SelectedItem)?.ToString() ?? SelectedItem.ToString();
                        }
                        catch
                        {
                            displayText = SelectedItem.ToString();
                        }
                    }
                    else
                    {
                        displayText = SelectedItem.ToString();
                    }
                    _selectedItemTextPart.Text = displayText;
                    _selectedItemTextPart.Opacity = 1.0; 
                    ToolTipService.SetToolTip(_selectedItemTextPart, displayText); 
                }
                else
                {
                    _selectedItemTextPart.Text = PlaceholderText;
                    _selectedItemTextPart.Opacity = 0.6;
                    ToolTipService.SetToolTip(_selectedItemTextPart, null);
                }
            }
        }
    }
