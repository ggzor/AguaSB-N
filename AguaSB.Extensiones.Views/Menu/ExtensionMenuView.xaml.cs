using System.Windows;
using System.Windows.Controls;

namespace AguaSB.Extensiones.Views.Menu
{
    public partial class ExtensionMenuView : UserControl
    {
        public ExtensionMenuView()
        {
            InitializeComponent();
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
