using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

using ReactiveUI;

using AguaSB.Views.Conversores.Reactive;
using AguaSB.Views.Utilerias;
using System.Windows.Media.Animation;
using System.Windows.Media;
using AguaSB.Views.Controles.Animaciones;
using AguaSB.Compartido.ViewModels;

namespace AguaSB.Compartido.Views
{
    public partial class AutenticacionPorUsuarioView : UserControl, IViewFor<AutenticacionPorUsuario>, IFocusable
    {
        public AutenticacionPorUsuarioView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.Usuario, v => v.Usuario.Text));
                d(this.Bind(ViewModel, vm => vm.Clave, v => v.Clave.Password,
                    Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(e => Clave.PasswordChanged += e, e => Clave.PasswordChanged -= e)));

                d(this.OneWayBind(ViewModel, vm => vm.Errores, v => v.Errores.Text));
                d(this.OneWayBind(ViewModel, vm => vm.TieneErrores, v => v.Errores.Visibility, BoolToVisibility.Convert));

                d(this.BindCommand(ViewModel, v => v.Autenticar, v => v.Boton));

                d(this.WhenAnyObservable(v => v.ViewModel.Autenticar).Subscribe(s => Completar()));
            });
        }

        private void Completar()
        {
            var animacion = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.50),
                EasingFunction = new BackEase { Amplitude = 0.3, EasingMode = EasingMode.EaseOut },
                To = (Contenido.ActualHeight / 2) - 30.0
            };

            animacion.Completed += (s, a) => Logo.Start = true;

            FadeOut.SetDuration(Interfaz, TimeSpan.FromSeconds(0.5));
            FadeOut.Apply(Interfaz,
                onCompleted: () => Logo.RenderTransform.BeginAnimation(TranslateTransform.YProperty, animacion));
        }

        public void DoFocus() => Usuario.Focus();

        #region IViewFor
        public AutenticacionPorUsuario ViewModel
        {
            get { return (AutenticacionPorUsuario)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(AutenticacionPorUsuario), typeof(AutenticacionPorUsuarioView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (AutenticacionPorUsuario)value;
        }
        #endregion
    }
}
