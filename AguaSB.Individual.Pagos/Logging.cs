using NLog;
using NLog.Config;
using NLog.Targets;

namespace AguaSB.Individual.Pagos
{
    internal static class Logging
    {
        internal static void Configurar()
        {
            var configuracion = new LoggingConfiguration();

            var consola = new ColoredConsoleTarget();
            var archivo = new FileTarget
            {
                FileName = "${basedir:dir=Logs}/${shortdate}.txt",
                ArchiveEvery = FileArchivePeriod.Minute
            };

            consola.Layout = archivo.Layout = "${longdate} [${uppercase:${level}}] ${logger}: ${message} ${exception:format=toString}";

            var reglaConsola = new LoggingRule("*", LogLevel.Info, consola);
            var reglaArchivo = new LoggingRule("*", LogLevel.Info, archivo);
            configuracion.LoggingRules.Add(reglaConsola);
            configuracion.LoggingRules.Add(reglaArchivo);

            configuracion.AddTarget(nameof(consola), consola);
            configuracion.AddTarget(nameof(archivo), archivo);

            LogManager.Configuration = configuracion;
        }
    }
}