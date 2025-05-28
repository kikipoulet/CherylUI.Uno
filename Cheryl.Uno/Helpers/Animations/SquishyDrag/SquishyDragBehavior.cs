using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.Xaml.Interactivity; 
using System;
using System.Diagnostics;
using Windows.Foundation;
using Cheryl.Uno.Helpers.Easings;

public class SquishyDragBehavior : Behavior<UIElement>
    {
        private Point? _startPoint;
        private CompositeTransform _compositeTransform;
        private DispatcherTimer _animationTimer;
        
        private double _releaseStartSkewX, _releaseStartSkewY;
        private double _releaseStartTranslateX, _releaseStartTranslateY;
        private double _releaseStartScaleX, _releaseStartScaleY;
        private DateTime _releaseAnimationStartTime;
        private const double RELEASE_ANIMATION_DURATION_MS = 700; 

        #region Dependency Properties (Configuration)

        public static readonly DependencyProperty IntensityProperty =
            DependencyProperty.Register(nameof(Intensity), typeof(double), typeof(SquishyDragBehavior), new PropertyMetadata(1.0));

        public double Intensity
        {
            get => (double)GetValue(IntensityProperty);
            set => SetValue(IntensityProperty, value);
        }

        public static readonly DependencyProperty SquishDepthProperty =
            DependencyProperty.Register(nameof(SquishDepth), typeof(double), typeof(SquishyDragBehavior), new PropertyMetadata(1.0));

        public double SquishDepth
        {
            get => (double)GetValue(SquishDepthProperty);
            set => SetValue(SquishDepthProperty, value);
        }

        public static readonly DependencyProperty EnableTiltProperty =
            DependencyProperty.Register(nameof(EnableTilt), typeof(bool), typeof(SquishyDragBehavior), new PropertyMetadata(false));

        public bool EnableTilt
        {
            get => (bool)GetValue(EnableTiltProperty);
            set => SetValue(EnableTiltProperty, value);
        }

        public static readonly DependencyProperty EnableXProperty =
            DependencyProperty.Register(nameof(EnableX), typeof(bool), typeof(SquishyDragBehavior), new PropertyMetadata(true));

        public bool EnableX
        {
            get => (bool)GetValue(EnableXProperty);
            set => SetValue(EnableXProperty, value);
        }

        public static readonly DependencyProperty EnableYProperty =
            DependencyProperty.Register(nameof(EnableY), typeof(bool), typeof(SquishyDragBehavior), new PropertyMetadata(true));

        public bool EnableY
        {
            get => (bool)GetValue(EnableYProperty);
            set => SetValue(EnableYProperty, value);
        }
        
        public static readonly DependencyProperty ScaleBasedOnAxisDisplacementProperty =
            DependencyProperty.Register(nameof(ScaleBasedOnAxisDisplacement), typeof(bool), typeof(SquishyDragBehavior), new PropertyMetadata(false));

        public bool ScaleBasedOnAxisDisplacement
        {
            get => (bool)GetValue(ScaleBasedOnAxisDisplacementProperty);
            set => SetValue(ScaleBasedOnAxisDisplacementProperty, value);
        }

        #endregion
        
        
        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject != null)
            {
                _compositeTransform = this.AssociatedObject.RenderTransform as CompositeTransform ?? new CompositeTransform();
                this.AssociatedObject.RenderTransform = _compositeTransform;
                this.AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);

                this.AssociatedObject.PointerPressed += AssociatedObject_PointerPressed;
                this.AssociatedObject.PointerMoved += AssociatedObject_PointerMoved;
                this.AssociatedObject.PointerReleased += AssociatedObject_PointerReleased;
                this.AssociatedObject.PointerExited += AssociatedObject_PointerExitedOrCanceled;
                this.AssociatedObject.PointerCanceled += AssociatedObject_PointerExitedOrCanceled;
                this.AssociatedObject.PointerCaptureLost += AssociatedObject_PointerCaptureLost;
                
                this.AssociatedObject.AddHandler(UIElement.PointerPressedEvent, new PointerEventHandler(AssociatedObject_PointerPressed), true); 
                this.AssociatedObject.AddHandler(UIElement.PointerReleasedEvent, new PointerEventHandler(AssociatedObject_PointerReleased), true); 
                this.AssociatedObject.AddHandler(UIElement.PointerMovedEvent, new PointerEventHandler(AssociatedObject_PointerMoved), true); 

          
                
                _animationTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) }; 
                _animationTimer.Tick += AnimationTimer_Tick;
                
               
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.AssociatedObject != null)
            {
                this.AssociatedObject.PointerPressed -= AssociatedObject_PointerPressed;
                this.AssociatedObject.PointerMoved -= AssociatedObject_PointerMoved;
                this.AssociatedObject.PointerReleased -= AssociatedObject_PointerReleased;
                this.AssociatedObject.PointerExited -= AssociatedObject_PointerExitedOrCanceled;
                this.AssociatedObject.PointerCanceled -= AssociatedObject_PointerExitedOrCanceled;
                this.AssociatedObject.PointerCaptureLost -= AssociatedObject_PointerCaptureLost;
            }

            if (_animationTimer != null)
            {
                _animationTimer.Stop();
                _animationTimer.Tick -= AnimationTimer_Tick;
                _animationTimer = null;
            }

            _compositeTransform = null;
        }

        private void AssociatedObject_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (AssociatedObject == null) return;
            
            PointerPoint pointerPoint = e.GetCurrentPoint(AssociatedObject);
            if (pointerPoint.Properties.IsLeftButtonPressed)
            {
                _startPoint = pointerPoint.Position;
                _animationTimer?.Stop(); 
                
                AssociatedObject.CapturePointer(e.Pointer);
            }
            e.Handled = true;
        }

        private void AssociatedObject_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (AssociatedObject == null || _startPoint == null || _compositeTransform == null) return;

            PointerPoint pointerPoint = e.GetCurrentPoint(AssociatedObject);
           
            if (pointerPoint.IsInContact) 
            {
                Point currentPosition = pointerPoint.Position;
                double dx = currentPosition.X - _startPoint.Value.X;
                double dy = currentPosition.Y - _startPoint.Value.Y;

                double skewFactor = 0.007 * Intensity;
                double translateFactor = 0.018 * Intensity;
                double scaleFactor = 0.00007 * Intensity;


                if (EnableTilt)
                {
                    
                        var Xangle = dx * skewFactor;
                        if (Xangle > 3 * SquishDepth)
                            Xangle = 3 * SquishDepth;
                        if (Xangle < -3 * SquishDepth)
                            Xangle = -3 * SquishDepth;

                        var Yangle = -dy * skewFactor;
                        if (Yangle > 3 * SquishDepth)
                            Yangle = 3 * SquishDepth;
                        if (Yangle < -3 * SquishDepth)
                            Yangle = -3 * SquishDepth;

                        _compositeTransform.SkewX = (Yangle < 0 ? Xangle : -Xangle) * Math.Abs(Yangle) * 0.3;
                        _compositeTransform.SkewY  = (Xangle < 0 ? Yangle : -Yangle) * Math.Abs(Xangle) * 0.3;
                    
                
                }
                else
                {
                    _compositeTransform.SkewX = 0;
                    _compositeTransform.SkewY = 0;
                }


                if (EnableX)
                    _compositeTransform.TranslateX = dx * translateFactor;
                else
                    _compositeTransform.TranslateX = 0;

                if (EnableY)
                    _compositeTransform.TranslateY = dy * translateFactor;
                else
                    _compositeTransform.TranslateY = 0;


                double targetScaleX = 1.0;
                double targetScaleY = 1.0;

                if (ScaleBasedOnAxisDisplacement) 
                {
                    if (EnableX)
                        targetScaleX = 1.0 + dx * scaleFactor * 1.5; 
                    if (EnableY)
                        targetScaleY = 1.0 - dy * scaleFactor * 1.5;
                }
                else
                {
                    double displacementMagnitude = Math.Sqrt(dx * dx + dy * dy);
                    double commonScaleFactor = 1.0 - (displacementMagnitude * scaleFactor);
                    if (EnableX) targetScaleX = commonScaleFactor;
                    if (EnableY) targetScaleY = commonScaleFactor;
                }

        
                _compositeTransform.ScaleX = Math.Max(0.2, targetScaleX);
                _compositeTransform.ScaleY = Math.Max(0.2, targetScaleY);
            }
            e.Handled = true;
        }

        private void AssociatedObject_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (AssociatedObject == null || _startPoint == null) return;

            StartReleaseAnimation();
            AssociatedObject.ReleasePointerCapture(e.Pointer);
            _startPoint = null;
            e.Handled = true;
        }
        
        private void AssociatedObject_PointerExitedOrCanceled(object sender, PointerRoutedEventArgs e)
        {
           
        }
        
        private void AssociatedObject_PointerCaptureLost(object sender, PointerRoutedEventArgs e)
        {
            
            if (AssociatedObject == null || _startPoint == null) return;

            StartReleaseAnimation();
            _startPoint = null; 
        }


        private void StartReleaseAnimation()
        {
            if (_compositeTransform == null || _animationTimer == null) return;

            _releaseStartSkewX = _compositeTransform.SkewX;
            _releaseStartSkewY = _compositeTransform.SkewY;
            _releaseStartTranslateX = _compositeTransform.TranslateX;
            _releaseStartTranslateY = _compositeTransform.TranslateY;
            _releaseStartScaleX = _compositeTransform.ScaleX;
            _releaseStartScaleY = _compositeTransform.ScaleY;

            _releaseAnimationStartTime = DateTime.Now;
            _animationTimer.Start();
        }

        private  CherylEasing.CherylSpringEase Easing = new CherylEasing.CherylSpringEase()
        {
            Mass = 1,
            Damping = 7,
            Stiffness = 50
        };
        
        private void AnimationTimer_Tick(object sender, object e)
        {
            if (_compositeTransform == null)
            {
                _animationTimer?.Stop();
                return;
            }

            double elapsedMs = (DateTime.Now - _releaseAnimationStartTime).TotalMilliseconds;
            double progress = Math.Min(elapsedMs / RELEASE_ANIMATION_DURATION_MS, 1.0);
            var durationMs = 700;

            double t = Math.Min(elapsedMs / durationMs, 1.0);
            double easedProgress = Easing.Ease(elapsedMs, 0,1,durationMs);

            _compositeTransform.SkewX = _releaseStartSkewX * (1.0 - easedProgress);
            _compositeTransform.SkewY = _releaseStartSkewY * (1.0 - easedProgress);
            _compositeTransform.TranslateX = _releaseStartTranslateX * (1.0 - easedProgress);
            _compositeTransform.TranslateY = _releaseStartTranslateY * (1.0 - easedProgress);
            _compositeTransform.ScaleX = _releaseStartScaleX + (1.0 - _releaseStartScaleX) * easedProgress;
            _compositeTransform.ScaleY = _releaseStartScaleY + (1.0 - _releaseStartScaleY) * easedProgress;

            if (progress >= 1.0)
            {
                _animationTimer.Stop();

                _compositeTransform.SkewX = 0;
                _compositeTransform.SkewY = 0;
                _compositeTransform.TranslateX = 0;
                _compositeTransform.TranslateY = 0;
                _compositeTransform.ScaleX = 1;
                _compositeTransform.ScaleY = 1;
            }
        }
    }
