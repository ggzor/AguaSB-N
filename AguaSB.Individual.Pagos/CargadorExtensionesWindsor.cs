using System;
using System.Collections.Generic;

using Castle.Windsor;

using AguaSB.Autenticacion;
using AguaSB.Compartido.Interfaces;
using AguaSB.Extensiones;

namespace AguaSB.Individual.Pagos
{
    internal class CargadorExtensionesWindsor : ICargadorExtensiones
    {
        public WindsorContainer Contenedor { get; }
        public IEnumerable<Type> TiposExtensiones { get; }

        public CargadorExtensionesWindsor(WindsorContainer contenedor, IEnumerable<Type> tiposExtensiones)
        {
            Contenedor = contenedor ?? throw new ArgumentNullException(nameof(contenedor));
            TiposExtensiones = tiposExtensiones ?? throw new ArgumentNullException(nameof(tiposExtensiones));
        }

        public AgregadoExtensiones Cargar(Sesion sesion)
        {
            var resultado = new AgregadoExtensiones();

            //foreach (var tipoExtension in TiposExtensiones)
              //  resultado.Registrar(tipoExtension, Contenedor.ResolveAll(tipoExtension));

            return resultado;
        }
    }
}