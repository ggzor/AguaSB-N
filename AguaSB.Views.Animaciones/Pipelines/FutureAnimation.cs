using System;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones.Pipelines
{
    public class FutureAnimation : IFutureAnimation
    {
        public static readonly Action NoAction = () => { };

        public Action PreAction { get; }

        public Storyboard Storyboard { get; }

        public Action PostAction { get; }

        public FutureAnimation(Timeline timeline, Action preAction = null, Action postAction = null)
        {
            PreAction = preAction ?? NoAction;

            if (timeline is Storyboard storyBoard)
                Storyboard = storyBoard;
            else if (timeline != null)
                Storyboard = new Storyboard { Children = { timeline } };
            else
                throw new ArgumentNullException(nameof(timeline));

            PostAction = postAction ?? NoAction;
        }

        public static implicit operator FutureAnimation(Timeline timeline) => new FutureAnimation(timeline);
    }
}
