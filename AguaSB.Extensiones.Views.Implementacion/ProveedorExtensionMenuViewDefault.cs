namespace AguaSB.Extensiones.Views.Implementacion
{
    public class ProveedorExtensionMenuViewDefault : IProveedorExtensionMenuView
    {
        public IExtensionMenuView Para(IExtensionMenu extension) =>
            new ExtensionMenuViewDefault { Extension = extension };
    }
}
