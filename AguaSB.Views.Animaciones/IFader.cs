using System.Windows;

using AguaSB.Views.Animaciones.Pipelines;

namespace AguaSB.Views.Animaciones
{
    public interface IFader
    {
        void Apply(FrameworkElement element);
        IFutureAnimation Create(FrameworkElement element);
    }
}
