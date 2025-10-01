namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>Выполняется после всех Initialize() (удобно для зависимостей от других инициализаций).</summary>
    public interface ILateInitializable
    {
        void LateInitialize();
    }
}