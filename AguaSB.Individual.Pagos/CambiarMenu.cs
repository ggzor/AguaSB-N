using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using AguaSB.Views.Controles.Animaciones;

namespace AguaSB.Individual.Pagos
{
    internal static class CambiarMenu
    {
        internal static void Aplicar(VentanaPrincipal ventana)
        {
            var duracion = TimeSpan.FromSeconds(0.6);
            var easing = new QuadraticEase { EasingMode = EasingMode.EaseOut };

            var animacion = new ColorAnimation
            {
                To = Colors.White,
                Duration = duracion,
                EasingFunction = easing
            };

            ventana.WindowTitleBrush = new SolidColorBrush(((ventana.WindowTitleBrush as SolidColorBrush)?.Color).Value);

            ventana.WindowTitleBrush.BeginAnimation(SolidColorBrush.ColorProperty, animacion);

            ventana.WindowButtonCommands.LightMinButtonStyle = ventana.WindowButtonCommands.LightMaxButtonStyle = ventana.FindResource("W10ButtonStyle") as Style;
            ventana.WindowButtonCommands.LightCloseButtonStyle = ventana.FindResource("W10CloseButtonStyle") as Style;

            ventana.TitleForeground = Brushes.Black;

            FadeIn.SetDuration(ventana.Menu, duracion.Add(TimeSpan.FromSeconds(0.2)));
            FadeIn.SetEasing(ventana.Menu, easing);
            FadeIn.Apply(ventana.Menu);
        }
    }
}
