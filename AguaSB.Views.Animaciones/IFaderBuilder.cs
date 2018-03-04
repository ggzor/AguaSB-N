using System.Windows;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones
{
    public interface IFaderBuilder : IFader
    {
        IFaderBuilder WithDuration(Duration newDuration);
        IFaderBuilder WithEasing(IEasingFunction newEasing);
    }
}
