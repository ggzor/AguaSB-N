﻿using System;
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
        public IInicioSesion InicioSesionViewModel { get; }

        public VentanaPrincipal(VentanaPrincipalViewModel viewModel, IInicioSesion inicioSesion)
        {
            DataContext = ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            InicioSesionViewModel = inicioSesion ?? throw new ArgumentNullException(nameof(inicioSesion));
            InitializeComponent();

            Activated += (s, a) => InicioSesion.DoFocus();

            this.WhenActivated(d =>
            {
                d(this.WhenAnyObservable(v => v.InicioSesionViewModel.IniciarSesion)
                    .SelectMany(c => Observable.Return(c).Delay(TimeSpan.FromSeconds(3.5)))
                    .ObserveOnDispatcher()
                    .Subscribe(s =>
                    {
                        PanelCarga.Visibility = Visibility.Visible;
                        FadeIn.Apply(PanelCarga);
                    }));

                d(this.WhenAnyObservable(v => v.ViewModel.Cargar)
                    .Subscribe(u => FadeOut.Apply(PanelCarga,
                        onCompleted: (s, a) => PanelCarga.Visibility = Visibility.Hidden)));

                d(this.OneWayBind(ViewModel, vm => vm.ProgresoCarga.Title, v => v.MensajeCarga.Text));
                d(this.OneWayBind(ViewModel, vm => vm.ProgresoCarga.Subtitle, v => v.SubmensajeCarga.Text));
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
