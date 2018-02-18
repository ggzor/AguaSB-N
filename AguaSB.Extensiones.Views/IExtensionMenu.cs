using System;
using System.Windows;

using AguaSB.Views.Navegacion;

namespace AguaSB.Extensiones.Views
{
    public interface IExtensionMenu : IExtensionView
    {
        IObservable<IClaveNavegacion> Navegacion { get; }

        FrameworkElement Icono { get; }

        string Titulo { get; }

        string Descripcion { get; }
    }
}
