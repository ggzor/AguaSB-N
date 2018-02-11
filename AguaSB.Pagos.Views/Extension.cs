using AguaSB.Extensiones.Views;
using AguaSB.Views;

using System.Windows;
using System.Windows.Controls;

namespace AguaSB.Pagos.Views
{
    public class Extension : IExtensionView
    {
        public IView View { get; }

        public FrameworkElement Icono => new Button { Content = "Pagos" };

        public Extension(Principal principal)
        {
            View = principal;
        }
    }
}
