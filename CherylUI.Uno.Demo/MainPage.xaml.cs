
using Cheryl.Uno.Controls;
using CherylUI.Uno.Demo.Pages;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Input;
using GlobalStaticResources = Uno.UI.GlobalStaticResources;

namespace CherylUI.Uno.Demo;

public sealed partial class MainPage : Page
{

    public static Frame GlobalContentFrame;
    public MainPage()
    {
        this.InitializeComponent();
        
        ContentFrame.Navigate(typeof(HomePage));
        GlobalContentFrame = ContentFrame;
    }

    private void ShosAThing(object sender, RoutedEventArgs e)
    {
        var b = new Button() { Content = "Button" };
        b.Click += (o, args) => InteractiveContainer.CloseDialog();
         
        InteractiveContainer.ShowDialog(b);
    }

    private void showbottom(object sender, RoutedEventArgs e)
    {
        var b = new Button() { Content = "Button", HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom} ;
        b.Click += (o, args) => InteractiveContainer.CloseBottomSheet();

        var g = new Grid();
        g.Children.Add(new TextBlock() { Text = "Hello from the Bottom Sheet !", FontFamily = (FontFamily)Application.Current.Resources["QuicksandBold"],FontSize = 18, FontWeight = FontWeights.DemiBold, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center , Margin = new Thickness(0,100)} );
         //g.Children.Add(b);
        InteractiveContainer.ShowBottomSheet(g);
        
    }

    private void UIElement_OnPointerPressed(object sender, PointerRoutedEventArgs e)
    {
        var b = new Button() { Content = "Button", HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom} ;
        b.Click += (o, args) => InteractiveContainer.CloseBottomSheet();

        var g = new Grid();
        g.Children.Add(new TextBlock() { Text = "Hello from the Bottom Sheet !", FontFamily = (FontFamily)Application.Current.Resources["QuicksandBold"],FontSize = 18, FontWeight = FontWeights.DemiBold, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center , Margin = new Thickness(0,100)} );
        //g.Children.Add(b);
        InteractiveContainer.ShowBottomSheet(new ConfirmationControl());
    }

    private void GoToSettings(object sender, PointerRoutedEventArgs e)
    {

        ContentFrame.Navigate(typeof(SettingsPage));
    }

    private void GoToButtons(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(ButtonsDemo));
        
    }

    private void gotosliders(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(Sliders));
    }

    private void gotohome(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(HomePage));
    }

    private void GoToToggles(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(TogglesPage));
    }
    
    private void GoToDialogs(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(DialogsPage));
    }
    
    private void GoToSquishy(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(SquishyBehaviorPage));
    }

    private void GoToEasing(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(CustomEasingPage));
    }

    private void GoToLayouts(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(LayoutsPage));
        
    }
    private void GoToOthers(object sender, PointerRoutedEventArgs e)
    {
        ContentFrame.Navigate(typeof(OthersPage));
        
    }
}
