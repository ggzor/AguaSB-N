using System;

namespace AguaSB.Interfaz
{
    public class FormateadorException : IFormateadorExcepciones
    {
        public string Formatear(Exception ex) => ex.Message;

        public bool PuedeFormatear(Exception ex) => true;
    }
}
