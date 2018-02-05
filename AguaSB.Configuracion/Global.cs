using System;
using System.IO;

namespace AguaSB.Configuracion
{
    public static class Global
    {
        public static readonly string DirectorioAplicacion =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AguaSB");

        public static readonly string DirectorioConfiguracion =
            Path.Combine(DirectorioAplicacion, "Configuración");
    }
}
