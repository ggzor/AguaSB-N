using System.Windows;

using AguaSB.Views.Animaciones.Pipelines;

namespace AguaSB.Views.Animaciones
{
    public static class SingleElementAnimationExtensions
    {
        public static void Apply(this ISingleElementAnimation animation, FrameworkElement element) =>
            animation.Create(element).BeginIn(element);
    }
}
