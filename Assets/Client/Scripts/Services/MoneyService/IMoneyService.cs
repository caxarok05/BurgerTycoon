using Client.Services;

namespace Client.Services.MoneyService
{
    public interface IMoneyService : IService
    {
        void AddMoney(int amount);
        int DisplayMoney();
        void SpendMoney(int amount);
    }
}
