using System.Collections.Generic;
using System.Linq;
using System.Windows;

using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones
{
    internal class CompositeSingleElementAnimation : ISingleElementAnimation
    {
        public IEnumerable<ISingleElementAnimation> Animations { get; }

        public CompositeSingleElementAnimation(IEnumerable<ISingleElementAnimation> animations)
        {
            Animations = animations ?? throw new System.ArgumentNullException(nameof(animations));
        }

        public IFutureAnimation Create(FrameworkElement element) => Animations.Select(a => a.Create(element)).Pair();
    }
}