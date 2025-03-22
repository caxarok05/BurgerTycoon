using Client.Data;

namespace Client.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}