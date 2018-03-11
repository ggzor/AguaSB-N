using System;
using System.Windows;

namespace AguaSB.Views.Animaciones
{
    public class FutureAnimation : IFutureAnimation
    {
        public static readonly FutureAnimation NoAnimation = new FutureAnimation();

        private FutureAnimation()
        {
        }

        public void Begin(FrameworkElement element, Action onCompleted) => onCompleted();
    }
}
