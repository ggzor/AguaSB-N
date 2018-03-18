using System;
using System.Windows;

namespace AguaSB.Views.Animaciones.Definition
{
    public interface IActionDelayer
    {
        IActionDelayer For(Duration newDuration);
        IFutureAnimation Create(Action action);
    }
}
