namespace AguaSB.Views.Animaciones.Definition
{
    public interface IUniformScalerBuilder :
        IAnimationBuilder<IUniformScalerBuilder>,
        ISingleElementAnimation,
        ITransitionAnimationBuilder<IUniformScalerBuilder, double>
    {
    }
}