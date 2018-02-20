using System.Windows;
using System.Windows.Media;

namespace AguaSB.Views.Estilos.Modern.Ventanas
{
    public static partial class EsquemasBarraTitulo
    {
        public static EsquemaBarraTitulo ClaroConEnfasis(Color enfasis) => ClaroConEnfasis(enfasis, Colors.Black);
        public static EsquemaBarraTitulo ClaroConEnfasis(Color enfasis, Color texto) => Crear(enfasis, texto, EstiloBotonesClaro);

        public static EsquemaBarraTitulo Crear(Color color, Color texto, (string Minimizar, string Maximizar, string Cerrar) estilosBotones)
        {
            Style BuscarEstilo(string nombre) => UtileriasRecursos.BuscarRecurso<Style>(nombre);

            var (minimizar, maximizar, cerrar) = estilosBotones;

            return new EsquemaBarraTitulo(color, texto, (BuscarEstilo(minimizar), BuscarEstilo(maximizar), BuscarEstilo(cerrar)));
        }
    }
}
