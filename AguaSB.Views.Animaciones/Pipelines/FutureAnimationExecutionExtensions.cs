using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;
using MoreLinq;

namespace AguaSB.Views.Animaciones.Pipelines
{
    public static class FutureAnimationExecutionExtensions
    {
        public static readonly Action NoAction = () => { };

        public static void BeginIn(this IFutureAnimation animation, FrameworkElement element) =>
            animation.Begin(element, NoAction);

        public static IFutureAnimation ToFutureAnimation(this Timeline timeline) =>
            new StoryboardFutureAnimation(timeline);

        public static IFutureAnimation Append(this IFutureAnimation animation, IFutureAnimation other) =>
            new SequentialFutureAnimation(first: animation, second: other);

        public static IFutureAnimation Before(this IFutureAnimation animation, Action action) =>
            new PreActionedFutureAnimation(action, animation);

        public static IFutureAnimation Then(this IFutureAnimation animation, Action action) =>
            new PostActionedFutureAnimation(animation, action);

        public static IFutureAnimation Pair(this IFutureAnimation animation, params IFutureAnimation[] other) =>
            new ParallelFutureAnimation(other.Concat(animation).ToArray());
    }
}
