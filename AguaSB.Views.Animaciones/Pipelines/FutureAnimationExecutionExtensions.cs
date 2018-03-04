using System;
using System.Linq;
using System.Windows;

using MoreLinq;

namespace AguaSB.Views.Animaciones.Pipelines
{
    public static class FutureAnimationExecutionExtensions
    {
        public static void Begin(this FrameworkElement element, IFutureAnimation animation)
        {
            animation.PreAction();

            animation.Storyboard.Completed += (s, a) => animation.PostAction();

            element.BeginStoryboard(animation.Storyboard);
        }

        public static IFutureAnimation Then(this IFutureAnimation animation, Action action) =>
            new CompositeFutureAnimation(preAction: FutureAnimation.NoAction, postAction: action, animations: animation);

        public static IFutureAnimation And(this IFutureAnimation animation, params IFutureAnimation[] other) =>
            new CompositeFutureAnimation(animations: other.Concat(animation).ToArray());

        public static void BeginWith(this IFutureAnimation animation, FrameworkElement element) =>
            element.Begin(animation);
    }
}
