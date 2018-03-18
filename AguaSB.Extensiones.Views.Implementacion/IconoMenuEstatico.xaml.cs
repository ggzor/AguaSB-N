using System.Windows;
using System.Windows.Controls;

using MahApps.Metro.IconPacks;

namespace AguaSB.Extensiones.Views.Implementacion
{
    public partial class IconoMenuEstatico : UserControl
    {
        protected IconoMenuEstatico(FrameworkElement icono)
        {
            InitializeComponent();

            icono.Width = icono.Height = 36;

            Contenido.Content = icono;
        }

        public static IconoMenuEstatico Crear(PackIconEntypoKind clase) => new IconoMenuEstatico(new PackIconEntypo { Kind = clase });
        public static IconoMenuEstatico Crear(PackIconFontAwesomeKind clase) => new IconoMenuEstatico(new PackIconFontAwesome { Kind = clase });
        public static IconoMenuEstatico Crear(PackIconMaterialKind clase) => new IconoMenuEstatico(new PackIconMaterial { Kind = clase });
        public static IconoMenuEstatico Crear(PackIconMaterialLightKind clase) => new IconoMenuEstatico(new PackIconMaterialLight { Kind = clase });
        public static IconoMenuEstatico Crear(PackIconModernKind clase) => new IconoMenuEstatico(new PackIconModern { Kind = clase });
        public static IconoMenuEstatico Crear(PackIconOcticonsKind clase) => new IconoMenuEstatico(new PackIconOcticons { Kind = clase });
        public static IconoMenuEstatico Crear(PackIconSimpleIconsKind clase) => new IconoMenuEstatico(new PackIconSimpleIcons { Kind = clase });
    }
}
