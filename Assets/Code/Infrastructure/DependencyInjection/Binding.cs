using System;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Модель привязки сервиса к реализации и источнику создания.
    /// Работает и для Singleton, и для Transient.
    /// </summary>
    public sealed class Binding
    {
        /// <summary>Какой сервис (интерфейс/базовый тип) запрашивают.</summary>
        public Type Service { get; }

        /// <summary>Какой конкретный тип будет создаваться (или использоваться).</summary>
        public Type Impl { get; }

        /// <summary>Время жизни (Singleton/Transient).</summary>
        public Lifetime Lifetime { get; }

        /// <summary>
        /// Если задан — использовать этот готовый экземпляр (ведёт себя как Singleton).
        /// Контейнер НЕ владеет этим объектом (не вызывает Dispose).
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// Если задана фабрика — создавать экземпляры через неё.
        /// Для Singleton — создаётся один раз и кешируется.
        /// </summary>
        public Func<DiContainer, object> Factory { get; set; }

        /// <summary>
        /// Флаг владения singleton-экземпляром, созданным контейнером (ctor/factory).
        /// Нужен, чтобы в Dispose() освобождать только «свои» объекты.
        /// </summary>
        public bool OwnsSingleton { get; set; }

        public Binding(Type service, Type impl, Lifetime lifetime)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
            Impl = impl ?? throw new ArgumentNullException(nameof(impl));
            Lifetime = lifetime;
        }
    }
}