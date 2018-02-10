using AguaSB.Autenticacion;
using AguaSB.Compartido.Interfaces;
using ReactiveUI;
using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;

namespace AguaSB.Individual.Pagos
{
    public sealed class VentanaPrincipalViewModel : ReactiveObject
    {
        public IInicioSesion InicioSesion { get; }

        public ProgressController ProgresoCarga { get; } = new ProgressController();

        public ReactiveCommand<Sesion, Unit> Cargar { get; }

        public VentanaPrincipalViewModel(IInicioSesion inicioSesion)
        {
            InicioSesion = inicioSesion ?? throw new ArgumentNullException(nameof(inicioSesion));

            Cargar = ReactiveCommand.CreateFromTask<Sesion>(CargarImpl);
            InicioSesion.IniciarSesion.InvokeCommand(Cargar);
        }

        public async Task CargarImpl(Sesion sesion)
        {
            Console.WriteLine($"{sesion.Usuario} !!! {sesion.Clave}");

            await Task.Delay(3000);

            ProgresoCarga.Set("Iniciando carga");

            await Task.Delay(2000);

            ProgresoCarga.Set("Cargando componentes", "1/2");

            await Task.Delay(2000);

            ProgresoCarga.Set("Cargando componentes", "2/2");

            await Task.Delay(2000);

            ProgresoCarga.Set("Cargando interfaz", "El programa podría dejar de responder por unos momentos");
            await Task.Delay(100);

            Thread.Sleep(2000);

            ProgresoCarga.Set("Completado");
        }
    }
}
