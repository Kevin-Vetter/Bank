using TheBank.Repository;

namespace Repository.Bank
{
    public class Bank
    {
        public string BankName { get; }

        public readonly IBank _bank;
        public Bank(IBank bank)
        {
            BankName = "EUC Bank";
            _bank = bank;
        }
    }
}
