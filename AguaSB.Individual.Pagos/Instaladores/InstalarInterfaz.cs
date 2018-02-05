using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using AguaSB.Views;

namespace AguaSB.Individual.Pagos.Instaladores
{
    public class InstalarInterfaz : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<VentanaPrincipalViewModel>());
            container.Register(
                Component.For<IVentana>()
                .ImplementedBy<VentanaPrincipal>());
        }
    }
}
