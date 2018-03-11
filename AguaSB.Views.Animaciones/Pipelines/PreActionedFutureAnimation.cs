using System;
using System.Windows;

namespace AguaSB.Views.Animaciones.Pipelines
{
    internal class PreActionedFutureAnimation : IFutureAnimation
    {
        public Action PreAction { get; }
        public IFutureAnimation Animation { get; }

        public PreActionedFutureAnimation(Action preAction, IFutureAnimation animation)
        {
            PreAction = preAction ?? throw new ArgumentNullException(nameof(preAction));
            Animation = animation ?? throw new ArgumentNullException(nameof(animation));
        }

        public void Begin(FrameworkElement element, Action onCompleted)
        {
            PreAction();
            Animation.Begin(element, onCompleted);
        }
    }
}
