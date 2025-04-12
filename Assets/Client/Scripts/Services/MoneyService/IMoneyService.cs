using Client.Services;

namespace Client.Services.MoneyService
{
    public interface IMoneyService : IService
    {
        void AddMoney(int amount);
        int DisplayMoney();
        int getMaximumMoney();
        void SpendMoney(int amount);
    }
}
