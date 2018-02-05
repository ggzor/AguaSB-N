using System.IO;

namespace AguaSB.Configuracion
{
    public sealed class ConfiguracionExtensiones
    {
        public string DirectorioExtensiones { get; set; } = Path.Combine(Global.DirectorioAplicacion, "Extensiones");
    }
}
