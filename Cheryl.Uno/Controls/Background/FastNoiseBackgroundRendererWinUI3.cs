

using Microsoft.UI.Xaml; // Pour ElementTheme
using Microsoft.UI.Xaml.Media.Imaging; // Pour WriteableBitmap
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI; // Pour Windows.UI.Color

using System.Diagnostics;

namespace Cheryl.Uno.Controls.Background;

    public class FastNoiseBackgroundRendererWinUI3
    {
        public bool SupportsAnimation => true;

        private static readonly Random Rand = new Random();
        private readonly FastNoiseLite _noiseGen = new FastNoiseLite(Rand.Next()); // Initialiser avec une seed aléatoire

        private readonly object _lockObj = new object();
        private bool _isRedrawing;

        // Couleurs stockées en uint (ARGB) comme dans l'original
        private uint _themeColorUint;
        private uint _accentColorUint;
        private uint _baseColorUint;

        // Champs pour les paramètres de rendu
        private float _pOffsetX;
        private float _pOffsetY;
        private float _aOffsetX;
        private float _aOffsetY;

        private readonly float _optNoiseScale;    // NoiseScale des options
        private readonly float _optXAnim;         // XAnimSpeed des options
        private readonly float _optYAnim;         // YAnimSpeed des options
        private readonly float _optPrimaryAlpha;  // PrimaryAlpha des options
        private readonly float _optAccentAlpha;   // AccentAlpha des options

        // Champs pour les valeurs dépendant du thème, mises à jour par UpdateValues
        private float _currentThemeAccentAlpha;
        private float _currentThemeNoiseScale;


        public FastNoiseBackgroundRendererWinUI3(FastNoiseRendererOptionsWinUI3? options = null)
        {
            var opt = options ?? new FastNoiseRendererOptionsWinUI3(FastNoiseLite.NoiseType.OpenSimplex2);
            _noiseGen.SetNoiseType(opt.Type);
            // Vous pourriez vouloir configurer d'autres paramètres de FastNoiseLite ici (fréquence, fractale etc.)
            // _noiseGen.SetFrequency(0.02f); // Exemple

            _optNoiseScale = opt.NoiseScale * 100f;
            _optXAnim = opt.XAnimSpeed;
            _optYAnim = opt.YAnimSpeed;
            _optPrimaryAlpha = opt.PrimaryAlpha;
            _optAccentAlpha = opt.AccentAlpha;

            // Initialiser les offsets pour l'animation
            _pOffsetX = Rand.Next(1000);
            _pOffsetY = Rand.Next(1000);
            _aOffsetX = Rand.Next(1000);
            _aOffsetY = Rand.Next(1000);
        }

        public void UpdateValues(ElementTheme baseTheme)
        {
            // Convertir les couleurs Avalonia en Windows.UI.Color puis en uint ARGB
            _themeColorUint = ConvertToUint(Color.FromArgb(255, 10, 89, 247));
            _accentColorUint = ConvertToUint(Color.FromArgb(255, 89, 0, 255));

            _baseColorUint = baseTheme == ElementTheme.Light
                ? ConvertToUint(Color.FromArgb(255, 245, 245, 245)) // Light Gray
                : ConvertToUint(Color.FromArgb(255, 0, 0, 0));     // Black

            // Définir accalpha et noisescale en fonction du thème, comme dans le Dispatcher.Invoke original
            if (baseTheme == ElementTheme.Light)
            {
                _currentThemeAccentAlpha = _optAccentAlpha;
                _currentThemeNoiseScale = _optNoiseScale;
            }
            else // Dark ou Default (on suppose que Default suit Dark pour ces valeurs)
            {
                _currentThemeAccentAlpha = 0.21f;
                _currentThemeNoiseScale = 0.86f * 100f;
            }
             Debug.WriteLine($"Renderer Updated: Theme={baseTheme}, AccentAlpha={_currentThemeAccentAlpha}, NoiseScale={_currentThemeNoiseScale}");
        }

        public async Task RenderAsync(WriteableBitmap surface)
        {
            if (surface == null) return;

            // Animation: mise à jour des offsets
            _pOffsetX += _optXAnim;
            _pOffsetY += _optYAnim;
            _aOffsetX -= _optXAnim; // Notez le signe moins pour un mouvement opposé/différent
            _aOffsetY -= _optYAnim;

            if (_isRedrawing) return;

            lock (_lockObj)
            {
                if (_isRedrawing) return;
                _isRedrawing = true;
            }

            try
            {
                int width = surface.PixelWidth;
                int height = surface.PixelHeight;
                IBuffer pixelBuffer = surface.PixelBuffer;

                // Le rendu se fait sur un thread séparé pour ne pas bloquer l'UI
                await Task.Run(() =>
                {
                    byte[] pixels = new byte[width * height * 4]; // BGRA format for WinUI
                    float frameScale = (1f / height) * _currentThemeNoiseScale;

                    Parallel.For(0, height, y =>
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // Couche Primaire
                            float noiseVal1 = _noiseGen.GetNoise((_pOffsetX + x) * frameScale, (_pOffsetY + y) * frameScale);
                            noiseVal1 = (noiseVal1 + 1f) / 2f * _optPrimaryAlpha; // Normaliser et appliquer alpha
                            byte alpha1 = (byte)(noiseVal1 * 255);
                            uint firstLayerColor = BlendPixelOverlay(WithAlpha(_themeColorUint, alpha1), _baseColorUint);

                            // Couche d'Accentuation
                            float noiseVal2 = _noiseGen.GetNoise((_aOffsetX + x) * frameScale, (_aOffsetY + y) * frameScale);
                            noiseVal2 = (noiseVal2 + 1f) / 2f * _currentThemeAccentAlpha; // Utiliser _currentThemeAccentAlpha
                            byte alpha2 = (byte)(noiseVal2 * 255);
                            uint finalColorUint = BlendPixel(WithAlpha(_accentColorUint, alpha2), firstLayerColor);

                            // Conversion de uint ARGB en bytes BGRA pour WriteableBitmap
                            int R_idx = (y * width + x) * 4;
                            pixels[R_idx + 0] = B(finalColorUint);     // Blue
                            pixels[R_idx + 1] = G(finalColorUint);     // Green
                            pixels[R_idx + 2] = R(finalColorUint);     // Red
                            pixels[R_idx + 3] = A(finalColorUint);     // Alpha (sera l'alpha de _baseColorUint après mélange)
                        }
                    });

                    // Copier les pixels dans le buffer du WriteableBitmap
                    // Doit être fait sur le thread UI ou via un Dispatcher si Task.Run n'est pas configuré pour cela.
                    // Cependant, PixelBuffer.AsStream().WriteAsync peut souvent gérer cela correctement.
                    // Pour plus de sécurité, on pourrait marshaler cette partie vers le thread UI si des problèmes surviennent.
                    using (var stream = pixelBuffer.AsStream())
                    {
                        stream.Write(pixels, 0, pixels.Length);
                    }
                });
            }
            finally
            {
                lock (_lockObj) { _isRedrawing = false; }
            }
        }

        // --- Méthodes utilitaires pour les couleurs (portées depuis l'original) ---
        private static uint ConvertToUint(Color color) => ARGB(color.A, color.R, color.G, color.B);
        private static uint ARGB(byte a, byte r, byte g, byte b) => (uint)(a << 24 | r << 16 | g << 8 | b);
        private static byte A(uint col) => (byte)(col >> 24);
        private static byte R(uint col) => (byte)(col >> 16);
        private static byte G(uint col) => (byte)(col >> 8);
        private static byte B(uint col) => (byte)col;
        private static uint WithAlpha(uint col, byte a) => (col & 0x00FFFFFF) | (uint)(a << 24);

        private static uint BlendPixel(uint fore, uint back) // Standard alpha blending
        {
            float alphaF = A(fore) / 255.0f;
            byte resultR = (byte)(R(fore) * alphaF + R(back) * (1 - alphaF));
            byte resultG = (byte)(G(fore) * alphaF + G(back) * (1 - alphaF));
            byte resultB = (byte)(B(fore) * alphaF + B(back) * (1 - alphaF));
            return ARGB(A(back), resultR, resultG, resultB); // Alpha de fond est conservé
        }

        private static uint BlendPixelOverlay(uint fore, uint back)
        {
            float alphaF = A(fore) / 255.0f;
            byte r = OverlayComponentBlend(R(fore), R(back), alphaF);
            byte g = OverlayComponentBlend(G(fore), G(back), alphaF);
            byte b = OverlayComponentBlend(B(fore), B(back), alphaF);
            return ARGB(A(back), r, g, b); // Alpha de fond est conservé
        }

        private static byte OverlayComponentBlend(byte cf, byte cb, float alphaF) // Component Fore, Component Back
        {
            float blendedC;
            if (cb <= 128)
                blendedC = (2 * cf * cb) / 255.0f;
            else
                blendedC = 255 - (2 * (255 - cf) * (255 - cb)) / 255.0f;
            
            // Appliquer l'alpha du premier plan sur le résultat du mélange Overlay
            return (byte)(blendedC * alphaF + cb * (1 - alphaF));
        }
    }
