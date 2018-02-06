using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

using ReactiveUI;

using AguaSB.Compartido.Interfaces;
using AguaSB.Views.Conversores.Reactive;

namespace AguaSB.Compartido.Views
{
    public partial class IniciarSesion : UserControl, IViewFor<IInicioSesion>
    {
        public IniciarSesion()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.Usuario, v => v.Usuario.Text));
                d(this.Bind(ViewModel, vm => vm.Clave, v => v.Clave.Password,
                    Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(e => Clave.PasswordChanged += e, e => Clave.PasswordChanged -= e)));

                d(this.OneWayBind(ViewModel, vm => vm.Errores, v => v.Errores.Text));
                d(this.OneWayBind(ViewModel, vm => vm.TieneErrores, v => v.Errores.Visibility, BoolToVisibility.Convert));

                d(this.BindCommand(ViewModel, v => v.IniciarSesion, v => v.Boton));
            });
        }

        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (IInicioSesion)value; }

        public IInicioSesion ViewModel
        {
            get { return (IInicioSesion)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(IInicioSesion), typeof(IniciarSesion), new PropertyMetadata(null));
    }
}
