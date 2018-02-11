namespace AguaSB.Extensiones
{
    public abstract class ResultadoAutorizacion
    {
        internal ResultadoAutorizacion() { }
    }

    public sealed class AutorizacionExitosa : ResultadoAutorizacion
    {
    }

    public sealed class AutorizacionFallida : ResultadoAutorizacion
    {
    }
}