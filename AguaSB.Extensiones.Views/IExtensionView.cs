using AguaSB.Views;
using System.Windows;

namespace AguaSB.Extensiones.Views
{
    public interface IExtensionView : IExtension
    {
        IView View { get; }
    }
}
