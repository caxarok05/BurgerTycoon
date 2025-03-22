using Client.Data;

namespace Client.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}