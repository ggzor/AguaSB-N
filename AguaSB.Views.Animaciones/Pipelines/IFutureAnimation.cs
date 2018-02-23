using System;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones.Pipelines
{
    public interface IFutureAnimation
    {
        Action PreAction { get; }

        Storyboard Storyboard { get; }

        Action PostAction { get; }
    }
}
