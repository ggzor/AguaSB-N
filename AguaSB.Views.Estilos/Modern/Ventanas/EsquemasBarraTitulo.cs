using System.Windows.Media;

namespace AguaSB.Views.Estilos.Modern.Ventanas
{
    public static partial class EsquemasBarraTitulo
    {
        private const string EstiloBotonesClaroVentana = nameof(EstiloBotonesClaroVentana);
        private const string EstiloBotonesClaroCerrar = nameof(EstiloBotonesClaroCerrar);
        public static readonly (string, string, string) EstiloBotonesClaro =
            (EstiloBotonesClaroVentana, EstiloBotonesClaroVentana, EstiloBotonesClaroCerrar);

        private const string EstiloBotonesW10Ventana = nameof(EstiloBotonesW10Ventana);
        private const string EstiloBotonesW10Cerrar = nameof(EstiloBotonesW10Cerrar);
        public static readonly (string, string, string) EstiloBotonesW10 =
            (EstiloBotonesW10Ventana, EstiloBotonesW10Ventana, EstiloBotonesW10Cerrar);

        public static readonly EsquemaBarraTitulo Blanco = Crear(Colors.White, Colors.Black, EstiloBotonesW10);

        public static readonly EsquemaBarraTitulo PrimarioObscuro =
            Crear(UtileriasRecursos.BuscarRecurso<Color>("PrimaryDark"), Colors.White, EstiloBotonesClaro);

        public static readonly EsquemaBarraTitulo PrimarioClaro =
            Crear(UtileriasRecursos.BuscarRecurso<Color>("PrimaryLight"), Colors.White, EstiloBotonesClaro);

        public static readonly EsquemaBarraTitulo PrimarioError =
            Crear(UtileriasRecursos.BuscarRecurso<Color>("PrimaryError"), Colors.White, EstiloBotonesClaro);
    }
}
