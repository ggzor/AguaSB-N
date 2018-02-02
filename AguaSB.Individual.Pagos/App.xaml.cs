using System;
using System.Windows;

using Castle.Windsor;
using Castle.Windsor.Installer;
using NLog;

using AguaSB.Views;

namespace AguaSB.Individual.Pagos
{
    public partial class App : Application
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            var contenedor = new WindsorContainer();

            try
            {
                Logging.Configurar();

                contenedor.Install(FromAssembly.This());

                contenedor.Resolve<IVentana>().Mostrar();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }
            finally
            {
                try
                {
                    contenedor.Dispose();
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error al liberar");
                }

                Current.Shutdown();
            }
        }
    }
}
