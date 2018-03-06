using System.Windows;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones.Definition
{
    public interface IAnimationBuilder<T>
    {
        T WithDuration(Duration newDuration);
        T WithEasing(IEasingFunction newEasing);
    }
}
