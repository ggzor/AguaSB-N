using System.Windows;
using System.Windows.Media;

namespace AguaSB.Views.Estilos.Modern.Ventanas
{
    public sealed class EsquemaBarraTitulo
    {
        public Color Color { get; }
        public Color ColorTexto { get; }

        public (Style Minimizar, Style Maximizar, Style Cerrar) EstiloBotones { get; }

        public EsquemaBarraTitulo(Color color, Color colorTexto, (Style Minimizar, Style Maximizar, Style Cerrar) estiloBotones)
        {
            Color = color;
            ColorTexto = colorTexto;
            EstiloBotones = estiloBotones;
        }
    }
}