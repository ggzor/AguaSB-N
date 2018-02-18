using System;
using System.Windows;

using MahApps.Metro.IconPacks;

using AguaSB.Extensiones.Views;
using AguaSB.Extensiones.Views.Menu;
using AguaSB.Views;
using AguaSB.Views.Navegacion;

namespace AguaSB.Pagos.Views
{
    public class Extension : IExtensionMenu
    {
        public IObservable<IClaveNavegacion> Navegacion => throw new NotImplementedException();

        public IView View { get; }

        public FrameworkElement Icono => IconoMenuEstatico.Crear(PackIconFontAwesomeKind.DollarSignSolid);

        public string Titulo => "Pagos";

        public string Descripcion => "Cobrar, dinero ingresado, reporte del mes";

        public Extension(Principal principal)
        {
            View = principal;
        }
    }
}
