using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using MahApps.Metro.Controls;

using AguaSB.Views.Animaciones;
using static AguaSB.Views.Animaciones.Util.PropPath;

namespace AguaSB.Views.Estilos.Modern.Ventanas
{
    public static class AplicadorEsquemasBarraTitulo
    {
        public static readonly TimeSpan Duracion = TimeSpan.FromSeconds(0.6);
        public static readonly QuadraticEase Easing = new QuadraticEase { EasingMode = EasingMode.EaseOut };

        private static readonly PropertyPath RutaCambioColor =
            Concat(For<MetroWindow, Brush>(m => m.WindowTitleBrush), For<SolidColorBrush, Color>(s => s.Color));

        public static IFutureAnimation Crear(MetroWindow ventana, EsquemaBarraTitulo esquema)
        {
            void AplicarColores()
            {
                ventana.TitleForeground = new SolidColorBrush(esquema.ColorTexto);
                ventana.WindowButtonCommands.LightMinButtonStyle = esquema.EstiloBotones.Minimizar;
                ventana.WindowButtonCommands.LightMaxButtonStyle = esquema.EstiloBotones.Maximizar;
                ventana.WindowButtonCommands.LightCloseButtonStyle = esquema.EstiloBotones.Cerrar;
            }

            void PrepararBarra()
            {
                if (ventana.WindowTitleBrush is SolidColorBrush brush)
                    ventana.WindowTitleBrush = new SolidColorBrush(brush.Color);
                else
                    throw new InvalidOperationException("Cannot animate the title brush because it is not a SolidColorBrush");
            }

            Timeline CrearAnimacion()
            {
                var animacion = new ColorAnimation
                {
                    To = esquema.Color,
                    Duration = Duracion,
                    EasingFunction = Easing
                };

                Storyboard.SetTarget(animacion, ventana);
                Storyboard.SetTargetProperty(animacion, RutaCambioColor);

                return new Storyboard { Children = { animacion } };
            }

            return CrearAnimacion()
                .ToFutureAnimation()
                .Before(() =>
                {
                    PrepararBarra();
                    AplicarColores();
                });
        }

        public static void Aplicar(MetroWindow ventana, EsquemaBarraTitulo esquema) =>
            Crear(ventana, esquema).BeginIn(ventana);
    }
}
