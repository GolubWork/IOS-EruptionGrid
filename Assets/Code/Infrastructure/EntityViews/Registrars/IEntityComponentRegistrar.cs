namespace Code.Infrastructure.EntityViews.Registrars
{
    public interface IEntityComponentRegistrar
    {
        void RegisterComponents();
        void UnRegisterComponents();
    }
}