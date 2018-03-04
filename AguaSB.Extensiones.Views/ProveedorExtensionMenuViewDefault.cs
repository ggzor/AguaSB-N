namespace AguaSB.Extensiones.Views
{
    public class ProveedorExtensionMenuViewDefault : IProveedorExtensionMenuView
    {
        public IExtensionMenuView Para(IExtensionMenu extension) =>
            new ExtensionMenuViewDefault { Extension = extension };
    }
}
