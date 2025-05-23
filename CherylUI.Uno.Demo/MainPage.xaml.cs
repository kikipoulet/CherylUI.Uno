
using Cheryl.Uno.Controls;
using CherylUI.Uno.Demo.Pages;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Input;
using GlobalStaticResources = Uno.UI.GlobalStaticResources;

namespace CherylUI.Uno.Demo;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        
    
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
}
