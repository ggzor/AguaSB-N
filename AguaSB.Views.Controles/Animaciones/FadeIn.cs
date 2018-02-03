using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Controles.Animaciones
{
    public static class FadeIn
    {
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.RegisterAttached("Delay", typeof(TimeSpan?), typeof(FadeIn), new PropertyMetadata(TimeSpan.Zero));

        public static void SetDelay(FrameworkElement elem, TimeSpan? val) => elem.SetValue(DelayProperty, val);
        public static TimeSpan? GetDelay(FrameworkElement elem) => (TimeSpan?)elem.GetValue(DelayProperty);

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.RegisterAttached("Duration", typeof(TimeSpan), typeof(FadeIn), new PropertyMetadata(FadeCommon.DefaultDuration));

        public static void SetDuration(FrameworkElement elem, TimeSpan val) => elem.SetValue(DurationProperty, val);
        public static TimeSpan GetDuration(FrameworkElement elem) => (TimeSpan)elem.GetValue(DurationProperty);

        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.RegisterAttached("Easing", typeof(IEasingFunction), typeof(FadeIn),
                new PropertyMetadata(FadeCommon.DefaultEase));

        public static void SetEasing(FrameworkElement elem, IEasingFunction val) => elem.SetValue(EasingProperty, val);
        public static IEasingFunction GetEasing(FrameworkElement elem) => (IEasingFunction)elem.GetValue(EasingProperty);

        /// <summary>
        /// Set to false if you want to ignore the animation.
        /// </summary>
        public static readonly DependencyProperty FirstTimeProperty =
            DependencyProperty.RegisterAttached("FirstTime", typeof(bool), typeof(FadeIn), new PropertyMetadata(true));

        public static void SetFirstTime(FrameworkElement elem, bool val) => elem.SetValue(FirstTimeProperty, val);
        public static bool GetFirstTime(FrameworkElement elem) => (bool)elem.GetValue(FirstTimeProperty);

        /// <summary>
        /// Set <see cref="StartWithProperty"/> first if required.
        /// </summary>
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.RegisterAttached("From", typeof(FadeDirection), typeof(FadeIn),
                new PropertyMetadata(FadeCommon.DefaultDirection, (o, a) => Configure((FrameworkElement)o)));

        public static void SetFrom(FrameworkElement elem, FadeDirection direction) => elem.SetValue(FromProperty, direction);
        public static FadeDirection GetFrom(FrameworkElement elem) => (FadeDirection)elem.GetValue(FromProperty);

        /// <summary>
        /// The amount of displacement that is going to be applied in the specified direction.
        /// </summary>
        public static readonly DependencyProperty PushProperty =
            DependencyProperty.RegisterAttached("Push", typeof(double?), typeof(FadeIn), new PropertyMetadata(null));

        public static void SetPush(FrameworkElement elem, double? val) => elem.SetValue(PushProperty, val);
        public static double? GetPush(FrameworkElement elem) => (double?)elem.GetValue(PushProperty);

        /// <summary>
        /// If set to null, animation will start with the <see cref="FrameworkElement.Loaded"/> event.
        /// Intended to be a binding.
        /// </summary>
        public static readonly DependencyProperty StartWithProperty =
            DependencyProperty.RegisterAttached("StartWith", typeof(bool?), typeof(FadeIn),
                new PropertyMetadata(null, (o, a) => TryStart((FrameworkElement)o, (bool)a.NewValue)));

        public static void SetStartWith(FrameworkElement elem, bool? val) => elem.SetValue(StartWithProperty, val);
        public static bool? GetStartWith(FrameworkElement elem) => (bool?)elem.GetValue(StartWithProperty);

        private static void Configure(FrameworkElement o)
        {
            if (GetStartWith(o) == null)
                o.Loaded += (s, a) => Apply(o, GetFrom(o));
        }

        private static void TryStart(FrameworkElement o, bool newValue)
        {
            if (newValue && GetFirstTime(o))
            {
                SetFirstTime(o, false);
                Apply(o, GetFrom(o));
            }
        }

        public static void Apply(FrameworkElement elem, FadeDirection direction = FadeDirection.None, EventHandler onCompleted = null) =>
            FadeCommon.Apply(elem, direction, onCompleted, 1.0, GetPush(elem), GetDelay(elem), GetDuration(elem), GetEasing(elem));
    }
}
