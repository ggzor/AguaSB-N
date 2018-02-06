using System;

namespace AguaSB.Autenticacion
{
    public sealed class Sesion
    {
        public string Usuario { get; }
        public string Clave { get; }
        public DateTime Fecha { get; } = DateTime.Now;

        public Sesion(string usuario, string clave)
        {
            Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            Clave = clave ?? throw new ArgumentNullException(nameof(clave));
        }
    }
}
