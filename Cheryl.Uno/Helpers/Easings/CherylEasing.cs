using Microsoft.UI.Xaml.Media.Animation;

namespace Cheryl.Uno.Helpers.Easings;

public class CherylEasing
{
  public class CherylSpringEase : IEasingFunction
    {
        private double _mass = 1.0;
        private double _stiffness = 50.0;
        private double _damping = 10.0;
        private double _normalizationFactor = 1.0;

        public double Mass
        {
            get => _mass;
            set
            {
                _mass = value;
                RecalculateNormalizationFactor();
            }
        }

        public double Stiffness
        {
            get => _stiffness;
            set
            {
                _stiffness = value;
                RecalculateNormalizationFactor();
            }
        }

        public double Damping
        {
            get => _damping;
            set
            {
                _damping = value;
                RecalculateNormalizationFactor();
            }
        }

        private void RecalculateNormalizationFactor()
        {
            _normalizationFactor = CalculateEase(1.0);
            if (_normalizationFactor != 0) // Éviter la division par zéro
            {
                _normalizationFactor = 1.0 / _normalizationFactor;
            }
        }

        private double CalculateEase(double normalizedTime)
        {
            if (Mass <= 0 || Stiffness <= 0)
                return normalizedTime;

            double w0 = Math.Sqrt(Stiffness / Mass);
            double zeta = Damping / (2 * Math.Sqrt(Stiffness * Mass));

            double t = normalizedTime;
            double position;

            if (Math.Abs(zeta - 1.0) < 1e-9)
            {
                position = Math.Exp(-w0 * t) * (1 + w0 * t);
            }
            else if (zeta < 1)
            {
                double wd = w0 * Math.Sqrt(1 - zeta * zeta);
                position = Math.Exp(-zeta * w0 * t) * (Math.Cos(wd * t) + (zeta * w0 / wd) * Math.Sin(wd * t));
            }
            else
            {
                double wd_prime = w0 * Math.Sqrt(zeta * zeta - 1);

                if (Math.Abs(wd_prime) < 1e-9)
                {
                    position = Math.Exp(-w0 * t) * (1 + w0 * t);
                }
                else
                {
                    position = Math.Exp(-zeta * w0 * t) * (Math.Cosh(wd_prime * t) + (zeta * w0 / wd_prime) * Math.Sinh(wd_prime * t));
                }
            }

            return (1.0 - position) * _normalizationFactor;
        }

        public double Ease(double currentTime, double startValue, double finalValue, double duration)
        {
            // Normaliser le temps entre 0 et 1
            double normalizedTime = currentTime / duration;
            if (normalizedTime < 0) normalizedTime = 0;
            if (normalizedTime > 1) normalizedTime = 1;

            // Calculer la progression ajustée avec l'assouplissement
            double easedProgress = CalculateEase(normalizedTime);

            // Interpoler entre startValue et finalValue
            return startValue + (finalValue - startValue) * easedProgress;
        }
    }
}
