using System;
using System.Windows;
using System.Windows.Media.Animation;

using AguaSB.Views.Animaciones.Pipelines;

namespace AguaSB.Views.Animaciones
{
    internal sealed partial class GenericFader : IFaderBuilder
    {
        public TimeSpan? Delay { get; }
        public Duration Duration { get; }
        public IEasingFunction Easing { get; }

        public Visibility InitialVisibility { get; }
        public double InitialOpacity { get; }

        public Visibility TargetVisibility { get; }
        public double TargetOpacity { get; }

        public GenericFader(TimeSpan? delay, Duration duration, IEasingFunction easing, Visibility initialVisibility, double initialOpacity, Visibility targetVisibility, double targetOpacity)
        {
            Delay = delay;
            Duration = duration;
            Easing = easing ?? throw new ArgumentNullException(nameof(easing));
            InitialVisibility = initialVisibility;
            InitialOpacity = initialOpacity;
            TargetVisibility = targetVisibility;
            TargetOpacity = targetOpacity;
        }

        private static readonly PropertyPath OpacityPath = PropPath.For<FrameworkElement, double>(e => e.Opacity);

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
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, OpacityPath);

            void PostAction() => element.Visibility = TargetVisibility;

            return animation
                .ToFutureAnimation()
                .Before(SetUpElement)
                .Then(PostAction);
        }

        public void Apply(FrameworkElement element) =>
            Create(element).BeginIn(element);
    }
}
