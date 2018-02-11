using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Castle.Windsor;
using Castle.Windsor.Installer;
using ReactiveUI;

using AguaSB.Autenticacion;
using AguaSB.Compartido.Interfaces;
using AguaSB.Extensiones;
using AguaSB.Extensiones.Views;
using AguaSB.ViewModels.Controles;

namespace AguaSB.Individual.Pagos
{
    public sealed class VentanaPrincipalViewModel : ReactiveObject
    {
        public ProgressText ProgresoCarga { get; } = new ProgressText();

        public ReactiveCommand<Sesion, AgregadoExtensiones> Cargar { get; }

        public IAutenticacion Autenticacion { get; }
        public IEnumerable<Type> TiposExtensiones { get; }

        public VentanaPrincipalViewModel(IAutenticacion autenticacion, IEnumerable<Type> tiposExtensiones)
        {
            Autenticacion = autenticacion ?? throw new ArgumentNullException(nameof(autenticacion));
            TiposExtensiones = tiposExtensiones ?? throw new ArgumentNullException(nameof(tiposExtensiones));

            Cargar = ReactiveCommand.CreateFromTask<Sesion, AgregadoExtensiones>(CargarImpl);
            Autenticacion.Autenticar.InvokeCommand(Cargar);
        }

        public async Task<AgregadoExtensiones> CargarImpl(Sesion sesion)
        {
            await Task.Delay(3000);

            ProgresoCarga.Set("Registrando componentes");

            var contenedor = await Task.Run(() =>
            {
                var k = new WindsorContainer();

                k.Install(FromAssembly.This());

                return k;
            });

            ProgresoCarga.Set("Cargando interfaz", "El programa podría dejar de responder por unos momentos");
            await Task.Delay(100);

            var extensiones = new CargadorExtensionesWindsor(contenedor, TiposExtensiones).Cargar(sesion);

            ProgresoCarga.Set("Carga completa");

            return extensiones;
        }
    }
}
