﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones
{
    public static class ScaleIn
    {
        public static void Apply(FrameworkElement elem, Action onCompleted = null)
        {
            SetUpElement(elem);
            BeginAnimations(elem, onCompleted);
        }

        public const double InitialScale = 0.85;
        public const double InitialOpacity = 0.2;
        public const double RenderTransformOrigin = 0.5;

        public static readonly TimeSpan Duration = TimeSpan.FromMilliseconds(500);
        public static readonly IEasingFunction EasingFunction = new PowerEase() { Power = 5, EasingMode = EasingMode.EaseOut };
        private static DoubleAnimation DoubleAnimationTo(double to) => new DoubleAnimation(to, Duration) { EasingFunction = EasingFunction };

        private static void SetUpElement(FrameworkElement elem)
        {
            elem.Opacity = InitialOpacity;
            elem.RenderTransform = new ScaleTransform { ScaleX = InitialScale, ScaleY = InitialScale };
            elem.RenderTransformOrigin = new Point(RenderTransformOrigin, RenderTransformOrigin);
            elem.Visibility = Visibility.Visible;
        }

        private static void BeginAnimations(FrameworkElement elem, Action onCompleted)
        {
            var animaciones = new Storyboard() { Children = SetUpAnimations() };

            if (onCompleted != null)
                animaciones.Completed += (s, a) => onCompleted.Invoke();

            animaciones.Begin(elem);
        }

        private static TimelineCollection SetUpAnimations()
        {
            var scaleXAnim = DoubleAnimationTo(1);
            var scaleYAnim = DoubleAnimationTo(1);
            var fadeIn = DoubleAnimationTo(1);

            scaleXAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath($"{nameof(FrameworkElement.RenderTransform)}.{nameof(ScaleTransform.ScaleX)}"));
            scaleYAnim.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath($"{nameof(FrameworkElement.RenderTransform)}.{nameof(ScaleTransform.ScaleY)}"));
            fadeIn.SetValue(Storyboard.TargetPropertyProperty, new PropertyPath(nameof(FrameworkElement.Opacity)));

            return new TimelineCollection { scaleXAnim, scaleYAnim, fadeIn };
        }
    }
}
