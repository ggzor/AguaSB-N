using System.Reactive;
using ReactiveUI;
using AguaSB.Autenticacion;

namespace AguaSB.Compartido.Interfaces
{
    public interface IInicioSesion
    {
        string Usuario { get; set; }
        string Clave { get; set; }

        ReactiveCommand<Unit, Sesion> IniciarSesion { get; }
        string Errores { get; }
        bool TieneErrores { get; }
    }
}
