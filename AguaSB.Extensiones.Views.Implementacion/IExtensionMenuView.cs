using System;
using System.Windows;

namespace AguaSB.Extensiones.Views.Implementacion
{
    public interface IExtensionMenuView
    {
        FrameworkElement View { get; }

        IExtensionMenu Extension { get; }

        IObservable<object> Seleccionada { get; }
    }
}
