using Client.Data;

namespace Client.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}