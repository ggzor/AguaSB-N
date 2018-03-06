namespace AguaSB.Views.Animaciones.Definition
{
    public interface ITransitionAnimationBuilder<TBuilder, TParam>
    {
        TBuilder WithFrom(TParam newFrom);
        TBuilder WithTo(TParam newTo);
    }
}
