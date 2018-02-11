using AguaSB.Autenticacion;
using AguaSB.Compartido.Interfaces;
using AguaSB.Extensiones.Views;
using AguaSB.ViewModels.Controles;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AguaSB.Individual.Pagos
{
    public sealed class VentanaPrincipalViewModel : ReactiveObject
    {
        public IAutenticacion Autenticacion { get; }

        public ProgressText ProgresoCarga { get; } = new ProgressText();

        public ReactiveCommand<Sesion, IEnumerable<IExtensionView>> Cargar { get; }

        public VentanaPrincipalViewModel(IAutenticacion autenticacion)
        {
            Autenticacion = autenticacion ?? throw new ArgumentNullException(nameof(autenticacion));

            Cargar = ReactiveCommand.CreateFromTask<Sesion, IEnumerable<IExtensionView>>(CargarImpl);
            Autenticacion.Autenticar.InvokeCommand(Cargar);
        }

        public async Task<IEnumerable<IExtensionView>> CargarImpl(Sesion sesion)
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

            var extensiones = contenedor.ResolveAll<IExtensionView>();

            ProgresoCarga.Set("Carga completa");

            return extensiones;
        }
    }
}
