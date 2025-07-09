using Windows.Foundation;
using Cheryl.Uno.Helpers.Easings;
using Microsoft.UI.Xaml.Media.Animation;

namespace Cheryl.Uno.Helpers.Animations;

public  static partial class ControlExtensions
{
    public static CancellationTokenSource AnimateTranslation(this UIElement element, string axis, double from, double to, int durationMs = 500, IEasingFunction easing = null)
    {
        if (axis != "X" && axis != "Y")
            throw new ArgumentException("L’axe doit être \"X\" ou \"Y\".", nameof(axis));

        var tcs = new TaskCompletionSource<bool>();
        var tokenSource = new CancellationTokenSource();

        if (element.RenderTransform is not TranslateTransform translate)
        {
            translate = new TranslateTransform();
            element.RenderTransform = translate;
        }

        var animation = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = easing ?? new CubicEase { EasingMode = EasingMode.EaseInOut }
        };

        var storyboard = new Storyboard();
        storyboard.Children.Add(animation);

        Storyboard.SetTarget(animation, element);

        string propertyPath = $"RenderTransform.(TranslateTransform.{axis})";
        Storyboard.SetTargetProperty(animation, propertyPath);

        tokenSource.Token.Register(() =>
        {
            storyboard.Stop();
            tcs.TrySetCanceled();
        });

        storyboard.Completed += (s, e) => tcs.TrySetResult(true);

        storyboard.Begin();

        return tokenSource;
    }
    
    public static CancellationTokenSource AnimateMorphingAppearing(this UIElement element, double TranslationX, double TranslationY, int durationMs = 500, double morphingIntensity = 1)
    {
        var tcs = new TaskCompletionSource<bool>();
        var tokenSource = new CancellationTokenSource();
        
        if (element.RenderTransform is not CompositeTransform composite)
        {
            composite = new CompositeTransform();
                element.RenderTransformOrigin = new Point(0.5, 0.5);
            element.RenderTransform = composite;
        }
        
        var storyboard = new Storyboard();
        
       

        var animationX = new DoubleAnimation
        {
            From = TranslationX,
            To = 0,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = new CherylEasing.CherylSpringEase()
        };
        storyboard.Children.Add(animationX);
        Storyboard.SetTarget(animationX, element);
        Storyboard.SetTargetProperty(animationX, "RenderTransform.(CompositeTransform.TranslateX)");
        
        var animationY = new DoubleAnimation
        {
            From = TranslationY,
            To = 0,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = new CherylEasing.CherylSpringEase()
        };
        storyboard.Children.Add(animationY);
        Storyboard.SetTarget(animationY, element);
        Storyboard.SetTargetProperty(animationY, "RenderTransform.(CompositeTransform.TranslateY)");
        
        var AnimOpacity = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction =  new CubicEase { EasingMode = EasingMode.EaseInOut }
        };
        storyboard.Children.Add(AnimOpacity);
        Storyboard.SetTarget(AnimOpacity, element);
        Storyboard.SetTargetProperty(AnimOpacity, "Opacity");
        
        var AnimScaleX = new DoubleAnimation
        {
            From = 1 - Math.Abs(TranslationY * morphingIntensity * 0.005),
            To = 1,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = new CherylEasing.CherylSpringEase()
        };
        storyboard.Children.Add(AnimScaleX);
        Storyboard.SetTarget(AnimScaleX, element);
        Storyboard.SetTargetProperty(AnimScaleX, "RenderTransform.(CompositeTransform.ScaleX)");
        
        var AnimScaleY = new DoubleAnimation
        {
            From = 1 - Math.Abs(TranslationX * morphingIntensity * 0.005),
            To = 1,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = new CherylEasing.CherylSpringEase()
        };
        storyboard.Children.Add(AnimScaleY);
        Storyboard.SetTarget(AnimScaleY, element);
        Storyboard.SetTargetProperty(AnimScaleY, "RenderTransform.(CompositeTransform.ScaleY)");


        tokenSource.Token.Register(() =>
        {
            storyboard.Stop();
            tcs.TrySetCanceled();
        });
        storyboard.Completed += (s, e) => tcs.TrySetResult(true);
        storyboard.Begin();

        return tokenSource;
    }
    
    public static CancellationTokenSource AnimateMorphingDisappearing(this UIElement element, double TranslationX, double TranslationY, int durationMs = 500, double morphingIntensity = 1)
    {
        var tcs = new TaskCompletionSource<bool>();
        var tokenSource = new CancellationTokenSource();
        
        if (element.RenderTransform is not CompositeTransform composite)
        {
            composite = new CompositeTransform();
                element.RenderTransformOrigin = new Point(0.5, 0.5);
            element.RenderTransform = composite;
        }
        
        var storyboard = new Storyboard();
        
       

        var animationX = new DoubleAnimation
        {
            From = 0,
            To = TranslationX,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = new CherylEasing.CherylSpringEase()
        };
        storyboard.Children.Add(animationX);
        Storyboard.SetTarget(animationX, element);
        Storyboard.SetTargetProperty(animationX, "RenderTransform.(CompositeTransform.TranslateX)");
        
        var animationY = new DoubleAnimation
        {
            From = 0,
            To = TranslationY,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = new CherylEasing.CherylSpringEase()
        };
        storyboard.Children.Add(animationY);
        Storyboard.SetTarget(animationY, element);
        Storyboard.SetTargetProperty(animationY, "RenderTransform.(CompositeTransform.TranslateY)");
        
        var AnimOpacity = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction =  new CubicEase { EasingMode = EasingMode.EaseInOut }
        };
        storyboard.Children.Add(AnimOpacity);
        Storyboard.SetTarget(AnimOpacity, element);
        Storyboard.SetTargetProperty(AnimOpacity, "Opacity");
        
        var AnimScaleX = new DoubleAnimation
        {
            From = 1,
            To = 1 - Math.Abs(TranslationY * morphingIntensity * 0.005),
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = new CherylEasing.CherylSpringEase()
        };
        storyboard.Children.Add(AnimScaleX);
        Storyboard.SetTarget(AnimScaleX, element);
        Storyboard.SetTargetProperty(AnimScaleX, "RenderTransform.(CompositeTransform.ScaleX)");
        
        var AnimScaleY = new DoubleAnimation
        {
            From = 1,
            To = 1 - Math.Abs(TranslationX * morphingIntensity * 0.005),
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = new CherylEasing.CherylSpringEase()
        };
        storyboard.Children.Add(AnimScaleY);
        Storyboard.SetTarget(AnimScaleY, element);
        Storyboard.SetTargetProperty(AnimScaleY, "RenderTransform.(CompositeTransform.ScaleY)");


        tokenSource.Token.Register(() =>
        {
            storyboard.Stop();
            tcs.TrySetCanceled();
        });
        storyboard.Completed += (s, e) => tcs.TrySetResult(true);
        storyboard.Begin();

        return tokenSource;
    }
}
