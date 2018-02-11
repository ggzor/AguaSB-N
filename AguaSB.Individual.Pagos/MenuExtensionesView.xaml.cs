using System.Windows;
using System.Windows.Controls;

using ReactiveUI;

namespace AguaSB.Individual.Pagos
{
    public partial class MenuExtensionesView : UserControl, IViewFor<MenuExtensiones>
    {
        public MenuExtensionesView()
        {
            InitializeComponent();
        }

        #region IViewFor
        public MenuExtensiones ViewModel
        {
            get { return (MenuExtensiones)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(MenuExtensiones), typeof(MenuExtensionesView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MenuExtensiones)value;
        }
        #endregion
    }
}
