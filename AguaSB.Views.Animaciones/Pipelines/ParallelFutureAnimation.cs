using System;
using System.Windows;

using MoreLinq;

namespace AguaSB.Views.Animaciones.Pipelines
{
    public class ParallelFutureAnimation : IFutureAnimation
    {
        public IFutureAnimation[] Animations { get; }

        public ParallelFutureAnimation(params IFutureAnimation[] animations)
        {
            Animations = animations ?? throw new ArgumentNullException(nameof(animations));
        }

        public void Begin(FrameworkElement element, Action onCompleted)
        {
            var count = 0;

            void CheckForAllAnimationsToComplete()
            {
                if (count == Animations.Length)
                    onCompleted();
                else
                    count++;
            }

            Animations.ForEach(a => a.Begin(element, CheckForAllAnimationsToComplete));
        }
    }
}