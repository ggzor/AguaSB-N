using System;
using System.Windows;

namespace AguaSB.Views.Animaciones.Pipelines
{
    internal class SequentialFutureAnimation : IFutureAnimation
    {
        public IFutureAnimation First { get; }
        public IFutureAnimation Second { get; }

        public SequentialFutureAnimation(IFutureAnimation first, IFutureAnimation second)
        {
            First = first ?? throw new ArgumentNullException(nameof(first));
            Second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public void Begin(FrameworkElement element, Action onCompleted) =>
            First.Begin(element,
                () => Second.Begin(element, onCompleted));
    }
}