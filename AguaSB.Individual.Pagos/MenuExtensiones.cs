using AguaSB.Extensiones.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace AguaSB.Individual.Pagos
{
    public class MenuExtensiones : ReactiveObject
    {
        public IEnumerable<IExtensionMenu> Extensiones { get; }

        public MenuExtensiones(IEnumerable<IExtensionMenu> extensiones)
        {
            Extensiones = extensiones ?? throw new ArgumentNullException(nameof(extensiones));
        }
    }
}
