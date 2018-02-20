using System;
using System.Windows.Media;
using System.Windows.Media.Animation;

using MahApps.Metro.Controls;

namespace AguaSB.Views.Estilos.Modern.Ventanas
{
    public static class AplicadorEsquemasBarraTitulo
    {
        private static readonly TimeSpan Duracion = TimeSpan.FromSeconds(0.6);
        private static readonly QuadraticEase Easing = new QuadraticEase { EasingMode = EasingMode.EaseOut };

        public static void Aplicar(MetroWindow ventana, EsquemaBarraTitulo esquema)
        {
            void AplicarColores()
            {
                ventana.WindowButtonCommands.LightMinButtonStyle = esquema.EstiloBotones.Minimizar;
                ventana.WindowButtonCommands.LightMaxButtonStyle = esquema.EstiloBotones.Maximizar;
                ventana.WindowButtonCommands.LightCloseButtonStyle = esquema.EstiloBotones.Cerrar;
                ventana.TitleForeground = new SolidColorBrush(esquema.ColorTexto);
            }

            void AplicarAnimacion()
            {
                var animacion = new ColorAnimation
                {
                    To = esquema.Color,
                    Duration = Duracion,
                    EasingFunction = Easing
                };
                ventana.WindowTitleBrush = new SolidColorBrush(((ventana.WindowTitleBrush as SolidColorBrush)?.Color).Value);
                ventana.WindowTitleBrush.BeginAnimation(SolidColorBrush.ColorProperty, animacion);
            }

            AplicarAnimacion();
            AplicarColores();
        }
    }
}
