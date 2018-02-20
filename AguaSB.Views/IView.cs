using System.Windows;

using AguaSB.Views.Utilerias;

namespace AguaSB.Views
{
    public interface IView : IFocusable
    {
        FrameworkElement View { get; }
    }
}
