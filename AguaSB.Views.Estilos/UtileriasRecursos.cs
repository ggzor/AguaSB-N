using System.Windows;

namespace AguaSB.Views.Estilos
{
    public static class UtileriasRecursos
    {
        public static T BuscarRecurso<T>(object clave) => (T)Application.Current.FindResource(clave);
    }
}