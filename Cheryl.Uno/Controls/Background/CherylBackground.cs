using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using Windows.Graphics.Imaging; // For BitmapPixelFormat
using System.Runtime.InteropServices.WindowsRuntime; // For AsStream() extension
using Windows.Storage.Streams; // For IBuffer
using System.Diagnostics;
using Cheryl.Uno.Controls.Background;


namespace Cheryl.Uno.Controls;
public class CherylBackgroundWinUI3 : Image, IDisposable
    {
        private const int ImageWidth = 100; // Largeur du bitmap interne
        private const int ImageHeight = 100; // Hauteur du bitmap interne
        private const float AnimFps = 15; // Images par seconde pour l'animation (5 FPS était un peu bas)

        private WriteableBitmap _bmp;
        private readonly FastNoiseBackgroundRendererWinUI3 _renderer;
        private DispatcherTimer _animationTimer;

        private bool _isLoadedSubscribed = false;
        private bool _disposedValue;


        public CherylBackgroundWinUI3()
        {
            // Ne pas exécuter la logique de rendu complexe en mode design
            if (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled)
            {
                // Vous pourriez afficher une couleur simple ou une placeholder image ici
                // this.Source = new BitmapImage(new Uri("ms-appx:///Assets/placeholder.png"));
                return;
            }

            _bmp = new WriteableBitmap(ImageWidth, ImageHeight);
            this.Source = _bmp;
            this.Stretch = Stretch.UniformToFill;

            _renderer = new FastNoiseBackgroundRendererWinUI3(
                new FastNoiseRendererOptionsWinUI3(FastNoiseLite.NoiseType.OpenSimplex2)
                // Vous pouvez passer des options personnalisées ici si nécessaire
            );

            this.Loaded += CherylBackground_Loaded;
            this.Unloaded += CherylBackground_Unloaded;
            Application.Current.Resuming += Current_Resuming;
            Application.Current.Suspending += Current_Suspending;
        }

        private async void CherylBackground_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isLoadedSubscribed || DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled) return;
             _isLoadedSubscribed = true; // Évite les abonnements multiples si rechargé

            this.ActualThemeChanged += CherylBackground_ActualThemeChanged;
            _renderer.UpdateValues(this.ActualTheme); // Mise à jour initiale basée sur le thème
            await DoRenderAsync(); // Rendu initial

            SetupAnimationTimer();
        }

        private void CherylBackground_Unloaded(object sender, RoutedEventArgs e)
        {
            // Se désabonner seulement si on était abonné
            if (_isLoadedSubscribed)
            {
                this.ActualThemeChanged -= CherylBackground_ActualThemeChanged;
                _isLoadedSubscribed = false;
            }
            StopAnimationTimer();
        }
        
        private void SetupAnimationTimer()
        {
            if (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled) return;

         /*   if (_renderer.SupportsAnimation && AnimFps > 0)
            {
                _animationTimer = new DispatcherTimer();
                _animationTimer.Interval = TimeSpan.FromMilliseconds(1000.0 / AnimFps);
                _animationTimer.Tick += AnimationTimer_Tick;
                _animationTimer.Start();
                Debug.WriteLine("Animation Timer Started");
            }*/
        }

        private void StopAnimationTimer()
        {
            if (_animationTimer != null)
            {
                _animationTimer.Stop();
                _animationTimer.Tick -= AnimationTimer_Tick;
                _animationTimer = null;
                Debug.WriteLine("Animation Timer Stopped");
            }
        }

        private async void AnimationTimer_Tick(object sender, object e)
        {
            await DoRenderAsync();
        }

        private async void CherylBackground_ActualThemeChanged(FrameworkElement sender, object args)
        {
            if (!this.IsLoaded || _renderer == null || DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled) return;
            
            _renderer.UpdateValues(this.ActualTheme);
            await DoRenderAsync(); // Re-rendre avec les nouvelles couleurs/paramètres du thème
        }
        
        private async System.Threading.Tasks.Task DoRenderAsync()
        {
            if (_bmp == null || _renderer == null || (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled)) return;
            
            await _renderer.RenderAsync(_bmp);
            _bmp.Invalidate(); // Indiquer à WinUI de redessiner l'image
        }

        // Gérer la suspension/reprise de l'application pour économiser les ressources
        private void Current_Suspending(object sender, SuspendingEventArgs e)
        {
            StopAnimationTimer();
        }

        private void Current_Resuming(object sender, object e)
        {
             // Redémarrer le timer seulement si le contrôle est chargé
            if (this.IsLoaded)
            {
                SetupAnimationTimer();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    StopAnimationTimer();
                    this.Loaded -= CherylBackground_Loaded;
                    this.Unloaded -= CherylBackground_Unloaded;
                    if (_isLoadedSubscribed)
                    {
                        this.ActualThemeChanged -= CherylBackground_ActualThemeChanged;
                    }
                    Application.Current.Resuming -= Current_Resuming;
                    Application.Current.Suspending -= Current_Suspending;

                    _bmp = null; 
                    // Si _renderer implémentait IDisposable :
                    // if (_renderer is IDisposable disposableRenderer) disposableRenderer.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
