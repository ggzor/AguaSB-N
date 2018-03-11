using System;
using System.Reactive;

using ReactiveUI;

namespace AguaSB.Individual.Pagos
{
    public class BackNavigationController : ReactiveObject, IBackNavigationController
    {
        private bool isEnabled;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { this.RaiseAndSetIfChanged(ref isEnabled, value); }
        }

        public ReactiveCommand<Unit, Unit> Execute { get; }
        public IObservable<Unit> Executed => Execute;

        public BackNavigationController()
        {
            Execute = ReactiveCommand.Create(Identity);
        }

        private Unit Identity() => Unit.Default;
    }
}
