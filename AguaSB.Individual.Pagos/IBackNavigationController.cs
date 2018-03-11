using System;
using System.Reactive;

namespace AguaSB.Individual.Pagos
{
    public interface IBackNavigationController
    {
        bool IsEnabled { get; set; }
        IObservable<Unit> Executed { get; }
    }
}
