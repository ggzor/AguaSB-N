using System.Windows;

using AguaSB.Extensiones;
using AguaSB.Views;

namespace AguaSB.Pagos.Views
{
    public interface IExtensionPrincipalPagos : IView, IExtension
    {
        string Titulo { get; }
        FrameworkElement Icono { get; }

        string Ruta { get; }
    }
}
