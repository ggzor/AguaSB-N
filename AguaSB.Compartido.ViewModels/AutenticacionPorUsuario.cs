using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

using ReactiveUI;

using AguaSB.Autenticacion;
using AguaSB.Compartido.Interfaces;
using AguaSB.Interfaz;

namespace AguaSB.Compartido.ViewModels
{
    public class AutenticacionPorUsuario : ReactiveObject, IAutenticacion
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

        #region Interfaz
        public ReactiveCommand<Unit, Sesion> Autenticar { get; }

        private readonly ObservableAsPropertyHelper<string> errores;
        public string Errores => errores.Value;

        private readonly ObservableAsPropertyHelper<bool> tieneErrores;
        public bool TieneErrores => tieneErrores.Value;
        #endregion

        public IAutenticador Autenticador { get; }

        public AutenticacionPorUsuario(IAutenticador autenticador, IFormateadorExcepciones formateadorExcepciones)
        {
            Autenticador = autenticador ?? throw new ArgumentNullException(nameof(autenticador));

            Autenticar = ReactiveCommand.CreateFromTask(AutenticarImpl);

            errores = Autenticar.ThrownExceptions.Select(e =>
                {
                    if (formateadorExcepciones.PuedeFormatear(e))
                        return formateadorExcepciones.Formatear(e);
                    else
                        throw new ArgumentException("El formateador no puede formatear la excepción.", e);
                })
                .ToProperty(this, x => x.Errores);

            tieneErrores = Autenticar
                .StartWith((Sesion)null)
                .Timestamp()
                .CombineLatest(
                    Autenticar.ThrownExceptions
                    .StartWith((Exception)null)
                    .Timestamp(),
                    (Sesion, Excepcion) => (Sesion, Excepcion))
                .Select(v => v.Sesion.Timestamp < v.Excepcion.Timestamp && v.Excepcion.Value != null)
                .ToProperty(this, x => x.TieneErrores);

        }

        private Task<Sesion> AutenticarImpl() => Task.Run(() => Autenticador.Autenticar(Usuario, Clave));
    }
}
