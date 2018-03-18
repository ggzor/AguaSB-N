using System;
using System.Threading.Tasks;
using System.Windows;

using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones.Implementation
{
    internal class AwaitTaskActionDelayer : IActionDelayer
    {
        public Duration Duration { get; }

        public AwaitTaskActionDelayer(Duration duration)
        {
            Duration = duration;
        }

        public IActionDelayer For(Duration newDuration) => new AwaitTaskActionDelayer(newDuration);

        public IFutureAnimation Create(Action action) => new AwaitingTaskFutureAnimation(Duration, action);

        private class AwaitingTaskFutureAnimation : IFutureAnimation
        {
            public Duration Duration { get; }

            public Action Action { get; }

            public AwaitingTaskFutureAnimation(Duration duration, Action action)
            {
                Duration = duration;
                Action = action ?? throw new ArgumentNullException(nameof(action));
            }

            // No better option for now
            public async void Begin(FrameworkElement element, Action onCompleted)
            {
                await Task.Delay(Duration.TimeSpan).ConfigureAwait(true);
                Action();
                onCompleted();
            }
        }
    }
}
