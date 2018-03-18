using System;
using System.Windows;

using AguaSB.Views.Animaciones.Definition;
using AguaSB.Views.Animaciones.Implementation;

namespace AguaSB.Views.Animaciones
{
    public static class Delay
    {
        public static readonly Duration DefaultDelay = TimeSpan.FromMilliseconds(200);

        public static readonly IActionDelayer Action = new AwaitTaskActionDelayer(DefaultDelay);
    }
}
