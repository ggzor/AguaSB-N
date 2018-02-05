using System.IO;

namespace AguaSB.Utilerias
{
    public static class Archivos
    {
        public static string CrearRuta(string subdirectorio, string nombre, string extension) =>
            Path.Combine(subdirectorio, nombre) + extension.Trim();
    }
}
