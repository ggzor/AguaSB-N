using static System.TimeSpan;
using static AguaSB.Views.Animaciones.Neon.AppearDisappearCommon;

using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones.Neon
{
    public static class Disappear
    {
        public static readonly ISingleElementAnimation ToBelow = new VisibilityAnimator(
            Fade.Out
                .WithDuration(FromMilliseconds(200)),
            Scale.Uniform
                .WithFromTo(1.0, StartingScale)
                .WithDuration(FromMilliseconds(200))
        );

        public static readonly ISingleElementAnimation ToAbove = new VisibilityAnimator(
            Fade.Out
                .WithDuration(FromMilliseconds(100)),
            Scale.Uniform
                .WithFromTo(1.0, FinalScale)
                .WithDuration(FromMilliseconds(400))
        );
    }
}
