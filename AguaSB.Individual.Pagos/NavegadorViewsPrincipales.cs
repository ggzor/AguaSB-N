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
            var animacionEsquema = AplicadorEsquemasBarraTitulo.Crear(Ventana, view.EsquemaBarraTitulo);

            var animacionEntrada = Fade.In
                .WithDuration(AplicadorEsquemasBarraTitulo.Duracion.Subtract(TimeSpan.FromMilliseconds(100)))
                .WithEasing(AplicadorEsquemasBarraTitulo.Easing)
                .Create(view.View);

            animacionEntrada.Pair(animacionEsquema)
                .Before(() => Panel.Children.Add(view.View))
                .Then(() => view.DoFocus())
                .BeginIn(view.View);
        }
    }
}
