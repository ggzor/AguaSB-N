using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ReactiveUI;

namespace AguaSB.Individual.Pagos
{
    public sealed class VentanaPrincipalViewModel : ReactiveObject
    {
        public const string Msj_ComenzandoCarga = "Comenzando carga";
        private const string Msj_CompletandoCarga = "Completado";

        private ISubject<string> ProgresoCarga = new ReplaySubject<string>(1);
        private ObservableAsPropertyHelper<string> mensajeCarga;
        public string MensajeCarga => mensajeCarga.Value;

        public IObservable<Unit> ComenzandoCarga => ProgresoCarga.ObserveOnDispatcher().Where(s => s == Msj_ComenzandoCarga).Select(s => Unit.Default);

        private ReactiveCommand<Unit, Unit> cargar;
        public ReactiveCommand<Unit, Unit> Cargar => cargar;

        public VentanaPrincipalViewModel()
        {
            mensajeCarga = ProgresoCarga.ObserveOnDispatcher().ToProperty(this, x => x.MensajeCarga);

            cargar = ReactiveCommand.CreateFromTask(() => CargarImpl());
        }

        private async Task CargarImpl()
        {
            ProgresoCarga.OnNext(Msj_ComenzandoCarga);
            await Task.Delay(4000);
            ProgresoCarga.OnNext("Verificando base de datos");
            await Task.Delay(1000);
            ProgresoCarga.OnNext("Cargando extensiones");
            await Task.Delay(1000);
            ProgresoCarga.OnNext(Msj_CompletandoCarga);
        }
    }
}
