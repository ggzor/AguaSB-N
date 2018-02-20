using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using MahApps.Metro.Controls;
using MoreLinq;

using AguaSB.Individual.Pagos.Views;
using AguaSB.Views.Animaciones;
using AguaSB.Views.Estilos.Modern.Ventanas;
using System.Windows.Media;

namespace AguaSB.Individual.Pagos
{
    public class NavegadorViewsPrincipales
    {
        public MetroWindow Ventana { get; }
        public Panel Panel { get; }

        public NavegadorViewsPrincipales(MetroWindow ventana, Panel panel)
        {
            Ventana = ventana ?? throw new ArgumentNullException(nameof(ventana));
            Panel = panel ?? throw new ArgumentNullException(nameof(panel));
        }

        public void IrA(IViewPrincipal view)
        {
            DoubleAnimation CrearAnimacionEscala() => new DoubleAnimation
            {
                EasingFunction = ScaleIn.EasingFunction,
                Duration = ScaleIn.Duration,
                To = 1.2
            };

            var animacionEscalaX = CrearAnimacionEscala();
            var animacionEscalaY = CrearAnimacionEscala();

            var ultimo = Panel.Children.OfType<UIElement>().Last();
            var transformacion = new ScaleTransform { ScaleX = 1, ScaleY = 1 };

            ultimo.RenderTransform = transformacion;
            ultimo.RenderTransformOrigin = new Point(0.5, 0.5);

            view.View.Opacity = 0;
            view.View.Visibility = Visibility.Hidden;
            Panel.Children.Add(view.View);

            AplicadorEsquemasBarraTitulo.Aplicar(Ventana, view.EsquemaBarraTitulo);

            transformacion.BeginAnimation(ScaleTransform.ScaleXProperty, animacionEscalaX);
            transformacion.BeginAnimation(ScaleTransform.ScaleYProperty, animacionEscalaY);

            ScaleIn.Apply(view.View,
                onCompleted: () =>
                {
                    Panel.Children.OfType<UIElement>()
                        .Take(Panel.Children.Count - 1)
                        .ForEach(Panel.Children.Remove);
                    view.DoFocus();
                });
        }
    }
}
