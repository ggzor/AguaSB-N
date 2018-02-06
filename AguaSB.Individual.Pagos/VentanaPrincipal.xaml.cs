using System;
using System.Windows;

using MahApps.Metro.Controls;
using ReactiveUI;

using AguaSB.Views;
using AguaSB.Compartido.Interfaces;

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
