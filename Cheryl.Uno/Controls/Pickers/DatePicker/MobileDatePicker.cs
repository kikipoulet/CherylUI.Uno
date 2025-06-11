using System.ComponentModel;
using System.Globalization;
using Windows.Globalization.DateTimeFormatting;
using Microsoft.UI.Xaml.Input;

namespace Cheryl.Uno.Controls.Pickers.DatePicker;


 public class MobileDatePicker : Control
    {
        private TextBlock _dateTextPart;
        private DateTimeFormatter _formatter;

        public MobileDatePicker()
        {
            this.DefaultStyleKey = typeof(MobileDatePicker);
            this.Tapped += OnControlTapped;
            // Par défaut, nous utilisons le format "shortdate"
            UpdateFormatter();
            UpdateDisplayedText();
        }
        
        
        

        #region Dependency Properties

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTimeOffset?), typeof(MobileDatePicker), new PropertyMetadata(null, OnDateChanged));

        public DateTimeOffset? Date
        {
            get { return (DateTimeOffset?)GetValue(DateProperty); }
            set { 
                SetValue(DateProperty, value); 
            }
        }

        private static void OnDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (MobileDatePicker)d;
            picker.UpdateDisplayedText();
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(MobileDatePicker), new PropertyMetadata(null));

        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(MobileDatePicker), new PropertyMetadata("jj/mm/aaaa"));

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty DateFormatProperty =
            DependencyProperty.Register("DateFormat", typeof(string), typeof(MobileDatePicker), new PropertyMetadata("shortdate", OnDateFormatChanged));
        
        public string DateFormat
        {
            get { return (string)GetValue(DateFormatProperty); }
            set { SetValue(DateFormatProperty, value); }
        }
        private static void OnDateFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (MobileDatePicker)d;
            picker.UpdateFormatter();
            picker.UpdateDisplayedText();
        }

        public static readonly DependencyProperty PopupTitleProperty =
            DependencyProperty.Register("PopupTitle", typeof(string), typeof(MobileDatePicker), new PropertyMetadata("Sélectionnez une date"));

        public string PopupTitle
        {
            get { return (string)GetValue(PopupTitleProperty); }
            set { SetValue(PopupTitleProperty, value); }
        }
        #endregion

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _dateTextPart = GetTemplateChild("DateTextPart") as TextBlock;
            UpdateDisplayedText();
        }

        private async void OnControlTapped(object sender, TappedRoutedEventArgs e)
        {
            var panel = new MobileDatePickerPanel
            {
                InitialDate = this.Date,
                Title = this.PopupTitle
            };

            var result = await InteractiveContainer.ShowBottomSheet(panel);

            if (result is DateTimeOffset newDate)
            {
                this.Date = newDate;
            }
            // Si result est null (annulation), on ne fait rien.
        }

        private void UpdateFormatter()
        {
            // DateFormat peut être un format standard ("longdate", "shortdate")
            // ou un pattern custom ("{month.full} {day.integer}, {year.full}")
            try
            {
                _formatter = new DateTimeFormatter(DateFormat);
            }
            catch(Exception)
            {
                // En cas de format invalide, on revient au format par défaut
                _formatter = new DateTimeFormatter("shortdate");
            }
        }

        private void UpdateDisplayedText()
        {
            if (_dateTextPart == null) return;

            if (Date.HasValue)
            {
                _dateTextPart.Text = _formatter.Format(Date.Value);
                _dateTextPart.Opacity = 1.0;
            }
            else
            {
                _dateTextPart.Text = PlaceholderText;
                _dateTextPart.Opacity = 0.6;
            }
        }
    }

    public class MobileDatePickerPanel : Control
    {
        private CalendarView _calendarViewPart;
        private Button _acceptButtonPart;
        private Button _dismissButtonPart;

        public MobileDatePickerPanel()
        {
            this.DefaultStyleKey = typeof(MobileDatePickerPanel);
            this.Loaded += OnPanelLoaded;
        }

        #region Dependency Properties
        
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MobileDatePickerPanel), new PropertyMetadata(""));
        
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty InitialDateProperty =
            DependencyProperty.Register("InitialDate", typeof(DateTimeOffset?), typeof(MobileDatePickerPanel), new PropertyMetadata(null));
        
        // La date initiale à afficher lors de l'ouverture
        public DateTimeOffset? InitialDate
        {
            get { return (DateTimeOffset?)GetValue(InitialDateProperty); }
            set { SetValue(InitialDateProperty, value); }
        }

        #endregion

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _calendarViewPart = GetTemplateChild("CalendarViewPart") as CalendarView;
            _acceptButtonPart = GetTemplateChild("AcceptButtonPart") as Button;
            _dismissButtonPart = GetTemplateChild("DismissButtonPart") as Button;

            if (_acceptButtonPart != null)
                _acceptButtonPart.Click += OnAcceptClick;
            
            if (_dismissButtonPart != null)
                _dismissButtonPart.Click += OnDismissClick;
        }

        private void OnPanelLoaded(object sender, RoutedEventArgs e)
        {
            if (_calendarViewPart != null && InitialDate.HasValue)
            {
                _calendarViewPart.SelectedDates.Clear();
                _calendarViewPart.SelectedDates.Add(InitialDate.Value);
                _calendarViewPart.SetDisplayDate(InitialDate.Value);
            }
        }
        
        private void OnAcceptClick(object sender, RoutedEventArgs e)
        {
            DateTimeOffset? selectedDate = null;
            if (_calendarViewPart?.SelectedDates.Any() == true)
            {
                selectedDate = _calendarViewPart.SelectedDates.FirstOrDefault();
            }
            // Ferme le bottom sheet en renvoyant la date sélectionnée
            InteractiveContainer.CloseBottomSheet(selectedDate);
        }

        private void OnDismissClick(object sender, RoutedEventArgs e)
        {
            // Ferme le bottom sheet en renvoyant null pour indiquer l'annulation
            InteractiveContainer.CloseBottomSheet(null);
        }
    }

