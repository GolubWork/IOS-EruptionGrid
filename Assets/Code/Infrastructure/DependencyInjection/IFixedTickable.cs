namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>Вызывается в FixedUpdate.</summary>
    public interface IFixedTickable
    {
        void FixedTick();
    }
}