using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using AguaSB.Views.Animaciones.Definition;
using static AguaSB.Views.Animaciones.Pipelines.PropPath;

namespace AguaSB.Views.Animaciones.Implementation
{
    internal partial class GenericUniformScaler : IUniformScalerBuilder
    {
        public TimeSpan? Delay { get; }
        public Duration Duration { get; }
        public IEasingFunction Easing { get; }

        public double From { get; }
        public double To { get; }

        public GenericUniformScaler(TimeSpan? delay, Duration duration, IEasingFunction easing, double from, double to)
        {
            Delay = delay;
            Duration = duration;
            Easing = easing ?? throw new ArgumentNullException(nameof(easing));
            From = from;
            To = to;
        }

        public static readonly PropertyPath RenderTransformPath = For<FrameworkElement, Transform>(e => e.RenderTransform);
        public static readonly PropertyPath ScaleXPath = Concat(RenderTransformPath, For<ScaleTransform, double>(s => s.ScaleX));
        public static readonly PropertyPath ScaleYPath = Concat(RenderTransformPath, For<ScaleTransform, double>(s => s.ScaleY));

        public IFutureAnimation Create(FrameworkElement element)
        {
            void SetUpElement()
            {
                var transform = GetOrCreateTransform(element);

                transform.CenterX = 0.5;
                transform.CenterY = 0.5;

                transform.ScaleX = From;
                transform.ScaleY = From;
            }

            var scaleXAnimation = new DoubleAnimation
            {
                BeginTime = Delay,
                Duration = Duration,
                EasingFunction = Easing,
                To = To
            };

            var scaleYAnimation = new DoubleAnimation
            {
                BeginTime = Delay,
                Duration = Duration,
                EasingFunction = Easing,
                To = To
            };

            Storyboard.SetTarget(scaleXAnimation, element);
            Storyboard.SetTargetProperty(scaleXAnimation, ScaleXPath);

            Storyboard.SetTarget(scaleYAnimation, element);
            Storyboard.SetTargetProperty(scaleYAnimation, ScaleYPath);

            return scaleXAnimation.ToFutureAnimation()
                .Pair(scaleYAnimation.ToFutureAnimation())
                .Before(SetUpElement);
        }

        private static ScaleTransform GetOrCreateTransform(FrameworkElement element)
        {
            if (element.RenderTransform is ScaleTransform transform)
            {
                return transform;
            }
            else
            {
                var scaleTransform = new ScaleTransform();
                element.RenderTransform = scaleTransform;
                return scaleTransform;
            }
        }
    }
}