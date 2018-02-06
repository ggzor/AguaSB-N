using AguaSB.Autenticacion;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AguaSB.Individual.Pagos.Instaladores
{
    public class InstalarAutenticacion : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAutenticador>().ImplementedBy<AutenticadorFalso>());
        }
    }
}
