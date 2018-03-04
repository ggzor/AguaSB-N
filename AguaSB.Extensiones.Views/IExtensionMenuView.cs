using System;
using System.Windows;

namespace AguaSB.Extensiones.Views
{
    public interface IExtensionMenuView
    {
        FrameworkElement View { get; }

        IExtensionMenu Extension { get; }

        IObservable<object> Seleccionada { get; }
    }
}
