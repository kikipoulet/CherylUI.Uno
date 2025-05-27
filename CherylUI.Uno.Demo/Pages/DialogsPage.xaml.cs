using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Cheryl.Uno.Controls;
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
public sealed partial class DialogsPage : Page
{
    public DialogsPage()
    {
        this.InitializeComponent();
    }

    private void showbottom(object sender, RoutedEventArgs e)
    {
        
        InteractiveContainer.ShowBottomSheet(new ConfirmationControl());
    }

    private void showdialog(object sender, RoutedEventArgs e)
    {
        InteractiveContainer.ShowDialog(new ConfirmationControl());
    }
}

