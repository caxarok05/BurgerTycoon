using Client.StaticData;

namespace Client.Logic.EntityUpgrade
{

    public interface IUpgradable
    {
        void Upgrade();
        bool CanUpgrade();
        IUpgradeStaticData GetNewData();
        int ReturnRealLevel();
    }
}
