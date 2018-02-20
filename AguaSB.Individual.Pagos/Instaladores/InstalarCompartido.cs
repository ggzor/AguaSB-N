using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AguaSB.Individual.Pagos.Instaladores
{
    public class InstalarCompartido : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("AguaSB.Compartido.ViewModels").Pick()
                .WithServiceDefaultInterfaces()
                .WithServiceSelf());
        }
    }
}
