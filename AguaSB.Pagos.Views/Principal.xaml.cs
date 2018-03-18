using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using AguaSB.Views;

namespace AguaSB.Pagos.Views
{
    public partial class Principal : UserControl, IView
    {
        public IEnumerable<IExtensionPrincipalPagos> Extensiones { get; }

        public Principal(IEnumerable<IExtensionPrincipalPagos> extensionesPrincipales)
        {
            Extensiones = extensionesPrincipales ?? throw new ArgumentNullException(nameof(extensionesPrincipales));
            InitializeComponent();
        }

        public FrameworkElement View => this;

        public void Entrar(object parametro) => ((IExtensionPrincipalPagos)Hamburger.SelectedItem)?.Entrar(parametro);
    }
}
