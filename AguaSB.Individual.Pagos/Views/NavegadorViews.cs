using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using AguaSB.Views;
using AguaSB.Views.Animaciones;
using AguaSB.Views.Animaciones.Neon;

namespace AguaSB.Individual.Pagos.Views
{
    internal sealed class NavegadorViews
    {
        public Panel Panel { get; }

        public NavegadorViews(Panel panel)
        {
            Panel = panel ?? throw new ArgumentNullException(nameof(panel));
        }

        public void Atras(IView view, object parametro)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            var trasero = view.View;
            var frontal = Panel.Children.OfType<FrameworkElement>().LastOrDefault();

            var animacionTrasero = Appear.FromAbove
                .Create(trasero)
                .Before(() => Panel.Children.Insert(Panel.Children.Count - 1, trasero));

            var animacionFrontal = frontal == null
                ? FutureAnimation.NoAnimation
                : Disappear.ToBelow.Create(frontal).Then(() => Panel.Children.Remove(frontal));

            animacionTrasero.Pair(animacionFrontal, CrearRetrasoEntrada(view, parametro))
                .BeginIn(Panel);
        }

        public void Adelante(IView view, object parametro)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            var trasero = Panel.Children.OfType<FrameworkElement>().LastOrDefault();
            var frontal = view.View;

            var animacionTrasero = trasero == null
                ? FutureAnimation.NoAnimation
                : Disappear.ToAbove.Create(trasero).Then(() => Panel.Children.Remove(trasero));

            var animacionFrontal = Appear.FromBelow
                .Create(frontal)
                .Before(() => Panel.Children.Add(frontal));

            animacionTrasero.Pair(animacionFrontal, CrearRetrasoEntrada(view, parametro))
                .BeginIn(Panel);
        }

        private static IFutureAnimation CrearRetrasoEntrada(IView view, object parametro) =>
            Delay.Action.For(TimeSpan.FromMilliseconds(20)).Create(() => view.Entrar(parametro));
    }
}
