using static System.TimeSpan;
using static AguaSB.Views.Animaciones.Neon.AppearDisappearCommon;

using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones.Neon
{
    public static class Appear
    {
        public static readonly ISingleElementAnimation FromBelow = new VisibilityAnimator(
            Fade.In
                .WithFrom(0.2)
                .WithDuration(FromMilliseconds(200)),
            Scale.Uniform
                .WithFromTo(StartingScale, 1.0)
                .WithDuration(FromMilliseconds(400))
        );

        public static readonly ISingleElementAnimation FromAbove = new VisibilityAnimator(
            Fade.In
                .WithDuration(FromMilliseconds(200)),
            Scale.Uniform
                .WithFromTo(FinalScale, 1.0)
                .WithDuration(FromMilliseconds(400))
        );
    }
}
