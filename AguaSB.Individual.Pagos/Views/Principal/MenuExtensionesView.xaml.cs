using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

using ReactiveUI;

namespace AguaSB.Individual.Pagos.Views.Principal
{
    public partial class MenuExtensionesView : UserControl, IViewFor<MenuExtensiones>
    {
        public MenuExtensionesView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                this.WhenAnyValue(v => v.ViewModel)
                    .Subscribe(vm => IconosMenu.ItemsSource = vm.ExtensionesViews.Select(e => e.View))
                    .DisposeWith(d);
            });
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
