using Windows.Foundation;
using Microsoft.UI.Xaml.Media.Animation;

namespace Cheryl.Uno.Helpers.Animations;

public static class ControlExtensions
{
    public static CancellationTokenSource AnimateDouble(this UIElement element, string property, double from, double to, int durationMs = 500, EasingFunctionBase? easing = null)
    {
        var tcs = new TaskCompletionSource<bool>();
        var tokenSource = new CancellationTokenSource();

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
        Storyboard.SetTargetProperty(animation, property);

        tokenSource.Token.Register(() =>
        {
            storyboard.Stop();
            tcs.TrySetCanceled();
        });

        storyboard.Completed += (s, e) => tcs.TrySetResult(true);

        storyboard.Begin();

        return tokenSource;
    }
    
    public static CancellationTokenSource AnimateTranslation(
        this UIElement element,
        string axis, // "X" ou "Y"
        double from,
        double to,
        int durationMs = 500,
        EasingFunctionBase? easing = null)
    {
        if (axis != "X" && axis != "Y")
            throw new ArgumentException("L’axe doit être \"X\" ou \"Y\".", nameof(axis));

        var tcs = new TaskCompletionSource<bool>();
        var tokenSource = new CancellationTokenSource();

        // S'assurer que le RenderTransform est bien un TranslateTransform
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

        // Définir la cible
        Storyboard.SetTarget(animation, element);

        // Utiliser le chemin vers la propriété du TranslateTransform
        string propertyPath = $"RenderTransform.(TranslateTransform.{axis})";
        Storyboard.SetTargetProperty(animation, propertyPath);

        // Gestion annulation
        tokenSource.Token.Register(() =>
        {
            storyboard.Stop();
            tcs.TrySetCanceled();
        });

        storyboard.Completed += (s, e) => tcs.TrySetResult(true);

        storyboard.Begin();

        return tokenSource;
    }
    
    public static CancellationTokenSource AnimateScale(this UIElement element, double from, double to, int durationMs = 500, EasingFunctionBase? easing = null)
    {
        var tokenSource = new CancellationTokenSource();
        var tcs = new TaskCompletionSource<bool>();

        // Vérifie ou ajoute un RenderTransform de type ScaleTransform
        if (element.RenderTransform is not ScaleTransform scale)
        {
            scale = new ScaleTransform { ScaleX = from, ScaleY = from };
            element.RenderTransform = scale;
            element.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        // Crée deux animations séparées pour X et Y
        var animX = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = easing ?? new CubicEase { EasingMode = EasingMode.EaseInOut }
        };

        var animY = new DoubleAnimation
        {
            From = from,
            To = to,
            Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
            EasingFunction = easing ?? new CubicEase { EasingMode = EasingMode.EaseInOut }
        };

        var storyboard = new Storyboard();
        storyboard.Children.Add(animX);
        storyboard.Children.Add(animY);

        Storyboard.SetTarget(animX, scale);
        Storyboard.SetTarget(animY, scale);

        Storyboard.SetTargetProperty(animX, "ScaleX");
        Storyboard.SetTargetProperty(animY, "ScaleY");

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
