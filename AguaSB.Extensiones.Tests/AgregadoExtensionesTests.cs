using System;
using System.Collections.Generic;
using System.Linq;

using MoreLinq;

using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace AguaSB.Extensiones.Tests
{
    [TestFixture]
    public class AgregadoExtensionesTests
    {
        private Fixture Cualquiera;
        private AgregadoExtensiones AgregadoExtensiones;

        [SetUp]
        public void Inicializar()
        {
            Cualquiera = new Fixture();
            Cualquiera.Customize(new AutoNSubstituteCustomization());

            AgregadoExtensiones = new AgregadoExtensiones();
        }

        [Test]
        public void DeberiaObtenerListaConExtensionRegistrada_CuandoSeRegistraUnaExtension()
        {
            var extension = RegistrarCualquierExtension();

            var extensionesRecibidas = AgregadoExtensiones.Obtener(extension.GetType());

            CollectionAssert.Contains(extensionesRecibidas, extension);
        }

        [Test]
        public void DeberiaObtenerListasDistintasConExtensionesRegistradasPorTipo_CuandoSeRegistranExtensionesDeMultiplesTipos()
        {
            foreach (var (tipo, extensiones) in RegistrarMultiplesTiposExtensiones())
            {
                var extensionesRecibidas = AgregadoExtensiones.Obtener(tipo);

                CollectionAssert.AreEquivalent(extensiones, extensionesRecibidas);
            }
        }

        [Test]
        public void Todas_DeberiaRegresarTodasLasExtensionesRegistradasDeCualquierTipo()
        {
            var todasExtensiones = RegistrarMultiplesTiposExtensiones().SelectMany(t => t.Extensiones);

            CollectionAssert.AreEquivalent(todasExtensiones, AgregadoExtensiones);
        }

        [Test]
        public void DeberiaRegistrarTodasLasExtensionesPasadasAlConstructor()
        {
            var extensiones = GenerarMultiplesTiposExtensiones().SelectMany(t => t.Extensiones);

            var agregadoExtensiones = new AgregadoExtensiones(extensiones);

            CollectionAssert.AreEquivalent(extensiones, agregadoExtensiones);
        }

        private IExtension RegistrarCualquierExtension()
        {
            var extension = Cualquiera.Create<IExtension>();

            AgregadoExtensiones.Registrar(extension);

            return extension;
        }

        private IEnumerable<(Type Tipo, IEnumerable<IExtension> Extensiones)> RegistrarMultiplesTiposExtensiones(int cantidad = 2)
        {
            var todas = GenerarMultiplesTiposExtensiones(cantidad);

            todas
                .Take(cantidad)
                .ForEach(t => AgregadoExtensiones.Registrar(t.Extensiones));

            return todas;
        }

        private IEnumerable<(Type Tipo, IEnumerable<IExtension> Extensiones)> GenerarMultiplesTiposExtensiones(int cantidad = 2)
        {
            var registro1 = (typeof(Extension1), Cualquiera.CreateMany<Extension1>());
            var registro2 = (typeof(Extension2), Cualquiera.CreateMany<Extension2>());

            return new(Type, IEnumerable<IExtension>)[] { registro1, registro2 }.Take(cantidad);
        }

        private abstract class ExtensionBase : IExtension
        {
            private readonly Dictionary<string, int> Contadores = new Dictionary<string, int>();

            protected string Alias { get; }

            protected int Numero { get; }

            protected ExtensionBase(string alias)
            {
                Alias = alias;

                if (!Contadores.ContainsKey(alias))
                    Contadores[alias] = 1;

                Numero = Contadores[alias];
                Contadores[alias]++;
            }

            public override string ToString() => $"{Alias}({Numero})";
        }

        private class Extension1 : ExtensionBase
        {
            public Extension1() : base("E1")
            {
            }
        }

        private class Extension2 : ExtensionBase
        {
            public Extension2() : base("E2")
            {
            }
        }
    }
}
