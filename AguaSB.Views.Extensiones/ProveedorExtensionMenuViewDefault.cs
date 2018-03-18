using AguaSB.Extensiones.Views;

namespace AguaSB.Views.Extensiones
{
    public class ProveedorExtensionMenuViewDefault : IProveedorExtensionMenuView
    {
        public IExtensionMenuView Para(IExtensionMenu extension) =>
            new ExtensionMenuViewDefault { Extension = extension };
    }
}
