namespace AguaSB.Views.Animaciones
{
    public interface IUniformScalerBuilder :
        IAnimationBuilder<IUniformScalerBuilder>,
        ISingleElementAnimation,
        ITransitionAnimationBuilder<IUniformScalerBuilder, double>
    {
    }
}