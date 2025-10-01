namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>Выполняется сразу после инъекции зависимостей.</summary>
    public interface IInitializable
    {
        void Initialize();
    }
}