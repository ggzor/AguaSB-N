using System;
using System.Windows;

namespace AguaSB.Views.Animaciones.Pipelines
{
    public interface IFutureAnimation
    {
        void Begin(FrameworkElement element, Action onCompleted);
    }
}
