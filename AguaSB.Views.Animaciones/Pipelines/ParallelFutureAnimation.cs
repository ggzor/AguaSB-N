using System;
using System.Collections.Generic;
using System.Windows;

using MoreLinq;

namespace AguaSB.Views.Animaciones.Pipelines
{
    internal class ParallelFutureAnimation : IFutureAnimation
    {
        public IReadOnlyCollection<IFutureAnimation> Animations { get; }

        public ParallelFutureAnimation(IReadOnlyCollection<IFutureAnimation> animations)
        {
            Animations = animations ?? throw new ArgumentNullException(nameof(animations));
        }

        public void Begin(FrameworkElement element, Action onCompleted)
        {
            var count = 0;

            void CheckForAllAnimationsToComplete()
            {
                count++;

                if (count == Animations.Count)
                    onCompleted();
            }

            Animations.ForEach(a => a.Begin(element, CheckForAllAnimationsToComplete));
        }
    }
}