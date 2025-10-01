namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>Вызывается каждый кадр (Update).</summary>
    public interface ITickable
    {
        void Tick();
    }
}