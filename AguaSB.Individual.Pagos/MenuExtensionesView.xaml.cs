using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

using ReactiveUI;

using AguaSB.Extensiones.Views.Menu;
using AguaSB.Views.Utilerias;

namespace AguaSB.Individual.Pagos
{
    public partial class MenuExtensionesView : UserControl, IViewFor<MenuExtensiones>, IFocusable
    {
        public MenuExtensionesView()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                var viewsObservable = this.WhenAnyValue(v => v.ViewModel.Extensiones)
                    .Select(c => c.Select(e => new ExtensionMenuView { Extension = e }));

                viewsObservable.Subscribe(c => IconosMenu.ItemsSource = c)
                    .DisposeWith(d);

                ExtensionSeleccionada = viewsObservable
                    .Select(c => c.Select(e => e.Seleccionada).Merge());
            });
        }

        public void DoFocus() => Busqueda.Focus();

        public IObservable<IObservable<ExtensionMenuView>> ExtensionSeleccionada { get; private set; }

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
