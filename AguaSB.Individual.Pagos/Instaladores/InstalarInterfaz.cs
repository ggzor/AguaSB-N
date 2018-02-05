using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using AguaSB.Views;
using AguaSB.Interfaz;
using static AguaSB.Individual.Pagos.Instaladores.Instalacion;

namespace AguaSB.Individual.Pagos.Instaladores
{
    public class InstalarInterfaz : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Register(
                Component.For<IFormateadorExcepciones>()
                .ImplementedBy<FormateadorExcepcionesCompuesto>());

            container.Register(EnsambladosEnExtensiones.BasedOn<IFormateadorExcepciones>().WithServiceAllInterfaces());

            container.Register(
                Classes.FromAssemblyNamed("AguaSB.Interfaz")
                .BasedOn<IFormateadorExcepciones>()
                .WithServiceAllInterfaces());

            container.Register(Component.For<VentanaPrincipalViewModel>());
            container.Register(
                Component.For<IVentana>()
                .ImplementedBy<VentanaPrincipal>());
        }
    }
}
