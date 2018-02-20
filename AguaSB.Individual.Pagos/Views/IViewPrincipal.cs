using AguaSB.Views;
using AguaSB.Views.Estilos.Modern.Ventanas;

namespace AguaSB.Individual.Pagos.Views
{
    public interface IViewPrincipal : IView
    {
        EsquemaBarraTitulo EsquemaBarraTitulo { get; }
    }
}
