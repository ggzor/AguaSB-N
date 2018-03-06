using System;
using System.Windows;

namespace AguaSB.Views.Animaciones
{
    public interface IFutureAnimation
    {
        void Begin(FrameworkElement element, Action onCompleted);
    }
}
