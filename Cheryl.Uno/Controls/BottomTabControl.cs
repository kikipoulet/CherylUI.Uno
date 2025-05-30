using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Markup;

namespace Cheryl.Uno.Controls;

    [ContentProperty(Name = "Items")] // Permet de définir les BottomTabItem directement en XAML
    public sealed class BottomTabControl : Control
    {
        private ListView _tabHeaderListView;
        private ContentPresenter _contentHostPresenter;

        public BottomTabControl()
        {
            
            this.DefaultStyleKey = typeof(BottomTabControl);
            // Initialiser la collection Items pour que le XAML puisse y ajouter des éléments
            Items = new ObservableCollection<BottomTabItem>();
        }

        // Collection de BottomTabItem définis par l'utilisateur en XAML
        public ObservableCollection<BottomTabItem> Items
        {
            get { return (ObservableCollection<BottomTabItem>)GetValue(ItemsProperty); }
            private set { SetValue(ItemsProperty, value); } // Setter privé, initialisé dans le constructeur
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

            // Gérer la sélection initiale si nécessaire
            if (e.NewValue is ObservableCollection<BottomTabItem> currentItems && currentItems.Any())
            {
                if (control.SelectedItem == null || !currentItems.Contains(control.SelectedItem))
                {
                    control.SelectedItem = currentItems.FirstOrDefault();
                }
            }
            else if (e.NewValue == null) // Si la collection est vidée
            {
                control.SelectedItem = null;
            }
        }

        // Le BottomTabItem actuellement sélectionné
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

            // Déclencher l'événement SelectionChanged standard
            var oldItem = e.OldValue as BottomTabItem;
            var newItem = e.NewValue as BottomTabItem;

            List<object> removedItems = new List<object>();
            if (oldItem != null) removedItems.Add(oldItem);

            List<object> addedItems = new List<object>();
            if (newItem != null) addedItems.Add(newItem);
            
            control.InternalSelectionChanged?.Invoke(control, new SelectionChangedEventArgs(removedItems, addedItems));
        }

        // Renommé pour éviter conflit si l'utilisateur nomme son événement "SelectionChanged"
        public event SelectionChangedEventHandler InternalSelectionChanged;


        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _tabHeaderListView = GetTemplateChild("TabHeaderListView") as ListView;
            _contentHostPresenter = GetTemplateChild("ContentHostPresenter") as ContentPresenter;

            if (_tabHeaderListView != null)
            {
                // ItemsSource est lié aux 'Items' du contrôle dans le template XAML.
                // SelectedItem est également lié en TwoWay dans le template.
            }
            
            // Assurer que le contenu est affiché pour l'élément initialement sélectionné (si Items sont déjà là)
            if (SelectedItem == null && Items != null && Items.Any())
            {
                SelectedItem = Items.FirstOrDefault();
            }
            UpdateContent(); // Met à jour le contenu initial
        }
        
        private void UpdateContent()
        {
            if (_contentHostPresenter != null)
            {
                if (SelectedItem != null)
                {
                    // Affiche le PageContent du BottomTabItem sélectionné
                    _contentHostPresenter.Content = SelectedItem.PageContent;
                }
                else
                {
                    _contentHostPresenter.Content = null; // Vide le contenu si aucun onglet n'est sélectionné
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
