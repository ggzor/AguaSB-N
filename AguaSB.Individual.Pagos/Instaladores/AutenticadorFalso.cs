using AguaSB.Autenticacion;
using System;

namespace AguaSB.Individual.Pagos.Instaladores
{
    internal class AutenticadorFalso : IAutenticador
    {
        public Sesion Autenticar(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentNullException(nameof(clave));

            var sesion = new Sesion(usuario, clave);
            Console.WriteLine($"Autenticado {usuario} -> {clave}");
            return sesion;
        }
    }
}
