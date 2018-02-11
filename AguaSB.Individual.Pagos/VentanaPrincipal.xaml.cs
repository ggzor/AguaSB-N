using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

using MahApps.Metro.Controls;
using ReactiveUI;

using AguaSB.Compartido.Interfaces;
using AguaSB.Views;
using AguaSB.Views.Controles.Animaciones;

namespace AguaSB.Individual.Pagos
{
    public partial class VentanaPrincipal : MetroWindow, IViewFor<VentanaPrincipalViewModel>, IVentana
    {
        public IAutenticacion Autenticacion { get; }

        public VentanaPrincipal(VentanaPrincipalViewModel viewModel, IAutenticacion autenticacion)
        {
            DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            Autenticacion = autenticacion ?? throw new ArgumentNullException(nameof(autenticacion));
            InitializeComponent();

            Activated += (s, a) => InicioSesion.DoFocus();

            this.WhenActivated(d =>
            {
                this.WhenAnyObservable(v => v.Autenticacion.Autenticar)
                    .SelectMany(c => Observable.Return(c).Delay(TimeSpan.FromSeconds(3.5)))
                    .ObserveOnDispatcher()
                    .Subscribe(s =>
                    {
                        PanelCarga.Visibility = Visibility.Visible;
                        FadeIn.Apply(PanelCarga);
                    }).DisposeWith(d);

                this.WhenAnyObservable(v => v.ViewModel.Cargar)
                     .SelectMany(c => Observable.Return(c).Delay(TimeSpan.FromSeconds(1)))
                     .ObserveOnDispatcher()
                     .Subscribe(u =>
                     {
                         FadeOut.Apply(PanelCarga,
                             onCompleted: (s, a) => PanelCarga.Visibility = Visibility.Hidden);
                         FadeOut.Apply(InicioSesion,
                             onCompleted: (s, a) =>
                             {
                                 InicioSesion.Visibility = Visibility.Hidden;
                                 MenuExtensiones.Visibility = Visibility.Visible;
                                 FadeIn.Apply(MenuExtensiones);
                             });
                     }).DisposeWith(d);

                this.OneWayBind(ViewModel, vm => vm.MenuExtensiones, v => v.MenuExtensiones.ViewModel).DisposeWith(d);
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
