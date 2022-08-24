using TheBank.Models;

namespace TheBank.Repository
{
    public interface IBank
    {
        Account? CreateAccount(string name, ConsoleKey accType);
        decimal? Deposit(int accountNumber, decimal amount);
        decimal? Withdraw(int accountNumber, decimal amount);
        decimal? Balance(int accountNumber);
        decimal BankBalance();
        void ChargeInterest();
        List<AccountListItem> GetAccountList();
    }
}
