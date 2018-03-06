using System.Windows;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones
{
    internal sealed partial class GenericFader : IFaderBuilder
    {
        public IFaderBuilder WithDuration(Duration newDuration) =>
            new GenericFader(Delay, newDuration, Easing, InitialVisibility, InitialOpacity, TargetVisibility, TargetOpacity);

        public IFaderBuilder WithEasing(IEasingFunction newEasing) =>
            new GenericFader(Delay, Duration, newEasing, InitialVisibility, InitialOpacity, TargetVisibility, TargetOpacity);
    }
}
