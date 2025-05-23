using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.Foundation;
using Microsoft.UI.Composition; // Added for ElementCompositionPreview
using Microsoft.UI.Xaml.Hosting; 


namespace Cheryl.Uno.Helpers.Animations;

 public class SquishyDragExtensions
    {
        private class State
        {
            public Point? StartPoint;
            public CompositeTransform Transform = new CompositeTransform();
            public double Intensity = 1;
            public double SquishDepth = 1;
            public bool EnableTilt = true;
            public bool EnableX = true;
            public bool EnableY = true;
            public bool ScaleByXY = true;
        }

        private static readonly DependencyProperty StateProperty = DependencyProperty.RegisterAttached(
            "State",
            typeof(State),
            typeof(SquishyDragExtensions),
            new PropertyMetadata(null));

        public static readonly DependencyProperty EnableXProperty = DependencyProperty.RegisterAttached(
            "EnableX",
            typeof(bool),
            typeof(SquishyDragExtensions),
            new PropertyMetadata(true, OnEnableXChanged));

        public static bool GetEnableX(UIElement element)
        {
            return (bool)element.GetValue(EnableXProperty);
        }

        public static void SetEnableX(UIElement element, bool value)
        {
            element.SetValue(EnableXProperty, value);
        }

        private static void OnEnableXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                var state = (State)element.GetValue(StateProperty);
                if (state != null)
                {
                    state.EnableX = (bool)e.NewValue;
                }
            }
        }

        public static readonly DependencyProperty EnableYProperty = DependencyProperty.RegisterAttached(
            "EnableY",
            typeof(bool),
            typeof(SquishyDragExtensions),
            new PropertyMetadata(true, OnEnableYChanged));

        public static bool GetEnableY(UIElement element)
        {
            return (bool)element.GetValue(EnableYProperty);
        }

        public static void SetEnableY(UIElement element, bool value)
        {
            element.SetValue(EnableYProperty, value);
        }

        private static void OnEnableYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                var state = (State)element.GetValue(StateProperty);
                if (state != null)
                {
                    state.EnableY = (bool)e.NewValue;
                }
            }
        }

        public static readonly DependencyProperty EnableTiltProperty = DependencyProperty.RegisterAttached(
            "EnableTilt",
            typeof(bool),
            typeof(SquishyDragExtensions),
            new PropertyMetadata(true, OnEnableTiltChanged));

        public static bool GetEnableTilt(UIElement element)
        {
            return (bool)element.GetValue(EnableTiltProperty);
        }

        public static void SetEnableTilt(UIElement element, bool value)
        {
            element.SetValue(EnableTiltProperty, value);
        }

        private static void OnEnableTiltChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                var state = (State)element.GetValue(StateProperty);
                if (state != null)
                {
                    state.EnableTilt = (bool)e.NewValue;
                }
            }
        }

        public static readonly DependencyProperty ScaleByXYAxisProperty = DependencyProperty.RegisterAttached(
            "ScaleByXYAxis",
            typeof(bool),
            typeof(SquishyDragExtensions),
            new PropertyMetadata(false, OnScaleByXYAxisChanged));

        public static bool GetScaleByXYAxis(UIElement element)
        {
            return (bool)element.GetValue(ScaleByXYAxisProperty);
        }

        public static void SetScaleByXYAxis(UIElement element, bool value)
        {
            element.SetValue(ScaleByXYAxisProperty, value);
        }

        private static void OnScaleByXYAxisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                var state = (State)element.GetValue(StateProperty);
                if (state != null)
                {
                    state.ScaleByXY = (bool)e.NewValue;
                }
            }
        }

        public static readonly DependencyProperty IntensityProperty = DependencyProperty.RegisterAttached(
            "Intensity",
            typeof(double),
            typeof(SquishyDragExtensions),
            new PropertyMetadata(1.0, OnIntensityChanged));

        public static double GetIntensity(UIElement element)
        {
            return (double)element.GetValue(IntensityProperty);
        }

        public static void SetIntensity(UIElement element, double value)
        {
            element.SetValue(IntensityProperty, value);
        }

        private static void OnIntensityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                var state = (State)element.GetValue(StateProperty);
                if (state != null)
                {
                    state.Intensity = (double)e.NewValue;
                }
            }
        }

        public static readonly DependencyProperty SquishDepthProperty = DependencyProperty.RegisterAttached(
            "SquishDepth",
            typeof(double),
            typeof(SquishyDragExtensions),
            new PropertyMetadata(1.0, OnSquishDepthChanged));

        public static double GetSquishDepth(UIElement element)
        {
            return (double)element.GetValue(SquishDepthProperty);
        }

        public static void SetSquishDepth(UIElement element, double value)
        {
            element.SetValue(SquishDepthProperty, value);
        }

        private static void OnSquishDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                var state = (State)element.GetValue(StateProperty);
                if (state != null)
                {
                    state.SquishDepth = (double)e.NewValue;
                }
            }
        }

        public static readonly DependencyProperty EnableProperty = DependencyProperty.RegisterAttached(
            "Enable",
            typeof(bool),
            typeof(SquishyDragExtensions),
            new PropertyMetadata(true, OnEnableChanged));

        public static bool GetEnable(UIElement element)
        {
            return (bool)element.GetValue(EnableProperty);
        }

        public static void SetEnable(UIElement element, bool value)
        {
            element.SetValue(EnableProperty, value);
        }

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UIElement element) return;

            if ((bool)e.NewValue)
            {
                var state = new State();
                element.SetValue(StateProperty, state);

                element.PointerPressed += OnPointerPressed;
                element.PointerMoved += OnPointerMoved;
                element.PointerReleased += OnPointerReleased;

                element.RenderTransform = state.Transform;
            }
            else
            {
                element.PointerPressed -= OnPointerPressed;
                element.PointerMoved -= OnPointerMoved;
                element.PointerReleased -= OnPointerReleased;

                element.RenderTransform = null;
                element.SetValue(StateProperty, null);
            }
        }

        private static void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (sender is not UIElement element) return;
            var state = (State)element.GetValue(StateProperty);
            if (state == null) return;

            var point = e.GetCurrentPoint(element);
            state.StartPoint = point.Position;
        }

        private static void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (sender is not UIElement element) return;
            var state = (State)element.GetValue(StateProperty);
            if (state?.StartPoint == null) return;

            var current = e.GetCurrentPoint(element).Position;
            var dx = current.X - state.StartPoint.Value.X;
            var dy = current.Y - state.StartPoint.Value.Y;

            double skewFactor = 0.02 * state.Intensity;
            double translateFactor = 0.05 * state.Intensity;
            double scaleFactor = 0.0001 * state.Intensity;

            if (state.EnableTilt)
            {
                var xAngle = dx * skewFactor;
                if (xAngle > 3 * state.SquishDepth)
                    xAngle = 3 * state.SquishDepth;
                if (xAngle < -3 * state.SquishDepth)
                    xAngle = -3 * state.SquishDepth;

                var yAngle = -dy * skewFactor;
                if (yAngle > 3 * state.SquishDepth)
                    yAngle = 3 * state.SquishDepth;
                if (yAngle < -3 * state.SquishDepth)
                    yAngle = -3 * state.SquishDepth;

                state.Transform.SkewX = (yAngle < 0 ? xAngle : -xAngle) * Math.Abs(yAngle) * 0.3;
                state.Transform.SkewY = (xAngle < 0 ? yAngle : -yAngle) * Math.Abs(xAngle) * 0.3;
            }

            if (state.EnableX)
                state.Transform.TranslateX = dx * translateFactor;
            if (state.EnableY)
                state.Transform.TranslateY = dy * translateFactor;

            if (state.ScaleByXY)
            {
                if (state.EnableX)
                    state.Transform.ScaleX = 1 - dx * (scaleFactor * 1.5);
                if (state.EnableY)
                    state.Transform.ScaleY = 1 - dy * (scaleFactor * 1.5);
            }
            else
            {
                if (state.EnableX)
                    state.Transform.ScaleX = 1 - Math.Abs(dx) * scaleFactor;
                if (state.EnableY)
                    state.Transform.ScaleY = 1 - Math.Abs(dy) * scaleFactor;
            }
        }

        private static void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (sender is not UIElement element) return;
            var state = (State)element.GetValue(StateProperty);
            if (state == null) return;

            state.StartPoint = null;

            // Get the Visual for the element to animate its properties
            var visual = ElementCompositionPreview.GetElementVisual(element);
            var compositor = visual.Compositor;

            // Animate SkewX back to 0
            var skewXAnimation = compositor.CreateSpringScalarAnimation();
            skewXAnimation.InitialValue = (float)state.Transform.SkewX;
            skewXAnimation.FinalValue = 0f;
            skewXAnimation.DampingRatio = 0.7f;
            skewXAnimation.Period = TimeSpan.FromMilliseconds(700);
            visual.StartAnimation("Transform.SkewX", skewXAnimation);

            // Animate SkewY back to 0
            var skewYAnimation = compositor.CreateSpringScalarAnimation();
            skewYAnimation.InitialValue = (float)state.Transform.SkewY;
            skewYAnimation.FinalValue = 0f;
            skewYAnimation.DampingRatio = 0.7f;
            skewYAnimation.Period = TimeSpan.FromMilliseconds(700);
            visual.StartAnimation("Transform.SkewY", skewYAnimation);

            // Animate TranslateX back to 0
            var translateXAnimation = compositor.CreateSpringScalarAnimation();
            translateXAnimation.InitialValue = (float)state.Transform.TranslateX;
            translateXAnimation.FinalValue = 0f;
            translateXAnimation.DampingRatio = 0.7f;
            translateXAnimation.Period = TimeSpan.FromMilliseconds(700);
            visual.StartAnimation("Transform.TranslateX", translateXAnimation);

            // Animate TranslateY back to 0
            var translateYAnimation = compositor.CreateSpringScalarAnimation();
            translateYAnimation.InitialValue = (float)state.Transform.TranslateY;
            translateYAnimation.FinalValue = 0f;
            translateYAnimation.DampingRatio = 0.7f;
            translateYAnimation.Period = TimeSpan.FromMilliseconds(700);
            visual.StartAnimation("Transform.TranslateY", translateYAnimation);

            // Animate ScaleX back to 1
            var scaleXAnimation = compositor.CreateSpringScalarAnimation();
            scaleXAnimation.InitialValue = (float)state.Transform.ScaleX;
            scaleXAnimation.FinalValue = 1f;
            scaleXAnimation.DampingRatio = 0.7f;
            scaleXAnimation.Period = TimeSpan.FromMilliseconds(700);
            visual.StartAnimation("Transform.ScaleX", scaleXAnimation);

            // Animate ScaleY back to 1
            var scaleYAnimation = compositor.CreateSpringScalarAnimation();
            scaleYAnimation.InitialValue = (float)state.Transform.ScaleY;
            scaleYAnimation.FinalValue = 1f;
            scaleYAnimation.DampingRatio = 0.7f;
            scaleYAnimation.Period = TimeSpan.FromMilliseconds(700);
            visual.StartAnimation("Transform.ScaleY", scaleYAnimation);

            // Remove the batch.End() if no explicit batching is needed.
            // When animating properties of the Visual directly, StartAnimation is sufficient.
            // If you need a combined completion event for these animations, you might consider
            // using an AnimationGroup in scenarios where the properties can be animated
            // via a single group, or listen to individual animation completions if needed.
            // For simple reset like this, individual StartAnimation calls are fine.
        }
    }
