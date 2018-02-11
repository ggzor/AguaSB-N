using System;
using System.Linq;
using System.Threading.Tasks;

using ReactiveUI;

using AguaSB.Autenticacion;
using AguaSB.Compartido.Interfaces;
using AguaSB.Extensiones;
using AguaSB.Extensiones.Views;
using AguaSB.ViewModels.Controles;

namespace AguaSB.Individual.Pagos
{
    public sealed class VentanaPrincipalViewModel : ReactiveObject
    {
        public ProgressText ProgresoCarga { get; } = new ProgressText();

        private MenuExtensiones menuExtensiones;
        public MenuExtensiones MenuExtensiones
        {
            get { return menuExtensiones; }
            set { this.RaiseAndSetIfChanged(ref menuExtensiones, value); }
        }

        public ReactiveCommand<Sesion, AgregadoExtensiones> Cargar { get; }

        public IAutenticacion Autenticacion { get; }
        public ICargadorExtensiones CargadorExtensiones { get; }

        public VentanaPrincipalViewModel(IAutenticacion autenticacion, ICargadorExtensiones cargadorExtensiones)
        {
            Autenticacion = autenticacion ?? throw new ArgumentNullException(nameof(autenticacion));
            CargadorExtensiones = cargadorExtensiones ?? throw new ArgumentNullException(nameof(cargadorExtensiones));

            Cargar = ReactiveCommand.CreateFromTask<Sesion, AgregadoExtensiones>(CargarImpl);
            Autenticacion.Autenticar.InvokeCommand(Cargar);
        }

        public async Task<AgregadoExtensiones> CargarImpl(Sesion sesion)
        {
            await Task.Delay(3000).ConfigureAwait(true); // Esperar animación

            ProgresoCarga.Set("Cargando interfaz", "El programa podría dejar de responder por unos momentos");
            var extensiones = CargadorExtensiones.Cargar(sesion);

            ProgresoCarga.Set("Carga completa");

            MenuExtensiones = new MenuExtensiones(extensiones.Obtener(typeof(IExtensionMenu)).Cast<IExtensionMenu>());

            return extensiones;
        }
    }
}
