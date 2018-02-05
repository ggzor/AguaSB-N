using AguaSB.Autenticacion;
using AguaSB.Compartido.Interfaces;
using AguaSB.Interfaz;
using ReactiveUI;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace AguaSB.Compartido.ViewModels
{
    public class InicioSesion : ReactiveObject, IInicioSesion
    {
        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set { this.RaiseAndSetIfChanged(ref usuario, value); }
        }

        private string clave;

        public string Clave
        {
            get { return clave; }
            set { this.RaiseAndSetIfChanged(ref clave, value); }
        }

        public ReactiveCommand<Unit, Sesion> IniciarSesion { get; }

        #region Utilerias
        private readonly ObservableAsPropertyHelper<string> errores;
        public string Errores => errores.Value;

        private readonly ObservableAsPropertyHelper<bool> tieneErrores;
        public bool TieneErrores => tieneErrores.Value;
        #endregion

        public IAutenticador Autenticador { get; }

        public InicioSesion(IAutenticador autenticador, IFormateadorExcepciones formateadorExcepciones)
        {
            Autenticador = autenticador ?? throw new ArgumentNullException(nameof(autenticador));

            IniciarSesion = ReactiveCommand.CreateFromTask(IniciarSesionImpl);

            errores = IniciarSesion.ThrownExceptions
                .Select(e =>
                {
                    if (formateadorExcepciones.PuedeFormatear(e))
                        return formateadorExcepciones.Formatear(e);
                    else
                        throw new ArgumentException("El formateador no puede formatear la excepción.", e);
                })
                .ToProperty(this, x => x.Errores);

            tieneErrores = this.WhenAnyObservable(x => x.IniciarSesion.ThrownExceptions, x => x.IniciarSesion.IsExecuting,
                (ex, enEjecucion) => ex != null && !enEjecucion)
                .ToProperty(this, x => x.TieneErrores);
        }

        private Task<Sesion> IniciarSesionImpl()
        {
            return Task.FromResult<Sesion>(null);
        }
    }
}
