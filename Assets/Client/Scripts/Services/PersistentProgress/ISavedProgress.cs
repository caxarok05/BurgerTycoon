using Client.Data;

namespace Client.Services.PersistentProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}