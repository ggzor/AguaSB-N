using System.Windows;

using AguaSB.Views.Animaciones.Pipelines;

namespace AguaSB.Views.Animaciones
{
    public interface ISingleElementAnimation
    {
        IFutureAnimation Create(FrameworkElement element);
    }
}
