using TheBank.Models;

namespace TheBank.Repository
{
    public interface IBank
    {
        Account? CreateAccount(string name, ConsoleKey accType);
        double? Deposit(int accountNumber, double amount);
        double? Withdraw(int accountNumber, double amount);
        double? Balance(int accountNumber);
        double BankBalance();
        void ChargeInterest();
        List<AccountListItem> GetAccountList();
    }
}
