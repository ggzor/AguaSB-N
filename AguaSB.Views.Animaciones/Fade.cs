using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones
{
    public static partial class Fade
    {
        public static readonly Duration DefaultDuration = TimeSpan.FromSeconds(1);
        public static readonly TimeSpan? DefaultDelay = default(TimeSpan?);
        public static readonly IEasingFunction DefaultEasing = new QuadraticEase { EasingMode = EasingMode.EaseOut };

        public static readonly IFader In = new GenericFader(DefaultDuration, DefaultDelay, DefaultEasing, Visibility.Visible, 0.0, Visibility.Visible, 1.0);
        public static readonly IFader Out = new GenericFader(DefaultDuration, DefaultDelay, DefaultEasing, Visibility.Visible, 1.0, Visibility.Hidden, 0.0);
    }
}
