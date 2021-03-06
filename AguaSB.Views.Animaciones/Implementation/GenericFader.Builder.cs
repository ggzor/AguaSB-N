﻿using System.Windows;
using System.Windows.Media.Animation;

using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones.Implementation
{
    internal sealed partial class GenericFader : IFaderBuilder
    {
        public IFaderBuilder WithDuration(Duration newDuration) =>
            new GenericFader(Delay, newDuration, Easing, InitialVisibility, InitialOpacity, TargetVisibility, TargetOpacity);

        public IFaderBuilder WithEasing(IEasingFunction newEasing) =>
            new GenericFader(Delay, Duration, newEasing, InitialVisibility, InitialOpacity, TargetVisibility, TargetOpacity);

        public IFaderBuilder WithFrom(double newFrom) =>
            new GenericFader(Delay, Duration, Easing, InitialVisibility, newFrom, TargetVisibility, TargetOpacity);

        public IFaderBuilder WithTo(double newTo) =>
            new GenericFader(Delay, Duration, Easing, InitialVisibility, InitialOpacity, TargetVisibility, newTo);
    }
}
