namespace AguaSB.Autenticacion
{
    public interface IAutenticador
    {
        Sesion Autenticar(string usuario, string clave);
    }
}
