using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

using MahApps.Metro.Controls;
using ReactiveUI;

using AguaSB.Compartido.ViewModels;
using AguaSB.Extensiones.Views;
using AguaSB.Individual.Pagos.Views;

using AguaSB.Views;
using AguaSB.Views.Animaciones;
using AguaSB.Views.Animaciones.Pipelines;

namespace AguaSB.Individual.Pagos
{
    public partial class VentanaPrincipal : MetroWindow, IViewFor<VentanaPrincipalViewModel>, IVentana
    {
        public VentanaPrincipal(VentanaPrincipalViewModel viewModel, AutenticacionPorUsuario autenticacion)
        {
            DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            InitializeComponent();

            var autenticacionView = new IniciarSesionView(autenticacion);
            var extensionesView = new PrincipalView(
                ViewModel.Cargar.Select(a => a.Obtener<IExtensionMenu>().ToArray()));

            PanelPrincipal.Children.Add(autenticacionView);
            var navegador = new NavegadorViewsPrincipales(this, PanelPrincipal);

            Activated += (s, a) => autenticacionView.DoFocus();

            (this).WhenActivated(d =>
            {
                (this).WhenAnyObservable(v => v.ViewModel.Autenticacion.Autenticar)
                    .SelectMany(c => Observable.Return(c).Delay(TimeSpan.FromSeconds(3.5)))
                    .ObserveOnDispatcher()
                    .Subscribe(s => Fade.In.Apply(PanelCarga))
                    .DisposeWith(d);

                (this).WhenAnyObservable(v => v.ViewModel.Cargar)
                     .SelectMany(c => Observable.Return(c).Delay(TimeSpan.FromSeconds(1)))
                     .ObserveOnDispatcher()
                     .Subscribe(u => 
                        Fade.Out
                            .Create(PanelCarga)
                            .Then(() => navegador.IrA(extensionesView))
                            .BeginIn(PanelCarga))
                     .DisposeWith(d);
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
