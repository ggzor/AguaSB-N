using System;
using System.Collections.Generic;
using System.Linq;

namespace AguaSB.Interfaz
{
    public class FormateadorExcepcionesCompuesto : IFormateadorExcepciones
    {
        public IReadOnlyCollection<IFormateadorExcepciones> Formateadores { get; }

        public FormateadorExcepcionesCompuesto(IReadOnlyCollection<IFormateadorExcepciones> formateadores)
        {
            Formateadores = formateadores ?? throw new ArgumentNullException(nameof(formateadores));
        }

        public bool PuedeFormatear(Exception ex) => Formateadores.Any(f => f.PuedeFormatear(ex));

        public string Formatear(Exception ex) => Formateadores.First(f => f.PuedeFormatear(ex)).Formatear(ex);
    }
}
