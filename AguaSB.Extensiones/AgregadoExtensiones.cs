using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AguaSB.Extensiones
{
    public sealed class AgregadoExtensiones
    {
        private readonly Dictionary<Type, IEnumerable> Registro = new Dictionary<Type, IEnumerable>();

        public void Registrar<T>(IEnumerable<T> extensiones) where T : IExtension =>
            Registro.Add(typeof(T), extensiones);

        public IEnumerable<T> Obtener<T>() where T : IExtension =>
            Registro[typeof(T)].OfType<T>();
    }
}
