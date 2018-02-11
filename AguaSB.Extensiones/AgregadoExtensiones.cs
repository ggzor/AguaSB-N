using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using MoreLinq;

namespace AguaSB.Extensiones
{
    public sealed class AgregadoExtensiones : IEnumerable<IExtension>
    {
        private readonly Dictionary<Type, List<IExtension>> extensiones = new Dictionary<Type, List<IExtension>>();

        public AgregadoExtensiones()
        {
        }

        public AgregadoExtensiones(IEnumerable<IExtension> extensiones) =>
            extensiones.ForEach(Registrar);

        public IEnumerable<IExtension> Todas => extensiones.Values.SelectMany(e => e);

        public void Registrar(IExtension extension)
        {
            var tipo = extension.GetType();

            if (!extensiones.ContainsKey(tipo))
                extensiones[tipo] = new List<IExtension>();

            extensiones[tipo].Add(extension);
        }

        public IEnumerable<IExtension> Obtener(Type tipo) => extensiones[tipo];

        public void Registrar(IEnumerable<IExtension> extensiones) =>
            extensiones.ForEach(Registrar);

        public IEnumerator<IExtension> GetEnumerator() => Todas.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
