using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AguaSB.Extensiones.Views.Menu
{
    public partial class ExtensionMenuView : UserControl
    {
        public IObservable<ExtensionMenuView> Seleccionada { get; }

        public ExtensionMenuView()
        {
            InitializeComponent();
            Seleccionada = Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(e => Boton.Click += e, e => Boton.Click -= e)
                .Select(e => (ExtensionMenuView)e.Sender);
        }

        public IExtensionMenu Extension
        {
            get { return (IExtensionMenu)GetValue(ExtensionProperty); }
            set { SetValue(ExtensionProperty, value); }
        }

        public static readonly DependencyProperty ExtensionProperty =
            DependencyProperty.Register(nameof(Extension), typeof(IExtensionMenu), typeof(ExtensionMenuView), new PropertyMetadata(null));
    }
}
