using Castle.MicroKernel.Registration;
using AguaSB.Configuracion;

namespace AguaSB.Individual.Pagos.Instaladores
{
    public static class Instalacion
    {
        public static readonly ConfiguracionExtensiones ConfiguracionExtensiones =
            Configuraciones.IntentarCargar<ConfiguracionExtensiones>(subdirectorio: Global.DirectorioConfiguracion);

        public static readonly FromAssemblyDescriptor EnsambladosEnExtensiones =
            Classes.FromAssemblyInDirectory(new AssemblyFilter(ConfiguracionExtensiones.DirectorioExtensiones));
    }
}
