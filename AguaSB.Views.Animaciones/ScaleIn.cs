using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using AguaSB.Views.Animaciones.Pipelines;
using static AguaSB.Views.Animaciones.Pipelines.PropPath;

namespace AguaSB.Views.Animaciones
{
    public static class ScaleIn
    {
        public const double InitialScale = 0.85;
        public const double InitialOpacity = 0.2;
        public const double RenderTransformOrigin = 0.5;

        public static readonly TimeSpan Duration = TimeSpan.FromMilliseconds(500);
        public static readonly IEasingFunction EasingFunction = new PowerEase() { Power = 5, EasingMode = EasingMode.EaseOut };
        private static DoubleAnimation DoubleAnimationTo(double to) => new DoubleAnimation(to, Duration) { EasingFunction = EasingFunction };

        private static readonly PropertyPath RenderTransformPath = For<FrameworkElement, Transform>(f => f.RenderTransform);
        private static readonly PropertyPath ScaleXPath = Concat(RenderTransformPath, For<ScaleTransform, double>(s => s.ScaleX));
        private static readonly PropertyPath ScaleYPath = Concat(RenderTransformPath, For<ScaleTransform, double>(s => s.ScaleY));
        private static readonly PropertyPath OpacityPath = For<FrameworkElement, double>(f => f.Opacity);
        private static readonly PropertyPath[] Paths = new[] { ScaleXPath, ScaleYPath, OpacityPath };

        public static IFutureAnimation Create(FrameworkElement elem)
        {
            void SetUpElement()
            {
                elem.Opacity = InitialOpacity;
                elem.RenderTransform = new ScaleTransform { ScaleX = InitialScale, ScaleY = InitialScale };
                elem.RenderTransformOrigin = new Point(RenderTransformOrigin, RenderTransformOrigin);
                elem.Visibility = Visibility.Visible;
            }

            Timeline CreateAnimations()
            {
                var timelines = Paths.Select(p => (p, Animation: DoubleAnimationTo(1))).ToArray();

                foreach (var (path, animation) in timelines)
                {
                    Storyboard.SetTarget(animation, elem);
                    Storyboard.SetTargetProperty(animation, path);
                }

                return new Storyboard { Children = new TimelineCollection(timelines.Select(t => t.Animation)) };
            }

            return new FutureAnimation(
                preAction: SetUpElement,
                timeline: CreateAnimations());
        }

        public static void Apply(FrameworkElement elem) =>
            elem.Begin(Create(elem));
    }
}
