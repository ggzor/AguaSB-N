using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using MoreLinq;

namespace AguaSB.Views.Controles.Animaciones
{
    internal static class FadeCommon
    {
        public static readonly FadeDirection DefaultDirection = FadeDirection.None;
        public static readonly TimeSpan DefaultDuration = TimeSpan.FromSeconds(1.0);
        public static readonly QuadraticEase DefaultEase = new QuadraticEase { EasingMode = EasingMode.EaseOut };

        public static void Apply(FrameworkElement elem, double targetOpacity, FadeDirection direction, Action onCompleted, EventHandler onCompletedEH,
            double? push, TimeSpan? delay, TimeSpan duration, IEasingFunction easing)
        {
            var fadeAnimation = new DoubleAnimation
            {
                BeginTime = delay,
                Duration = duration,
                EasingFunction = easing,
                To = targetOpacity
            };

            var (amount, dir) = GeneratePush(elem, push, direction);
            var pushAnimation = new DoubleAnimation
            {
                BeginTime = delay,
                Duration = duration,
                EasingFunction = easing,
                To = amount
            };

            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(nameof(elem.Opacity)));
            Storyboard.SetTargetProperty(pushAnimation, new PropertyPath(nameof(elem.RenderTransform) + TryFindTranslateTransformPath(elem) + dir));

            var storyboard = new Storyboard
            {
                Children = { fadeAnimation }
            };

            if (direction != FadeDirection.None)
                storyboard.Children.Add(pushAnimation);

            if (onCompleted != null)
                storyboard.Completed += (s, a) => onCompleted();

            if (onCompletedEH != null)
                storyboard.Completed += onCompletedEH;

            elem.BeginStoryboard(storyboard);
        }

        public static (double Amount, string Direction) GeneratePush(FrameworkElement elem, double? pushNullable, FadeDirection direction)
        {
            var push = pushNullable ?? elem.ActualHeight;
            switch (direction)
            {
                case FadeDirection.Up:
                    return (push, ".Y");
                case FadeDirection.Down:
                    return (-push, ".Y");
                case FadeDirection.None:
                    return (0, string.Empty);
                default:
                    throw new InvalidOperationException("Unknown direction");
            }
        }

        public static string TryFindTranslateTransformPath(FrameworkElement elem)
        {
            if (elem.RenderTransform == null || elem.RenderTransform == Transform.Identity)
            {
                elem.RenderTransform = new TranslateTransform();
                return string.Empty;
            }
            else if (elem.RenderTransform is TranslateTransform t)
            {
                return string.Empty;
            }
            else if (elem.RenderTransform is TransformGroup g)
            {
                var tts = g.Children.OfType<TranslateTransform>();

                if (!tts.Any())
                {
                    var transform = new TranslateTransform();
                    g.Children.Add(transform);

                    return $".Children[{g.Children.IndexOf(transform)}]";
                }
                else if (tts.Exactly(1))
                {
                    return $".Children[{g.Children.IndexOf(tts.Single())}]";
                }
                else
                {
                    throw new InvalidOperationException("There is more than one translate transform in the specified object.");
                }
            }
            else
            {
                throw new InvalidOperationException("Deeply nested transforms are not supported.");
            }
        }
    }
}
