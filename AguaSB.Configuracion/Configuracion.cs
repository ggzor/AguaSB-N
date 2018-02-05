using System;
using System.IO;

using Newtonsoft.Json;

using AguaSB.Utilerias;

namespace AguaSB.Configuracion
{
    /// <summary>
    /// Utilerías para cargar configuraciones con facilidad. Es básicamente un adaptador para JSON.
    /// </summary>
    public static class Configuracion
    {
        public const string ExtensionDeArchivos = ".json";

        public static T Cargar<T>(FileInfo direccion)
        {
            if (direccion == null)
                throw new ArgumentNullException(nameof(direccion));

            string ExtraerTexto()
            {
                using (var stream = direccion.OpenText())
                    return stream.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(ExtraerTexto());
        }

        /// <summary>
        /// Si no se pasa algún nombre, se tomará el nombre del tipo T
        /// </summary>
        public static T Cargar<T>(string nombre = "", string subdirectorio = "", string extension = ExtensionDeArchivos) =>
            Cargar<T>(new FileInfo(Archivos.CrearRuta(subdirectorio, string.IsNullOrWhiteSpace(nombre) ? typeof(T).Name : nombre, extension)));

        /// <summary>
        /// Intenta cargar la configuración con los parámetros establecidos, si no encuentra el archivo,
        /// guarda en ese archivo la configuracion por defecto 
        /// (guarda <paramref name="def"/> con el nombre especificado, en su defecto el nombre del tipo) y la devuelve.
        /// </summary>
        public static T IntentarCargar<T>(T def, string nombre = "", string subdirectorio = "", string extension = ExtensionDeArchivos, bool indentar = true)
        {
            string nombreArchivo = string.IsNullOrWhiteSpace(nombre) ? typeof(T).Name : nombre;
            var rutaArchivo = Archivos.CrearRuta(subdirectorio, nombreArchivo, extension);

            if (File.Exists(rutaArchivo))
            {
                return Cargar<T>(nombre, subdirectorio, extension);
            }
            else
            {
                Guardar(def, nombre, subdirectorio, extension, indentar);

                return def;
            }
        }

        /// <summary>
        /// Análogo a la otra sobrecarga, pero admite un tipo con constructor sin parametros para el parametro def
        public static T IntentarCargar<T>(string nombre = "", string subdirectorio = "", string extension = ExtensionDeArchivos, bool indentar = true) where T : new() =>
            IntentarCargar(new T(), nombre, subdirectorio, extension, indentar);

        public static void Guardar(object objeto, FileInfo direccion, bool indentar = true)
        {
            if (objeto == null)
                throw new ArgumentNullException(nameof(objeto));

            if (direccion == null)
                throw new ArgumentNullException(nameof(direccion));

            if (!direccion.Directory.Exists)
                direccion.Directory.Create();

            var formato = indentar ? Formatting.Indented : Formatting.None;
            var texto = JsonConvert.SerializeObject(objeto, formato);

            using (var stream = new StreamWriter(direccion.Open(FileMode.Create)))
                stream.Write(texto);
        }

        /// <summary>
        /// Si no se pasa algún nombre, se tomará el nombre del tipo del <paramref name="objeto"/>
        /// </summary>
        public static void Guardar(object objeto, string nombre = "", string subdirectorio = "", string extension = ExtensionDeArchivos, bool indentar = true) =>
            Guardar(objeto, new FileInfo(Archivos.CrearRuta(subdirectorio, string.IsNullOrWhiteSpace(nombre) ? objeto.GetType().Name : nombre, extension)), indentar);
    }
}
