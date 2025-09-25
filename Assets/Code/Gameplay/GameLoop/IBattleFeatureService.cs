namespace Code.Gameplay.GameLoop
{
    public interface IBattleFeatureService
    {
        void Activate();
        void Execute();
        void Deactivate();
    }
}