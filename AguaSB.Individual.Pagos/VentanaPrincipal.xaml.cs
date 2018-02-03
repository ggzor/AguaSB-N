using System;
using System.Reactive.Linq;
using System.Windows;

using MahApps.Metro.Controls;
using ReactiveUI;

using AguaSB.Views;
using AguaSB.Views.Controles.Animaciones;

namespace AguaSB.Individual.Pagos
{
    public partial class VentanaPrincipal : MetroWindow, IViewFor<VentanaPrincipalViewModel>, IVentana
    {
        public VentanaPrincipal(VentanaPrincipalViewModel viewModel)
        {
            DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.MensajeCarga, v => v.MensajeCarga.Text));

                d(this.WhenAnyObservable(v => v.ViewModel.ComenzandoCarga)
                    .Subscribe(u => { FadeIn.SetDelay(PanelCarga, TimeSpan.FromSeconds(2.5)); FadeIn.Apply(PanelCarga); }));
                d(this.WhenAnyObservable(v => v.ViewModel.Cargar)
                    .Subscribe(u => FadeOut.Apply(PanelCarga)));

                d(this.WhenAnyValue(v => v.ViewModel.Cargar)
                    .SelectMany(c => c.Execute())
                    .Subscribe());
            });
        }

        public void Mostrar() => ShowDialog();

        #region IViewFor
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (VentanaPrincipalViewModel)value; }

        public VentanaPrincipalViewModel ViewModel
        {
            get { return (VentanaPrincipalViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(VentanaPrincipalViewModel), typeof(VentanaPrincipal), new PropertyMetadata(null));
        #endregion
    }
}
