namespace Code.Infrastructure.DependencyInjection
{
    /// <summary>Вызывается в LateUpdate.</summary>
    public interface ILateTickable
    {
        void LateTick();
    }
}