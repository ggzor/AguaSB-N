using System;
using System.Collections.Generic;

using AguaSB.Extensiones.Views;

using ReactiveUI;

namespace AguaSB.Individual.Pagos.Views.Principal
{
    public class MenuExtensiones : ReactiveObject
    {
        private IReadOnlyCollection<IExtensionMenu> extensiones;

        public IReadOnlyCollection<IExtensionMenu> Extensiones
        {
            get { return extensiones; }
            set { this.RaiseAndSetIfChanged(ref extensiones, value); }
        }

        public MenuExtensiones(IReadOnlyCollection<IExtensionMenu> extensiones)
        {
            Extensiones = extensiones ?? throw new ArgumentNullException(nameof(extensiones));
        }
    }
}
