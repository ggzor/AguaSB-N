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

        public IEnumerable<IExtension> Todas =>
            extensiones.Values
                .SelectMany(e => e)
                .Distinct();

        public void Registrar(IExtension extension)
        {
            var todasInterfaces = ExtraerTiposInterfacesDerivadosDeExtension(extension.GetType())
                .Concat(new[] { typeof(IExtension), extension.GetType() });

            foreach (var interfaz in todasInterfaces)
            {
                if (!extensiones.ContainsKey(interfaz))
                    extensiones[interfaz] = new List<IExtension>();

                extensiones[interfaz].Add(extension);
            }
        }

        private static IEnumerable<Type> ExtraerTiposInterfacesDerivadosDeExtension(Type tipo) =>
            from tipoInterfaz in tipo.GetInterfaces()
            where tipoInterfaz.GetInterfaces().Contains(typeof(IExtension))
            select tipoInterfaz;

        public IEnumerable<IExtension> Obtener(Type tipo) => extensiones[tipo];

        public void Registrar(IEnumerable<IExtension> extensiones) =>
            extensiones.ForEach(Registrar);

        public IEnumerator<IExtension> GetEnumerator() => Todas.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
