﻿using System;
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

namespace CherylUI.Uno.Demo;

public sealed partial class ConfirmationControl : UserControl
{
    public ConfirmationControl()
    {
        this.InitializeComponent();
    }

    private void close(object sender, RoutedEventArgs e)
    {
        InteractiveContainer.CloseDialog();
    }
}

