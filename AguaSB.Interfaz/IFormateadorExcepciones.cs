using System;

namespace AguaSB.Interfaz
{
    public interface IFormateadorExcepciones
    {
        bool PuedeFormatear(Exception ex);
        string Formatear(Exception ex);
    }
}
