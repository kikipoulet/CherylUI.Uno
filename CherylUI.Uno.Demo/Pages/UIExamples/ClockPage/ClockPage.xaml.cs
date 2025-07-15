using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CherylUI.Uno.Demo.Pages.UIExamples.ClockPage;


public class Alarm
{
    public TimeSpan Time { get; set; }
    public bool Everyday { get; set; }
    public string Note { get; set; }
    public bool IsOn { get; set; }
}


public sealed partial class ClockPage : Page
{
    public ClockPage()
    {
        this.InitializeComponent();
        
    }
    
    public ObservableCollection<Alarm> Alarms { get; } = new ObservableCollection<Alarm>()
    {
        new Alarm()
        {
            Time = new TimeSpan(0,10,10,0),
            Everyday = true,
            IsOn = false,
            Note = "Alarm"
        },
        new Alarm()
        {
            Time = new TimeSpan(0,10,30,0),
            Everyday = true,
            IsOn = true,
            Note = "Work Alarm"
        },
        new Alarm()
        {
            Time = new TimeSpan(0,15,30,0),
            Everyday = true,
            IsOn = false,
            Note = "Medicine Alarm"
        }
    };

    private void UIElement_OnPointerReleased(object sender, PointerRoutedEventArgs e)
    {
        
        
        InteractiveContainer.ShowBottomSheet(new AlarmEditControl());
    }
}


