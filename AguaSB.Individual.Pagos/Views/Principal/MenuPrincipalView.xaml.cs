using System.Windows;
using System.Windows.Controls;

using AguaSB.Views;

namespace AguaSB.Individual.Pagos.Views.Principal
{
    public partial class MenuPrincipalView : UserControl, IView
    {
        public MenuPrincipalView(MenuExtensiones menuExtensiones)
        {
            InitializeComponent();
            Menu.ViewModel = menuExtensiones;
        }

        public FrameworkElement View => this;

        public void Entrar(object parametro)
        {
            if (parametro == null)
                Busqueda.DoFocus();
        }
    }
}
