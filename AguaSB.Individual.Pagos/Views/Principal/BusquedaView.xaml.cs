using System.Windows.Controls;
using AguaSB.Views.Utilerias;

namespace AguaSB.Individual.Pagos.Views.Principal
{
    public partial class BusquedaView : UserControl, IFocusable
    {
        public BusquedaView()
        {
            InitializeComponent();
        }

        public void DoFocus() => Barra.Focus();
    }
}
