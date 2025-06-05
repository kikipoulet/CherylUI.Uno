using System.Diagnostics;
using Cheryl.Uno.Helpers.Animations;
using Microsoft.UI.Xaml.Markup;

namespace Cheryl.Uno.Controls;

 [ContentProperty(Name = nameof(Content))]
    public sealed class InteractiveContainer : Control
    {
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(object), typeof(InteractiveContainer), new PropertyMetadata(null));

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
        
        
        private ContentPresenter DialogPresenter;
        public ContentPresenter BottomPresenter;
        private ContentPresenter MainContent;
        private Border BorderDialog;
        private Border BorderBottom;
        private Grid BlackPanel;

        public static InteractiveContainer Instance;
        
        public InteractiveContainer()
        {
            this.DefaultStyleKey = typeof(InteractiveContainer);
            
            Instance = this;
        }
        
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DialogPresenter = GetTemplateChild("DialogContent") as ContentPresenter;
            BorderDialog = GetTemplateChild("DialogBorder") as Border;
            BottomPresenter = GetTemplateChild("BottomContent") as ContentPresenter;
            BorderBottom = GetTemplateChild("BottomBorder") as Border;
            BlackPanel = GetTemplateChild("BlackPanel") as Grid;
            MainContent = GetTemplateChild("MainContent") as ContentPresenter;

            BlackPanel.PointerPressed += (sender, args) =>
            {
                CloseBottomSheet();
               
            };
        }

        public static void ShowDialog(Control control)
        {
            Instance.BorderDialog.AnimateDouble("Opacity", 0,1, 500 );
            Instance.BorderDialog.IsHitTestVisible = true;
            Instance.DialogPresenter.Content = control;
            
            Instance.MainContent.IsHitTestVisible = false;
            Instance.BlackPanel.AnimateDouble("Opacity", 0,0.3, 400 );
        }
        
        public static void CloseDialog()
        {
            Instance.BorderDialog.AnimateDouble("Opacity", 1,0, 500 );
            Instance.BorderDialog.IsHitTestVisible = false;

            
            Instance.MainContent.IsHitTestVisible = true;
            Instance.BlackPanel.AnimateDouble("Opacity", 0.3,0, 400 );
        }
        
        private static TaskCompletionSource<object> _bottomSheetTcs = new TaskCompletionSource<object>();
    
        public static async Task<object> ShowBottomSheet(UIElement control)
        {
            _bottomSheetTcs = new TaskCompletionSource<object>();

            Instance.BottomPresenter.Content = control;
            control.UpdateLayout();

            Instance.BorderBottom.IsHitTestVisible = true;
            Instance.BorderBottom.AnimateDouble("Opacity", 0, 1, 500);
            Instance.BorderBottom.AnimateTranslation("Y", control.ActualSize.Y + 30, 0, 500);

            Instance.MainContent.AnimateScale(1, 0.95, 300);
            Instance.MainContent.IsHitTestVisible = false;

            Instance.BlackPanel.AnimateDouble("Opacity", 0, 0.5, 300);
            Instance.BlackPanel.IsHitTestVisible = true;

            return await _bottomSheetTcs.Task;
        }
        
        
        public static void CloseBottomSheet(object result = null)
        {
            Instance.BorderBottom.AnimateDouble("Opacity", 1, 0.5, 500);
            Instance.BorderBottom.AnimateTranslation("Y", 0, Instance.BorderBottom.ActualHeight, 500);
            Instance.BorderBottom.IsHitTestVisible = false;

            Instance.MainContent.AnimateScale(0.95, 1, 400);
            Instance.MainContent.IsHitTestVisible = true;

            Instance.BlackPanel.AnimateDouble("Opacity", 0.5, 0, 400);
            Instance.BlackPanel.IsHitTestVisible = false;

            _bottomSheetTcs?.TrySetResult(result);
            _bottomSheetTcs = null;
        }
  

        
    }
