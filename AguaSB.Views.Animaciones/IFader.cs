using AguaSB.Views.Animaciones.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace AguaSB.Views.Animaciones
{

    public interface IFader
    {
        void Apply(FrameworkElement element);
        IFutureAnimation Create(FrameworkElement element);
    }
}
