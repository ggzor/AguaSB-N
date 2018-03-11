using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using AguaSB.Extensiones.Views;
using AguaSB.Individual.Pagos.Views.Principal;
using AguaSB.Views.Estilos.Modern.Ventanas;

namespace AguaSB.Individual.Pagos.Views
{
    public partial class PrincipalView : UserControl, IViewPrincipal
    {
        private MenuPrincipalView MenuPrincipal { get; set; }

        public PrincipalView(IObservable<IReadOnlyCollection<IExtensionMenu>> extensiones, IProveedorExtensionMenuView proveedorExtensionMenuView,
                             IControladorVentanaPrincipal controladorVentanaPrincipal)
        {
            InitializeComponent();
            extensiones.Subscribe(c =>
            {
                var menuExtensiones = new MenuExtensiones(c, proveedorExtensionMenuView);
                MenuPrincipal = new MenuPrincipalView(menuExtensiones);

                if (Panel.Children.Contains(MenuPrincipal))
                    Panel.Children.Remove(MenuPrincipal);

                var navegador = new NavegadorViews(Panel);
                navegador.Adelante(MenuPrincipal, null);

                controladorVentanaPrincipal.BackNavigation.Executed.Subscribe(u =>
                {
                    navegador.Atras(MenuPrincipal, null);
                    controladorVentanaPrincipal.BackNavigation.IsEnabled = false;
                });

                menuExtensiones.ExtensionSeleccionada
                    .Subscribe(t =>
                    {
                        controladorVentanaPrincipal.BackNavigation.IsEnabled = true;
                        navegador.Adelante(t.Extension.View, t.Parametro);
                    });
            });
        }

        public EsquemaBarraTitulo EsquemaBarraTitulo => EsquemasBarraTitulo.Blanco;
        public FrameworkElement View => this;

        public void DoFocus()
        {
            MenuPrincipal.Entrar(null);
        }
    }
}
