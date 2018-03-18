using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

using AguaSB.Extensiones.Views;
using AguaSB.Extensiones.Views.Implementacion;

namespace AguaSB.Individual.Pagos.Views.Principal
{
    public class MenuExtensiones
    {
        public IReadOnlyCollection<IExtensionMenu> Extensiones { get; }
        public IProveedorExtensionMenuView ProveedorExtensionMenuView { get; }

        public MenuExtensiones(IReadOnlyCollection<IExtensionMenu> extensiones, IProveedorExtensionMenuView proveedorExtensionMenuView)
        {
            Extensiones = extensiones ?? throw new ArgumentNullException(nameof(extensiones));
            ProveedorExtensionMenuView = proveedorExtensionMenuView ?? throw new ArgumentNullException(nameof(proveedorExtensionMenuView));

            ExtensionesViews = Extensiones.Select(proveedorExtensionMenuView.Para).ToArray();
            ExtensionSeleccionada = ExtensionesViews.Select(e => e.Seleccionada.Select(s => (e.Extension, s))).Merge();
        }

        public IReadOnlyCollection<IExtensionMenuView> ExtensionesViews { get; }
        public IObservable<(IExtensionMenu Extension, object Parametro)> ExtensionSeleccionada { get; }
    }
}
