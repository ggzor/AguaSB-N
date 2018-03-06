using System;
using System.Windows;
using System.Windows.Media.Animation;

using AguaSB.Views.Animaciones.Definition;
using AguaSB.Views.Animaciones.Implementation;

namespace AguaSB.Views.Animaciones
{
    public static class Scale
    {
        public static readonly TimeSpan DefaultDelay = TimeSpan.Zero;
        public static readonly Duration DefaultDuration = TimeSpan.FromSeconds(1);
        public static readonly IEasingFunction DefaultEasing = new QuadraticEase { EasingMode = EasingMode.EaseOut };

        public static readonly double DefaultFrom = 0.0;
        public static readonly double DefaultTo = 1.0;

        public static readonly IUniformScalerBuilder Uniform = new GenericUniformScaler(DefaultDelay, DefaultDuration, DefaultEasing, DefaultFrom, DefaultTo);
    }
}
