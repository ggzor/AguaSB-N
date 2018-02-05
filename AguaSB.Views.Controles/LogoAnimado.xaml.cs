using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Controles
{
    public partial class LogoAnimado : UserControl
    {
        public LogoAnimado()
        {
            InitializeComponent();
        }

        public bool Start
        {
            get { return (bool)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(bool), typeof(LogoAnimado),
                new PropertyMetadata(false, (s, a) => ((LogoAnimado)s).TryStartAnimation((bool)a.NewValue)));

        private void TryStartAnimation(bool start)
        {
            if (start)
            {
                var pathAnimations = CreatePathAnimations();
                var textAnimations = CreateTextAnimations();

                Path.BeginStoryboard(pathAnimations);
                Text.BeginStoryboard(textAnimations);
            }
        }

        private static readonly QuadraticEase quadraticOut = new QuadraticEase { EasingMode = EasingMode.EaseOut };
        private static readonly TimeSpan movementDuration = TimeSpan.FromSeconds(0.5);

        private DoubleAnimation NewMovementAnimation(double beginTime, double to, string path)
        {
            var animation = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromSeconds(beginTime),
                EasingFunction = quadraticOut,
                Duration = movementDuration,
                To = to
            };
            Storyboard.SetTargetProperty(animation, new PropertyPath(path));

            return animation;
        }

        private Storyboard CreatePathAnimations()
        {
            var rotateAnimation = new DoubleAnimation
            {
                BeginTime = TimeSpan.FromSeconds(1),
                EasingFunction = new BackEase { Amplitude = 0.8, EasingMode = EasingMode.EaseInOut },
                Duration = TimeSpan.FromSeconds(1.2),
                To = 0.0
            };
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("RenderTransform.Children[0].Angle"));

            var moveAnimation = NewMovementAnimation(
                beginTime: 1.5,
                to: 0.0,
                path: "RenderTransform.Children[1].X");

            return new Storyboard { Children = { rotateAnimation, moveAnimation } };
        }

        private Storyboard CreateTextAnimations()
        {
            var maskAnimation = NewMovementAnimation(
                                beginTime: 1.525,
                                to: 122,
                                path: "OpacityMask.Visual.Width");

            var textAnimation = NewMovementAnimation(
                beginTime: 1.5,
                to: 0.0,
                path: "RenderTransform.X");

            return new Storyboard { Children = { maskAnimation, textAnimation } };
        }
    }
}
