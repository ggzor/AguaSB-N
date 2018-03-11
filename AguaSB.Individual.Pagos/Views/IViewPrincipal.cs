using System.Windows;

using AguaSB.Views.Estilos.Modern.Ventanas;
using AguaSB.Views.Utilerias;

namespace AguaSB.Individual.Pagos.Views
{
    public interface IViewPrincipal : IFocusable
    {
        FrameworkElement View { get; }
        EsquemaBarraTitulo EsquemaBarraTitulo { get; }
    }
}
