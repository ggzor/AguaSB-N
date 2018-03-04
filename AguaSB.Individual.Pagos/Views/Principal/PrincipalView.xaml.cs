using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using AguaSB.Extensiones.Views;
using AguaSB.Views.Estilos.Modern.Ventanas;

namespace AguaSB.Individual.Pagos.Views.Principal
{
    public partial class PrincipalView : UserControl, IViewPrincipal
    {
        public PrincipalView(IObservable<IReadOnlyCollection<IExtensionMenu>> extensiones)
        {
            InitializeComponent();
            extensiones.Subscribe(c => Menu.ViewModel = new MenuExtensiones(c));
        }

        public EsquemaBarraTitulo EsquemaBarraTitulo => EsquemasBarraTitulo.Blanco;
        public FrameworkElement View => this;

        public void DoFocus() => Busqueda.DoFocus();
    }
}
