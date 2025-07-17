using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.ViewManagement;
using Cheryl.Uno.Helpers.Animations;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CherylUI.Uno.Demo.Pages.UIExamples.Browser;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class WebBrowser : Page
{
    public WebBrowser()
    {
        this.InitializeComponent(); 
        Start();
    }

    public async void Start()
    {
        await MyWebView.EnsureCoreWebView2Async();
    
    }



    
    private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            
            e.Handled = true;

                SA.AnimateMorphingDisappearing(0, 30, 400, 0.5);
                SA.IsHitTestVisible = false;
                MyWebView.CoreWebView2.Navigate("https://www." + TB.Text);
                MyWebView.AnimateMorphingAppearing(0, 0, 1500, 0.5);
                GB.AnimateMorphingAppearing(0, 50, 1500, 1);
            

            InputPane.GetForCurrentView().TryHide();
        }
    }

    private void TBB_OnKeyDownbottom(object sender, KeyRoutedEventArgs e)
    {
        MyWebView.CoreWebView2.Navigate("https://www." + TBB.Text);
        
        InputPane.GetForCurrentView().TryHide();
    }
}

