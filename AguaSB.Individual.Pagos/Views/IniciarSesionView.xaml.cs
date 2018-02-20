using System.Windows;
using System.Windows.Controls;

using AguaSB.Compartido.ViewModels;
using AguaSB.Views.Estilos.Modern.Ventanas;

namespace AguaSB.Individual.Pagos.Views
{
    public partial class IniciarSesionView : UserControl, IViewPrincipal
    {
        public IniciarSesionView(AutenticacionPorUsuario viewModel)
        {
            InitializeComponent();
            Autenticacion.ViewModel = viewModel;
        }

        public EsquemaBarraTitulo EsquemaBarraTitulo => EsquemasBarraTitulo.PrimarioObscuro;
        public FrameworkElement View => this;

        public void DoFocus() => Autenticacion.DoFocus();
    }
}
