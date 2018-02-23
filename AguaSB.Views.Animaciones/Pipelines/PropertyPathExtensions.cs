using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace AguaSB.Views.Animaciones.Pipelines
{
    public static class PropPath
    {
        public static PropertyPath For<T, R>(Expression<Func<T, R>> pathExpression) =>
            new PropertyPath(string.Join(".", pathExpression.Body.ToString().Split('.').Skip(1)));

        public static PropertyPath For<T>(Expression<Action<T>> pathExpression) =>
            new PropertyPath(string.Join(".", pathExpression.Body.ToString().Split('.').Skip(1)));

        public static PropertyPath Concat(PropertyPath p1, PropertyPath p2) =>
            new PropertyPath(p1.Path + "." + p2.Path);
    }
}
