using AguaSB.Autenticacion;

namespace AguaSB.Extensiones
{
    public interface IAutorizadorExtension
    {
        ResultadoAutorizacion Autorizar(IExtension extension, Sesion sesion);
    }
}
