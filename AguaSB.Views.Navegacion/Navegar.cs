namespace AguaSB.Views.Navegacion
{
    public class Navegar
    {
        public string Direccion { get; }
        public object Datos { get; }

        public Navegar(string direccion, object datos)
        {
            Direccion = direccion;
            Datos = datos;
        }

        public static Navegar A(string direccion, object datos = null) => new Navegar(direccion, datos);
    }
}
