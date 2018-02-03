using System;
using System.Reactive.Linq;

using ReactiveUI;

namespace AguaSB.Individual.Pagos
{
    public sealed class VentanaPrincipalViewModel : ReactiveObject
    {
        private ObservableAsPropertyHelper<string> mensajeCarga;
        public string MensajeCarga => mensajeCarga.Value;

        public VentanaPrincipalViewModel()
        {
            mensajeCarga = Cargar().ToProperty(this, x => x.MensajeCarga);
        }

        private IObservable<string> Cargar() => Observable.Return("Cargando");
    }
}
