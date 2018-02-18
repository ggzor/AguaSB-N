using AguaSB.Views;

namespace AguaSB.Extensiones.Views
{
    public interface IExtensionView : IExtension
    {
        IView View { get; }
    }
}
