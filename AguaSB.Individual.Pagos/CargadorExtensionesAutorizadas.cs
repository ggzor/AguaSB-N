using System;
using System.Linq;

using AguaSB.Autenticacion;
using AguaSB.Compartido.Interfaces;
using AguaSB.Extensiones;

namespace AguaSB.Individual.Pagos
{
    public class CargadorExtensionesAutorizadas : ICargadorExtensiones
    {
        public IAutorizadorExtension AutorizadorExtension { get; }
        public ICargadorExtensiones CargadorExtensiones { get; }

        public CargadorExtensionesAutorizadas(ICargadorExtensiones cargadorExtensiones, IAutorizadorExtension autorizadorExtension)
        {
            AutorizadorExtension = autorizadorExtension ?? throw new ArgumentNullException(nameof(autorizadorExtension));
            CargadorExtensiones = cargadorExtensiones ?? throw new ArgumentNullException(nameof(cargadorExtensiones));
        }

        public AgregadoExtensiones Cargar(Sesion sesion) =>
            new AgregadoExtensiones(
                from extension in CargadorExtensiones.Cargar(sesion)
                let resultadoAutorizacion = AutorizadorExtension.Autorizar(extension, sesion)
                where resultadoAutorizacion is AutorizacionExitosa
                select extension
            );
    }
}
