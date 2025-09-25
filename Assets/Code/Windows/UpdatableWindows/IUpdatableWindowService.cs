namespace Code.Windows.UpdatableWindows
{
    public interface IUpdatableWindowService
    {
        void Open(UpdatableWindowId staticWindowId);
        void Close(UpdatableWindowId staticWindowId);
        void CloseAll();
    }
}