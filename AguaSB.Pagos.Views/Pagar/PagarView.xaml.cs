using MahApps.Metro.IconPacks;
using System.Windows;
using System.Windows.Controls;

namespace AguaSB.Pagos.Views.Pagar
{
    public partial class PagarView : UserControl, IExtensionPrincipalPagos
    {
        public PagarView()
        {
            InitializeComponent();
        }

        public FrameworkElement View => this;

        public void Entrar(object parametro)
        {
            System.Console.WriteLine("Entrando en pagar view con parametro: " + parametro);
        }

        public string Titulo => "Pagar";
        public string Ruta => Titulo;

        public FrameworkElement Icono => new PackIconMaterialLight { Kind = PackIconMaterialLightKind.CurrencyUsd };
    }
}
