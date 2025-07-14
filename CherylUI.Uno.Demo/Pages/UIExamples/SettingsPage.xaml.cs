using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Cheryl.Uno.Controls;
using Cheryl.Uno.Helpers.Animations;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CherylUI.Uno.Demo.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsPage : Page
{
    public SettingsPage()
    {
        this.InitializeComponent();
       
    }
    public TimeSpan SelectedTime { get; set; } = new TimeSpan(12, 0, 0);
    public ObservableCollection<string> Fonts { get; set; }=  new ObservableCollection<string>(){"Thin", "Regular",  "Bold"};
    public string SelectedFont { get; set; }=  "Bold";
    public DateTimeOffset SelectedDate { get; set; }=new DateTime(2000,1,1);
    
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


    private void DarkModeToggles(object sender, RoutedEventArgs e)
    {
        SystemThemeHelper.SetApplicationTheme(this.XamlRoot,  App.Current.RequestedTheme == ApplicationTheme.Dark
            ? ElementTheme.Light
            : ElementTheme.Dark);
           
    }
}

