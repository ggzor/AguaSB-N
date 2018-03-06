namespace AguaSB.Views.Animaciones
{
    public interface ITransitionAnimationBuilder<TBuilder, TParam>
    {
        TBuilder WithFrom(TParam newFrom);
        TBuilder WithTo(TParam newTo);
    }
}
