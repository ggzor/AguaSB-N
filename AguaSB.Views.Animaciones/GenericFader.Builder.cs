using System.Windows;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones
{
    internal sealed partial class GenericFader : IFaderBuilder
    {
        public IFaderBuilder WithDuration(Duration newDuration) =>
            new GenericFader(newDuration, Delay, Easing, InitialVisibility, InitialOpacity, TargetVisibility, TargetOpacity);

        public IFaderBuilder WithEasing(IEasingFunction newEasing) =>
            new GenericFader(Duration, Delay, newEasing, InitialVisibility, InitialOpacity, TargetVisibility, TargetOpacity);
    }
}
