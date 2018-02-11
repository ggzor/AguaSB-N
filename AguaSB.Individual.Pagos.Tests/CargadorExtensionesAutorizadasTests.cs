using System.Linq;

using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

using AguaSB.Compartido.Interfaces;
using AguaSB.Extensiones;
using AguaSB.Autenticacion;

namespace AguaSB.Individual.Pagos.Tests
{
    [TestFixture]
    public class CargadorExtensionesAutorizadasTests
    {
        private readonly Fixture Cualquiera = new Fixture();

        [SetUp]
        public void Inicializar()
        {
            Cualquiera.Customize(new AutoNSubstituteCustomization());
        }

        [Test]
        public void DeberiaDevolverExtensionesAutorizadasParaSesion_CuandoExtensionEsValidaParaSesion()
        {
            // Arrange
            var extensionesAutorizadas = Cualquiera.CreateMany<IExtension>();
            var todasExtensiones = extensionesAutorizadas.Concat(Cualquiera.CreateMany<IExtension>());

            var cargadorExtensiones = Substitute.For<ICargadorExtensiones>();
            var autorizadorExtensiones = Substitute.For<IAutorizadorExtension>();

            var agregadoTodasExtensiones = new AgregadoExtensiones();
            agregadoTodasExtensiones.Registrar(todasExtensiones);

            cargadorExtensiones.Cargar(null).ReturnsForAnyArgs(agregadoTodasExtensiones);

            autorizadorExtensiones.Autorizar(null, null).ReturnsForAnyArgs(ci =>
            {
                if (extensionesAutorizadas.Contains(ci.Arg<IExtension>()))
                    return new AutorizacionExitosa();
                else
                    return new AutorizacionFallida();
            });

            var cargadorExtensionesAutorizadas = new CargadorExtensionesAutorizadas(cargadorExtensiones, autorizadorExtensiones);

            // Act
            var extensionesCargadas = cargadorExtensionesAutorizadas.Cargar(Cualquiera.Create<Sesion>());

            // Assert
            CollectionAssert.AreEquivalent(extensionesAutorizadas, extensionesCargadas.Todas);
        }
    }
}
