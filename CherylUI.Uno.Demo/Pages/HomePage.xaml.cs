using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using CherylUI.Uno.Demo.Pages.UIExamples.ChatPage;

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
public sealed partial class HomePage : Page
{
    public ObservableCollection<string> Fruits { get; set; }=  new ObservableCollection<string>(){"Apple", "Banana", "Pear", "Orange"};
    public string SelectedFruit { get; set; }=  "Pear";
    
    public HomePage()
    {
        this.InitializeComponent();
    }
    

    private void GoToSettings(object sender, PointerRoutedEventArgs e)
    {
        MainPage.GlobalContentFrame.Navigate(typeof(SettingsPage));
    }

    private void GoToChat(object sender, PointerRoutedEventArgs e)
    {
        MainPage.GlobalContentFrame.Navigate(typeof(ChatPage));
    }
}

