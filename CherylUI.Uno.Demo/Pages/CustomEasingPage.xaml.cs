using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Cheryl.Uno.Helpers.Animations;
using Cheryl.Uno.Helpers.Easings;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using SkiaSharp;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CherylUI.Uno.Demo.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CustomEasingPage : Page
{
    public ObservableCollection<ObservableValue> ObservableValues { get; set; } =
            new ObservableCollection<ObservableValue>();

        public CustomEasingPage()
        {
            this.InitializeComponent();

            var chart = MyChart;

            MyChart.Series = new ISeries[]
            {
                new LineSeries<ObservableValue>
                {
                    Values = ObservableValues,
                    Fill = null,
                    GeometrySize = 0,
                    LineSmoothness = 0.2
                }
            };

            chart.XAxes = new[]
            {
                new Axis
                {
                    Labels = new string[] { },
                    IsVisible = false
                }
            };

            chart.YAxes = new[]
            {
                new Axis
                {
                    Labels = new[] { "0", "1" },
                    LabelsPaint = new SolidColorPaint(SKColors.Gray, 2),
                    TextSize = 22,
                    SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                    {
                        StrokeThickness = 1,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };

            UpdateChart(new CherylEasing.CherylSpringEase());
        }

        private void ParametersChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            var newEasing = new CherylEasing.CherylSpringEase
            {
                Damping = (this.FindName("DampingBox") as Slider)?.Value ?? 10,
                Mass = (this.FindName("MassBox") as Slider)?.Value ?? 1,
                Stiffness = (this.FindName("StiffnessBox") as Slider)?.Value ?? 50
            };

            if (newEasing.Damping == 0 || newEasing.Mass == 0 || newEasing.Stiffness == 0)
                return;

            UpdateChart(newEasing);
          
        }

        private void UpdateChart(CherylEasing.CherylSpringEase easing)
        {
            if (ObservableValues.Count == 0)
            {
                double start = 0;
                while (start <= 1)
                {
                    // Map the normalized time (start) to the Ease method's parameters
                    double easedValue = easing.Ease(
                        currentTime: start,  // Normalized time (0 to 1)
                        startValue: 0.0,     // Starting at 0
                        finalValue: 1.0,     // Ending at 1
                        duration: 1.0        // Duration of 1 to match normalized time
                    );
                    ObservableValues.Add(new ObservableValue(easedValue));
                    start += 0.01;
                }
            }
            else
            {
                double start = 0;
                int i = 0;
                while (start <= 1)
                {
                    double easedValue = easing.Ease(
                        currentTime: start,
                        startValue: 0.0,
                        finalValue: 1.0,
                        duration: 1.0
                    );
                    ObservableValues[i].Value = easedValue;
                    start += 0.01;
                    i++;
                }
            }
            
            
        }

        private void SetBase(object sender, RoutedEventArgs e)
        {
            (this.FindName("MassBox") as Slider).Value = 1;
            (this.FindName("DampingBox") as Slider).Value = 10;
            (this.FindName("StiffnessBox") as Slider).Value = 50;
        }

        private void SetSmooth(object sender, RoutedEventArgs e)
        {
            (this.FindName("MassBox") as Slider).Value = 1;
            (this.FindName("DampingBox") as Slider).Value = 10;
            (this.FindName("StiffnessBox") as Slider).Value = 30;
        }


        private bool flag = false;
        private void EB_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
     
            if (flag)
            {
                EB.AnimateTranslation("X", 200, 0, 700, new CherylEasing.CherylSpringEase
                {
                    Damping = (this.FindName("DampingBox") as Slider)?.Value ?? 10,
                    Mass = (this.FindName("MassBox") as Slider)?.Value ?? 1,
                    Stiffness = (this.FindName("StiffnessBox") as Slider)?.Value ?? 50
                });
                
            }
            else
            {
                EB.AnimateTranslation("X", 0, 200, 700, new CherylEasing.CherylSpringEase
                {
                    Damping = (this.FindName("DampingBox") as Slider)?.Value ?? 10,
                    Mass = (this.FindName("MassBox") as Slider)?.Value ?? 1,
                    Stiffness = (this.FindName("StiffnessBox") as Slider)?.Value ?? 50
                });
               
            }
            
            flag = !flag;
        }
}

