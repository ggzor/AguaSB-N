using AguaSB.Views.Animaciones.Definition;

namespace AguaSB.Views.Animaciones
{
    public static class TransitionAnimationBuilderExtensions
    {
        public static TBuilder WithFromTo<TBuilder, TParam>(this TBuilder builder, TParam newFrom, TParam newTo)
            where TBuilder : ITransitionAnimationBuilder<TBuilder, TParam> =>
            builder.WithFrom(newFrom).WithTo(newTo);
    }
}
