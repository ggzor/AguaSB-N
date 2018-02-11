using AguaSB.Autenticacion;
using AguaSB.Extensiones;

namespace AguaSB.Individual.Pagos
{
    public class AutorizadorCualquierExtension : IAutorizadorExtension
    {
        public ResultadoAutorizacion Autorizar(IExtension extension, Sesion sesion) => new AutorizacionExitosa();
    }
}
