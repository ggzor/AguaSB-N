using System.Reactive;

using ReactiveUI;

using AguaSB.Autenticacion;

namespace AguaSB.Compartido.Interfaces
{
    public interface IAutenticacion
    {
        ReactiveCommand<Unit, Sesion> Autenticar { get; }

        string Errores { get; }
        bool TieneErrores { get; }
    }
}
