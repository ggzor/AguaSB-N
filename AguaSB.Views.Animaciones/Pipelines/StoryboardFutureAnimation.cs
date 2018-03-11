using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones.Pipelines
{
    internal class StoryboardFutureAnimation : IFutureAnimation
    {
        public Timeline Timeline { get; }

        public StoryboardFutureAnimation(Timeline timeline)
        {
            Timeline = timeline ?? throw new ArgumentNullException(nameof(timeline));
        }

        public void Begin(FrameworkElement element, Action onCompleted)
        {
            var storyboard = new Storyboard { Children = { Timeline } };
            storyboard.Completed += (s, a) => onCompleted();
            element.BeginStoryboard(storyboard);
        }

        public static implicit operator StoryboardFutureAnimation(Timeline timeline) => new StoryboardFutureAnimation(timeline);
    }
}
