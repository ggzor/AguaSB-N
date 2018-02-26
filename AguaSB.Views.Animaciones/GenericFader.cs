using System;
using System.Windows;
using System.Windows.Media.Animation;

using AguaSB.Views.Animaciones.Pipelines;

namespace AguaSB.Views.Animaciones
{
    internal class GenericFader : IFader
    {
        public Duration Duration { get; }
        public TimeSpan? Delay { get; }
        public IEasingFunction Easing { get; }

        public Visibility InitialVisibility { get; }
        public double InitialOpacity { get; }

        public Visibility TargetVisibility { get; }
        public double TargetOpacity { get; }

        public GenericFader(Duration duration, TimeSpan? delay, IEasingFunction easing,
            Visibility initialVisibility, double initialOpacity, Visibility targetVisibility, double targetOpacity)
        {
            Duration = duration;
            Delay = delay;
            Easing = easing;
            InitialVisibility = initialVisibility;
            InitialOpacity = initialOpacity;
            TargetVisibility = targetVisibility;
            TargetOpacity = targetOpacity;
        }

        public IFutureAnimation Create(FrameworkElement element)
        {
            void SetUpElement()
            {
                element.Visibility = InitialVisibility;
                element.Opacity = InitialOpacity;
            }

            var animation = new DoubleAnimation
            {
                BeginTime = Delay,
                Duration = Duration,
                EasingFunction = Easing,
                To = TargetOpacity
            };

            void PostAction() => element.Visibility = TargetVisibility;

            return new FutureAnimation(
                preAction: SetUpElement,
                timeline: animation,
                postAction: PostAction);
        }

        public void Apply(FrameworkElement element) =>
            Create(element).BeginWith(element);
    }
}
