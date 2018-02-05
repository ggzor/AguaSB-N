using AguaSB.Configuracion;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using AguaSB.Extensiones;
using static AguaSB.Individual.Pagos.Instaladores.Instalacion;

namespace AguaSB.Individual.Pagos.Instaladores
{
    public class InstalarExtensiones : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            container.Register(EnsambladosEnExtensiones.BasedOn<IExtension>());
        }
    }
}
