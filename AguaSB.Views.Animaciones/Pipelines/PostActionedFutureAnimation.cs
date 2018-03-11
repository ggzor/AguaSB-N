using System;
using System.Windows;

namespace AguaSB.Views.Animaciones.Pipelines
{
    internal class PostActionedFutureAnimation : IFutureAnimation
    {
        public IFutureAnimation Animation { get; }
        public Action PostAction { get; }

        public PostActionedFutureAnimation(IFutureAnimation animation, Action postAction)
        {
            Animation = animation ?? throw new ArgumentNullException(nameof(animation));
            PostAction = postAction ?? throw new ArgumentNullException(nameof(postAction));
        }

        public void Begin(FrameworkElement element, Action onCompleted)
        {
            Animation.Begin(element, () =>
            {
                PostAction();
                onCompleted();
            });
        }
    }
}
