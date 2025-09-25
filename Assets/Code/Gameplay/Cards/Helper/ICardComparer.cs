namespace Code.Gameplay.Cards.Helper
{
    public interface ICardComparer
    {
        bool isFirstCardAsigned { get; set; }
        void SetFirstCard(GameEntity card);
        void SetSecondCard(GameEntity card);
        bool Compare();
    }
}