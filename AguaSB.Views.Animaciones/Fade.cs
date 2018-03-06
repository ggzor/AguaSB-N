using System;
using System.Windows;
using System.Windows.Media.Animation;

using AguaSB.Views.Animaciones.Definition;
using AguaSB.Views.Animaciones.Implementation;

namespace AguaSB.Views.Animaciones
{
    public static class Fade
    {
        public static readonly TimeSpan DefaultDelay = TimeSpan.Zero;
        public static readonly Duration DefaultDuration = TimeSpan.FromSeconds(1);
        public static readonly IEasingFunction DefaultEasing = new QuadraticEase { EasingMode = EasingMode.EaseOut };

        public static readonly IFaderBuilder In = new GenericFader(DefaultDelay, DefaultDuration, DefaultEasing, Visibility.Visible, 0.0, Visibility.Visible, 1.0);
        public static readonly IFaderBuilder Out = new GenericFader(DefaultDelay, DefaultDuration, DefaultEasing, Visibility.Visible, 1.0, Visibility.Hidden, 0.0);
    }
}
