using System;
using System.Collections.Generic;
using System.Linq;

using MoreLinq;

using NUnit.Framework;
using AutoFixture;
using AutoFixture.AutoNSubstitute;

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
        public void DeberiaObtenerListaConExtensionRegistradaDelTipoEspecificado_CuandoSeRegistraUnaExtension()
        {
            var extension = new Extension();
            AgregadoExtensiones.Registrar(extension);

            var extensionesRecibidas = AgregadoExtensiones.Obtener<Extension>();

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

        [Test]
        public void DeberiaRegistrarExtensionConTodasSusInterfaces_CuandoExtensionImplementaMultiplesInterfaces()
        {
            var (tiposInterfazExtension, extension) = RegistrarCualquierExtensionConMultiplesInterfaces();

            foreach (var tipoExtension in tiposInterfazExtension)
                CollectionAssert.Contains(AgregadoExtensiones.Obtener(tipoExtension), extension);
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

        private (IEnumerable<Type> TiposImplementados, IExtension extension) RegistrarCualquierExtensionConMultiplesInterfaces()
        {
            var tiposInterfazExtension = new[] {
                typeof(IExtension),
                typeof(IExtensionTipo1),
                typeof(IExtensionTipo2),
                typeof(IExtensionTipo3),
                typeof(ExtensionConMultiplesInterfaces)
            };

            var extension = new ExtensionConMultiplesInterfaces();
            AgregadoExtensiones.Registrar(extension);

            return (tiposInterfazExtension, extension);
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

        private class Extension : ExtensionBase
        {
            public Extension() : base("E")
            {
            }
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

        private interface IExtensionTipo1 : IExtension { }
        private interface IExtensionTipo2 : IExtension { }
        private interface IExtensionTipo3 : IExtensionTipo1 { }

        private class ExtensionConMultiplesInterfaces : IExtensionTipo2, IExtensionTipo3
        {
        }
    }
}
