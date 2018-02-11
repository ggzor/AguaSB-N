using System.Windows;

namespace AguaSB.Views
{
    public interface IView
    {
        FrameworkElement View { get; }

        void Entrar();
    }
}
