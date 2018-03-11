using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

using AguaSB.Views;
using AguaSB.Views.Animaciones;
using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Individual.Pagos.Views
{
    internal sealed class NavegadorViews
    {
        public Panel Panel { get; }

        public NavegadorViews(Panel panel)
        {
            Panel = panel ?? throw new ArgumentNullException(nameof(panel));
        }

        #region Configuracion
        private static readonly IEasingFunction Easing = new CircleEase { EasingMode = EasingMode.EaseOut };
        private const double EscalaFrontal = 0.85;
        private const double EscalaTrasero = 1.1;

        private static readonly ISingleElementAnimation AnimacionesAtrasTrasero = new ISingleElementAnimation[]
        {
            Scale.Uniform
                .WithFromTo(EscalaTrasero, 1.0)
                .WithDuration(TimeSpan.FromMilliseconds(500))
                .WithEasing(Easing),
            Fade.In
                .WithDuration(TimeSpan.FromMilliseconds(200))
                .WithEasing(Easing)
        }.Compose();

        private static readonly ISingleElementAnimation AnimacionesAtrasFrontal = new ISingleElementAnimation[]
        {
            Scale.Uniform
                .WithFromTo(1.0, EscalaFrontal)
                .WithDuration(TimeSpan.FromMilliseconds(200))
                .WithEasing(Easing),
            Fade.Out
                .WithDuration(TimeSpan.FromMilliseconds(200))
                .WithEasing(Easing)
        }.Compose();

        private static readonly ISingleElementAnimation AnimacionesAdelanteTrasero = new ISingleElementAnimation[]
        {
            Fade.Out
                .WithDuration(TimeSpan.FromMilliseconds(100))
                .WithEasing(Easing),
            Scale.Uniform
                .WithFromTo(1, EscalaTrasero)
                .WithDuration(TimeSpan.FromMilliseconds(400))
                .WithEasing(Easing)
        }.Compose();

        private static readonly ISingleElementAnimation AnimacionesAdelanteFrontal = new ISingleElementAnimation[]
        {
            Fade.In
                .WithDuration(TimeSpan.FromMilliseconds(200))
                .WithEasing(Easing)
                .WithFrom(0.2),
            Scale.Uniform
                .WithFromTo(EscalaFrontal, 1)
                .WithEasing(Easing)
                .WithDuration(TimeSpan.FromMilliseconds(500))
        }.Compose();
        #endregion

        public void Atras(IView view, object parametro)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            var trasero = view.View;
            var frontal = Panel.Children.OfType<FrameworkElement>().LastOrDefault();

            var animacionTrasero = AnimacionesAtrasTrasero
                .Create(trasero)
                .Before(() => Panel.Children.Insert(Panel.Children.Count - 1, trasero))
                .Then(() => view.Entrar(parametro));

            var animacionFrontal = frontal == null
                ? FutureAnimation.NoAnimation
                : AnimacionesAtrasFrontal.Create(frontal).Then(() => Panel.Children.Remove(frontal));

            animacionTrasero.Pair(animacionFrontal)
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
                : AnimacionesAdelanteTrasero.Create(trasero).Then(() => Panel.Children.Remove(trasero));

            var animacionFrontal = AnimacionesAdelanteFrontal
                .Create(frontal)
                .Before(() => Panel.Children.Add(frontal))
                .Then(() => view.Entrar(parametro));

            animacionTrasero.Pair(animacionFrontal)
                .BeginIn(Panel);
        }
    }
}
