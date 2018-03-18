using System.Collections.Generic;
using System.Windows;

using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones
{
    public static class SingleElementAnimationExtensions
    {
        public static void Apply(this ISingleElementAnimation animation, FrameworkElement element) =>
            animation.Create(element).BeginIn(element);

        public static ISingleElementAnimation Compose(params ISingleElementAnimation[] animations) => animations.Compose();

        public static ISingleElementAnimation Compose(this IEnumerable<ISingleElementAnimation> animations) =>
            new CompositeSingleElementAnimation(animations);
    }
}
