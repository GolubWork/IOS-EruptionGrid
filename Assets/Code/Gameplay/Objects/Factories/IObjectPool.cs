namespace Code.Gameplay.Objects.Factories
{
    public interface IObjectPool
    {
        void Initialize();
        void CleanUp();
        GameEntity ReserveDefaultObject();
        void ReturnDefaultObject(GameEntity obj);
    }
}