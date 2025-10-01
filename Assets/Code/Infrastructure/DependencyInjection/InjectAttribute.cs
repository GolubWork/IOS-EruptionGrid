using System;

namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Помечает точку инъекции зависимостей.
    /// Поддерживаются: конструкторы, методы, поля, свойства.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Constructor |
        AttributeTargets.Method |
        AttributeTargets.Field |
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public sealed class InjectAttribute : Attribute
    {
        /// <summary>
        /// Необязательный порядок выполнения для нескольких [Inject]-методов.
        /// Меньше — раньше. Для MonoBehaviour это позволит выстроить очередность.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Если true — отсутствие зависимости не приведёт к ошибке (полезно для опциональных сервисов).
        /// </summary>
        public bool Optional { get; set; }

        public InjectAttribute(int order = 0)
        {
            Order = order;
        }
    }
}