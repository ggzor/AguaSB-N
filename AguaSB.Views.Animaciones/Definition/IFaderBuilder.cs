namespace AguaSB.Views.Animaciones.Definition
{
    public interface IFaderBuilder :
        IAnimationBuilder<IFaderBuilder>,
        ISingleElementAnimation,
        ITransitionAnimationBuilder<IFaderBuilder, double>
    {
    }
}
