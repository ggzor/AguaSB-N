using System.Windows;
using System.Windows.Media.Animation;

using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones.Implementation
{
    internal partial class GenericUniformScaler : IUniformScalerBuilder
    {
        public IUniformScalerBuilder WithDuration(Duration newDuration) =>
            new GenericUniformScaler(Delay, newDuration, Easing, From, To);

        public IUniformScalerBuilder WithEasing(IEasingFunction newEasing) =>
            new GenericUniformScaler(Delay, Duration, newEasing, From, To);

        public IUniformScalerBuilder WithFrom(double newFrom) =>
            new GenericUniformScaler(Delay, Duration, Easing, newFrom, To);

        public IUniformScalerBuilder WithTo(double newTo) =>
            new GenericUniformScaler(Delay, Duration, Easing, From, newTo);

        public IUniformScalerBuilder WithFromTo(double newFrom, double newTo) =>
            new GenericUniformScaler(Delay, Duration, Easing, newFrom, newTo);
    }
}
