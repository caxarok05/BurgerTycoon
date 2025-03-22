using Client.Data;

namespace Client.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}