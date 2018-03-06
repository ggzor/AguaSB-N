using System.Windows;

namespace AguaSB.Views.Animaciones.Definition
{
    public interface ISingleElementAnimation
    {
        IFutureAnimation Create(FrameworkElement element);
    }
}
