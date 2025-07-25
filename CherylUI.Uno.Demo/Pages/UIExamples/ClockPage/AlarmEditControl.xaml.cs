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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CherylUI.Uno.Demo.Pages.UIExamples.ClockPage;

public sealed partial class AlarmEditControl : UserControl
{
    public AlarmEditControl()
    {
        this.InitializeComponent();
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        
       // MTP.AcceptButtonPart.Visibility = Visibility.Collapsed;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        
        InteractiveContainer.CloseBottomSheet();
    }
}

