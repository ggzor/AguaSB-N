using AguaSB.Autenticacion;
using AguaSB.Extensiones;

namespace AguaSB.Compartido.Interfaces
{
    public interface ICargadorExtensiones
    {
        AgregadoExtensiones Cargar(Sesion sesion);
    }
}
