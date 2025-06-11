using System.Collections;
using System.Collections.ObjectModel;
using Cheryl.Uno.Helpers.Animations;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;
using Uno.UI.Extensions;

namespace Cheryl.Uno.Controls;

    [ContentProperty(Name = "Items")] // Permet de définir les BottomTabItem directement en XAML
public sealed class BottomTabControl : Control
{
    private ListView _tabHeaderListView;
    private Border _baseborder;
    private ContentPresenter _contentHostPresenter;
    private ScrollViewer _currentScrollViewer; // Référence au ScrollViewer actuel
    private double _lastVerticalOffset; // Dernier offset pour déterminer la direction du défilement

    public BottomTabControl()
    {
        this.DefaultStyleKey = typeof(BottomTabControl);
        Items = new ObservableCollection<BottomTabItem>();
    }

    public ObservableCollection<BottomTabItem> Items
    {
        get { return (ObservableCollection<BottomTabItem>)GetValue(ItemsProperty); }
        private set { SetValue(ItemsProperty, value); }
    }
    public static readonly DependencyProperty ItemsProperty =
        DependencyProperty.Register(nameof(Items), typeof(ObservableCollection<BottomTabItem>), typeof(BottomTabControl), new PropertyMetadata(null, OnItemsChanged));

    private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (BottomTabControl)d;
        if (control._tabHeaderListView != null && e.NewValue is ObservableCollection<BottomTabItem> newItems)
        {
            control._tabHeaderListView.ItemsSource = newItems;
        }

        if (e.NewValue is ObservableCollection<BottomTabItem> currentItems && currentItems.Any())
        {
            if (control.SelectedItem == null || !currentItems.Contains(control.SelectedItem))
            {
                control.SelectedItem = currentItems.FirstOrDefault();
            }
        }
        else if (e.NewValue == null)
        {
            control.SelectedItem = null;
        }
    }

    public BottomTabItem SelectedItem
    {
        get { return (BottomTabItem)GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }
    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register(nameof(SelectedItem), typeof(BottomTabItem), typeof(BottomTabControl), new PropertyMetadata(null, OnSelectedItemChanged));

    private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (BottomTabControl)d;
        control.UpdateContent();

        var oldItem = e.OldValue as BottomTabItem;
        var newItem = e.NewValue as BottomTabItem;

        List<object> removedItems = new List<object>();
        if (oldItem != null) removedItems.Add(oldItem);

        List<object> addedItems = new List<object>();
        if (newItem != null) addedItems.Add(newItem);

        control.InternalSelectionChanged?.Invoke(control, new SelectionChangedEventArgs(removedItems, addedItems));
    }

    public event SelectionChangedEventHandler InternalSelectionChanged;

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _tabHeaderListView = GetTemplateChild("TabHeaderListView") as ListView;
        _contentHostPresenter = GetTemplateChild("ContentHostPresenter") as ContentPresenter;
        _baseborder = GetTemplateChild("BaseBorder") as Border;

        if (SelectedItem == null && Items != null && Items.Any())
        {
            SelectedItem = Items.FirstOrDefault();
        }
        UpdateContent();
    }

    private void UpdateContent()
    {
        // Nettoyer l'ancien gestionnaire d'événements
        if (_currentScrollViewer != null)
        {
            _currentScrollViewer.ViewChanged -= OnScrollViewerViewChanged;
            _currentScrollViewer = null;
        }

        if (_contentHostPresenter != null)
        {
            if (SelectedItem != null)
            {
                _contentHostPresenter.Content = SelectedItem.PageContent;

                // Chercher un ScrollViewer dans le contenu
                _currentScrollViewer = FindScrollViewer(SelectedItem.PageContent);
                if (_currentScrollViewer != null)
                {
                    _lastVerticalOffset = _currentScrollViewer.VerticalOffset;
                    _currentScrollViewer.ViewChanged += OnScrollViewerViewChanged;
                }
            }
            else
            {
                _contentHostPresenter.Content = null;
            }
        }

        // S'assurer que le ListView est visible au départ
        if (_tabHeaderListView != null)
        {
            _tabHeaderListView.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
        }
    }

    private ScrollViewer FindScrollViewer(object content)
    {
        if (content is ScrollViewer scrollViewer)
        {
            return scrollViewer;
        }
        else if (content is FrameworkElement element)
        {
            // Parcourir l'arbre visuel pour trouver un ScrollViewer
            for (int i = 0; i < Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = Microsoft.UI.Xaml.Media.VisualTreeHelper.GetChild(element, i);
                var result = FindScrollViewer(child);
                if (result != null)
                {
                    return result;
                }
            }
        }
        return null;
    }

    private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
    {
        if (_currentScrollViewer == null || _tabHeaderListView == null)
            return;

        double currentOffset = _currentScrollViewer.VerticalOffset;

        if (currentOffset == _lastVerticalOffset)
            return;
        
        bool isScrollingDown = currentOffset > _lastVerticalOffset ;

       

        // Mettre à jour la visibilité du ListView
        if (isScrollingDown)
            HideAllLabels();
        else 
            ShowAllLabels();

        _lastVerticalOffset = currentOffset;
    }

    private bool IsLabelShow = true;
    
    private void HideAllLabels()
    {
        if (IsLabelShow)
        {
            IsLabelShow = false;
            _baseborder.AnimateTranslation("Y", 0, 15);
            
            var items =  _tabHeaderListView.Items;
            foreach (BottomTabItem item in items)
            {
           
            
                if(item._TextBlockLabel.Opacity == 1)
                    item._TextBlockLabel.AnimateDouble("Opacity", 1, 0);
            }
            
        }
    }

    private void ShowAllLabels()
    {

        if (!IsLabelShow)
        {
            IsLabelShow = true;
            _baseborder.AnimateTranslation("Y", 15, 0);
            
            var items =  _tabHeaderListView.Items;
            foreach (BottomTabItem item in items)
            {
                if(item._TextBlockLabel.Opacity == 0)
                    item._TextBlockLabel.AnimateDouble("Opacity", 0, 1);
                //  item._TextBlockLabel.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
            }
            
        }
    }
}

    [ContentProperty(Name = "PageContent")] // Permet de mettre le contenu de la page directement en XAML
    public sealed class BottomTabItem : Control
    {
        public BottomTabItem()
        {
            this.DefaultStyleKey = typeof(BottomTabItem);
        }

        public TextBlock _TextBlockLabel;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _TextBlockLabel = GetTemplateChild("TL") as TextBlock;
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(BottomTabItem), new PropertyMetadata("Item"));

        public string IconGlyph
        {
            get { return (string)GetValue(IconGlyphProperty); }
            set { SetValue(IconGlyphProperty, value); }
        }
        public static readonly DependencyProperty IconGlyphProperty =
            DependencyProperty.Register(nameof(IconGlyph), typeof(string), typeof(BottomTabItem), new PropertyMetadata(""));

        // Le contenu à afficher dans la zone principale du BottomTabControl lorsque cet onglet est sélectionné.
        public object PageContent
        {
            get { return (object)GetValue(PageContentProperty); }
            set { SetValue(PageContentProperty, value); }
        }
        public static readonly DependencyProperty PageContentProperty =
            DependencyProperty.Register(nameof(PageContent), typeof(object), typeof(BottomTabItem), new PropertyMetadata(null));
    }
