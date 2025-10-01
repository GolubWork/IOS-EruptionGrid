using System;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Тонкий адаптер над текущим DiContext, чтобы удобно:
    /// 1) Получать сервисы из контейнера (гарантируя вызов [Inject]-методов),
    /// 2) Инъектить уже созданные объекты (вызовет поля/свойства/методы с [Inject], включая Construct()).
    /// </summary>
    public static class ServiceProvider
    {
        /// <summary>Resolve из контейнера. Если контейнера нет — бросит InvalidOperationException.</summary>
        public static T Get<T>()
        {
            var ctx = DiContext.Instance ?? throw new InvalidOperationException("DiContext.Instance is null");
            return ctx.Container.Resolve<T>();
        }

        /// <summary>Инъектит уже созданный объект: поля/свойства/методы с [Inject] (включая Construct()).</summary>
        public static void Inject(object instance)
        {
            if (instance == null) return;
            var ctx = DiContext.Instance ?? throw new InvalidOperationException("DiContext.Instance is null");
            ctx.Inject(instance);
        }
    }
}