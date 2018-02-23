using System;
using System.Windows.Controls;

using MahApps.Metro.Controls;

using AguaSB.Individual.Pagos.Views;
using AguaSB.Views.Animaciones;
using AguaSB.Views.Estilos.Modern.Ventanas;
using AguaSB.Views.Animaciones.Pipelines;

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
            view.View.Opacity = 0;
            Panel.Children.Add(view.View);

            var animacionEsquema = AplicadorEsquemasBarraTitulo.Crear(Ventana, view.EsquemaBarraTitulo);

            FadeIn.SetDuration(view.View, AplicadorEsquemasBarraTitulo.Duracion);
            FadeIn.SetEasing(view.View, AplicadorEsquemasBarraTitulo.Easing);
            FadeIn.Apply(view.View);
            Ventana.Begin(animacionEsquema);
        }
    }
}
