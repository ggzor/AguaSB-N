using System;
using System.Windows;

using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones.Neon
{
    internal class VisibilityAnimator : ISingleElementAnimation
    {
        public IFaderBuilder Fader { get; }
        public IUniformScalerBuilder Scaler { get; }

        public VisibilityAnimator(IFaderBuilder fader, IUniformScalerBuilder scaler)
        {
            Fader = fader.WithEasing(AppearDisappearCommon.Easing) ?? throw new ArgumentNullException(nameof(fader));
            Scaler = scaler.WithEasing(AppearDisappearCommon.Easing) ?? throw new ArgumentNullException(nameof(scaler));
        }

        public IFutureAnimation Create(FrameworkElement element) =>
            SingleElementAnimationExtensions.Compose(Fader, Scaler).Create(element);
    }
}
