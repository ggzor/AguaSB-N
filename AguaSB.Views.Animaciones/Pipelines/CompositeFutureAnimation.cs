using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;

using MoreLinq;

namespace AguaSB.Views.Animaciones.Pipelines
{
    public class CompositeFutureAnimation : IFutureAnimation
    {
        public Action PreAction { get; }
        public Storyboard Storyboard { get; }
        public Action PostAction { get; }

        public CompositeFutureAnimation(Action preAction = null, Action postAction = null, params IFutureAnimation[] animations)
        {
            PreAction = CompactAllActions(animations, a => a.PreAction, preAction);
            Storyboard = new Storyboard
            {
                Children = new TimelineCollection(animations.Select(a => a.Storyboard).ToArray())
            };
            PostAction = CompactAllActions(animations, a => a.PostAction, postAction);
        }

        private static Action CompactAllActions(IEnumerable<IFutureAnimation> animaciones, Func<IFutureAnimation, Action> selector, Action extra) => () =>
            animaciones.Select(selector)
            .Concat(extra ?? FutureAnimation.NoAction)
            .ForEach(a => a());
    }
}