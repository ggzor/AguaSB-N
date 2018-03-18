using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones.Neon
{
    public static class AppearDisappearCommon
    {
        public static readonly IEasingFunction Easing = new CircleEase { EasingMode = EasingMode.EaseOut };
        public const double StartingScale = 0.85;
        public const double FinalScale = 1.1;
    }
}
