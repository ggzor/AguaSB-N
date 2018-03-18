using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AguaSB.Extensiones.Views.Implementacion
{
    public partial class ExtensionMenuViewDefault : UserControl, IExtensionMenuView
    {
        public ExtensionMenuViewDefault()
        {
            InitializeComponent();
            Seleccionada = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                e => Boton.Click += e,
                e => Boton.Click -= e)
                .Select(e => (object)null);
        }

        public IObservable<object> Seleccionada { get; }
        public FrameworkElement View => this;

        public IExtensionMenu Extension
        {
            get { return (IExtensionMenu)GetValue(ExtensionProperty); }
            set { SetValue(ExtensionProperty, value); }
        }

        public static readonly DependencyProperty ExtensionProperty =
            DependencyProperty.Register(nameof(Extension), typeof(IExtensionMenu), typeof(ExtensionMenuViewDefault), new PropertyMetadata(null));
    }
}
