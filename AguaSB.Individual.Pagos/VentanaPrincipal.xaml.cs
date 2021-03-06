﻿using System;
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
using AguaSB.Views.Conversores.Reactive;

namespace AguaSB.Individual.Pagos
{
    public partial class VentanaPrincipal : MetroWindow, IViewFor<VentanaPrincipalViewModel>, IVentana
    {
        public VentanaPrincipal(VentanaPrincipalViewModel viewModel, AutenticacionPorUsuario autenticacion, IProveedorExtensionMenuView proveedorExtensionMenuView)
        {
            DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            InitializeComponent();

            var autenticacionView = new IniciarSesionView(autenticacion);
            var extensionesView = new PrincipalView(
                ViewModel.Cargar.Select(a => a.Obtener<IExtensionMenu>().ToArray()),
                proveedorExtensionMenuView,
                ViewModel);

            PanelPrincipal.Children.Add(autenticacionView);
            var navegador = new NavegadorViewsPrincipales(this, PanelPrincipal);

            Activated += (s, a) => autenticacionView.DoFocus();

            (this).WhenActivated(d =>
            {
                (this).WhenAnyObservable(v => v.ViewModel.Autenticacion.Autenticar)
                    .SelectMany(c => Observable.Return(c).Delay(TimeSpan.FromSeconds(3.8)))
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

                this.OneWayBind(ViewModel, vm => vm.BackNavigation.IsEnabled, v => v.PanelBotonAtras.Visibility, BoolToVisibility.Convert)
                    .DisposeWith(d);

                this.BindCommand(ViewModel, vm => vm.BackNavigation.Execute, v => v.BotonAtras)
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
