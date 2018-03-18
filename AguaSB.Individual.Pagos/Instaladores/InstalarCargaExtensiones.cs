using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using AguaSB.Compartido.Interfaces;
using AguaSB.Extensiones;
using AguaSB.Extensiones.Views.Implementacion;

namespace AguaSB.Individual.Pagos.Instaladores
{
    public class InstalarCargaExtensiones : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAutorizadorExtension>().ImplementedBy<AutorizadorCualquierExtension>());

            container.Register(Component.For<ICargadorExtensiones>().ImplementedBy<CargadorExtensionesAutorizadas>());

            container.Register(Component.For<IProveedorExtensionMenuView>().ImplementedBy<ProveedorExtensionMenuViewDefault>());

            var contenedor = new WindsorContainer();
            contenedor.Install(new InstalarExtensiones());

            container.Register(
                Component.For<ICargadorExtensiones>()
                .ImplementedBy<CargadorExtensionesWindsor>()
                .DependsOn(new
                {
                    contenedor,
                    tiposExtensiones = new[] { typeof(IExtension) }
                }));
        }
    }
}
