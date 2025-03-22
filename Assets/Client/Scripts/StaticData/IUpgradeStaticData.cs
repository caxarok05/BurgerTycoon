using UnityEngine;

namespace Client.StaticData
{
    public interface IUpgradeStaticData
    {
        Sprite Icon { get; }
        string Title { get; }
        string Description { get; }
        int Cost { get; }
        int UpgradeLevel { get; }
        int MaxLevel { get; }
    }
}
