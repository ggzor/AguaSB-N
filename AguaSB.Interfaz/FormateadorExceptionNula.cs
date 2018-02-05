using System;

namespace AguaSB.Interfaz
{
    public class FormateadorExceptionNula : IFormateadorExcepciones
    {
        public string Formatear(Exception ex) => string.Empty;

        public bool PuedeFormatear(Exception ex) => ex == null;
    }
}
