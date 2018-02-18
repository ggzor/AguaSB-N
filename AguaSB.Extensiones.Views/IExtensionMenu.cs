using System.Windows;

namespace AguaSB.Extensiones.Views
{
    public interface IExtensionMenu : IExtensionView
    {
        FrameworkElement Icono { get; }

        string Titulo { get; }

        string Descripcion { get; }
    }
}
