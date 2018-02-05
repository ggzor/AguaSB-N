using AguaSB.Configuracion;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AguaSB.Individual.Pagos.Instaladores
{
    public class InstalarExtensiones : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var conf = Configuraciones.IntentarCargar<ConfiguracionExtensiones>(subdirectorio: Global.DirectorioConfiguracion);

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(conf.DirectorioExtensiones));
        }
    }
}
