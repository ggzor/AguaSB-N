using MoreLinq;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Controles.Animaciones
{
    public static class FadeOut
    {
        private const double DefaultDuration = 1;

        /// <summary>
        /// Default is (<see cref="FadeDirection.None"/>).
        /// Set <see cref="StartWithProperty"/> first if required.
        /// </summary>
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.RegisterAttached("From", typeof(FadeDirection), typeof(FadeOut),
                new PropertyMetadata(FadeDirection.None, (o, a) => Configure((FrameworkElement)o)));

        public static void SetFrom(FrameworkElement elem, FadeDirection direction) => elem.SetValue(FromProperty, direction);
        public static FadeDirection GetFrom(FrameworkElement elem) => (FadeDirection)elem.GetValue(FromProperty);

        /// <summary>
        /// If set to null, animation will start with the <see cref="FrameworkElement.Loaded"/> event.
        /// Intended to be a binding.
        /// </summary>
        public static readonly DependencyProperty StartWithProperty =
            DependencyProperty.RegisterAttached("StartWith", typeof(bool?), typeof(FadeOut),
                new PropertyMetadata(null, (o, a) => TryStart((FrameworkElement)o, (bool)a.NewValue)));

        public static void SetStartWith(FrameworkElement elem, bool? val) => elem.SetValue(StartWithProperty, val);
        public static bool? GetStartWith(FrameworkElement elem) => (bool?)elem.GetValue(StartWithProperty);

        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.RegisterAttached("Delay", typeof(TimeSpan?), typeof(FadeOut), new PropertyMetadata(TimeSpan.Zero));

        public static void SetDelay(FrameworkElement elem, TimeSpan? val) => elem.SetValue(DelayProperty, val);
        public static TimeSpan? GetDelay(FrameworkElement elem) => (TimeSpan?)elem.GetValue(DelayProperty);

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.RegisterAttached("Duration", typeof(TimeSpan), typeof(FadeOut), new PropertyMetadata(TimeSpan.FromSeconds(DefaultDuration)));

        public static void SetDuration(FrameworkElement elem, TimeSpan val) => elem.SetValue(DurationProperty, val);
        public static TimeSpan GetDuration(FrameworkElement elem) => (TimeSpan)elem.GetValue(DurationProperty);

        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.RegisterAttached("Easing", typeof(IEasingFunction), typeof(FadeOut),
                new PropertyMetadata(new QuadraticEase { EasingMode = EasingMode.EaseOut }));

        public static void SetEasing(FrameworkElement elem, IEasingFunction val) => elem.SetValue(EasingProperty, val);
        public static IEasingFunction GetEasing(FrameworkElement elem) => (IEasingFunction)elem.GetValue(EasingProperty);

        /// <summary>
        /// Set to false if you want to ignore the animation.
        /// </summary>
        public static readonly DependencyProperty FirstTimeProperty =
            DependencyProperty.RegisterAttached("FirstTime", typeof(bool), typeof(FadeOut), new PropertyMetadata(true));

        public static void SetFirstTime(FrameworkElement elem, bool val) => elem.SetValue(FirstTimeProperty, val);
        public static bool GetFirstTime(FrameworkElement elem) => (bool)elem.GetValue(FirstTimeProperty);

        /// <summary>
        /// The amount of displacement that is going to be applied in the specified direction.
        /// </summary>
        public static readonly DependencyProperty PushProperty =
            DependencyProperty.RegisterAttached("Push", typeof(double?), typeof(FadeOut), new PropertyMetadata(null));

        public static void SetPush(FrameworkElement elem, double? val) => elem.SetValue(PushProperty, val);
        public static double? GetPush(FrameworkElement elem) => (double?)elem.GetValue(PushProperty);

        private static void Configure(FrameworkElement o)
        {
            if (GetStartWith(o) == null)
                o.Loaded += (s, a) => o.Apply(GetFrom(o));
        }

        private static void TryStart(FrameworkElement o, bool newValue)
        {
            if (newValue && !GetFirstTime(o))
            {
                SetFirstTime(o, false);
                o.Apply(GetFrom(o));
            }
        }

        public static void Apply(this FrameworkElement elem, FadeDirection direction = FadeDirection.None, EventHandler onComplete = null)
        {
            var fadeAnimation = new DoubleAnimation
            {
                BeginTime = GetDelay(elem),
                Duration = GetDuration(elem),
                EasingFunction = GetEasing(elem),
                To = 0.0
            };

            var (amount, dir) = GeneratePush(elem, direction);
            var pushAnimation = new DoubleAnimation
            {
                BeginTime = GetDelay(elem),
                Duration = GetDuration(elem),
                EasingFunction = GetEasing(elem),
                To = amount
            };

            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(nameof(elem.Opacity)));
            Storyboard.SetTargetProperty(pushAnimation, new PropertyPath($"{nameof(elem.RenderTransform)}{TryFindTranslateTransformPath(elem)}{dir}"));

            var storyboard = new Storyboard
            {
                Children = { fadeAnimation }
            };

            if (direction != FadeDirection.None)
                storyboard.Children.Add(pushAnimation);

            if (onComplete != null)
                storyboard.Completed += onComplete;

            elem.BeginStoryboard(storyboard);
        }

        private static (double Amount, string Direction) GeneratePush(FrameworkElement elem, FadeDirection direction)
        {
            var push = GetPush(elem) ?? elem.ActualHeight;
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

        private static string TryFindTranslateTransformPath(FrameworkElement elem)
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
                    throw new InvalidOperationException("There is more than one translate transform in the specified object.");
            }
            else
                throw new NotImplementedException("Deeply nested transforms are not supported.");
        }
    }
}
