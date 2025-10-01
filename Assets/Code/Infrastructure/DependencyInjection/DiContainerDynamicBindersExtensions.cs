using System;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Динамические аналоги BindInterfacesTo<TImpl>() / BindInterfacesAndSelfTo<TImpl>().
    /// Реализованы через рефлексию к generic-методам контейнера, так что DiContainer править не нужно.
    /// </summary>
    public static class DiContainerDynamicBindersExtensions
    {
        public static DiContainer.MultiBinder BindInterfacesTo(this DiContainer c, Type impl)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));
            if (impl == null) throw new ArgumentNullException(nameof(impl));

            var m = typeof(DiContainer).GetMethod(nameof(DiContainer.BindInterfacesTo), Type.EmptyTypes);
            var gm = m.MakeGenericMethod(impl);
            return (DiContainer.MultiBinder)gm.Invoke(c, null);
        }

        public static DiContainer.MultiBinder BindInterfacesAndSelfTo(this DiContainer c, Type impl)
        {
            if (c == null) throw new ArgumentNullException(nameof(c));
            if (impl == null) throw new ArgumentNullException(nameof(impl));

            var m = typeof(DiContainer).GetMethod(nameof(DiContainer.BindInterfacesAndSelfTo), Type.EmptyTypes);
            var gm = m.MakeGenericMethod(impl);
            return (DiContainer.MultiBinder)gm.Invoke(c, null);
        }
    }
}