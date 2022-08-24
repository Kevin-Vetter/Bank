using TheBank.Models;

namespace TheBank.Repository
{
    public class BankRepo : IBank
    {
        readonly List<Account> accounts = new();
        int AccountCounter;

        /// <summary>
        /// Creates account
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The account created</returns>
        public Account? CreateAccount(string name, ConsoleKey accType)
        {
            switch (accType)
            {
                case ConsoleKey.D1 or ConsoleKey.NumPad1:
                    CheckingAccount cAcc = new(name, AccountCounter);
                    accounts.Add(cAcc);
                    AccountCounter++;
                    Account acc = cAcc;
                    return acc;
                case ConsoleKey.D2 or ConsoleKey.NumPad2:
                    SavingsAccount sAcc = new(name, AccountCounter);
                    accounts.Add(sAcc);
                    AccountCounter++;
                    acc = sAcc;
                    return acc;
                case ConsoleKey.D3 or ConsoleKey.NumPad3:
                    MasterCardAccount mcAcc = new(name, AccountCounter);
                    accounts.Add(mcAcc);
                    AccountCounter++;
                    acc = mcAcc;
                    return acc;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Deposits to account
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>Balance after deposit</returns>
        public double? Deposit(int accountNumber, double amount)
        {
            Account? _account = accounts.Find(x => x.AccountNumber == accountNumber);
            return _account != null ? _account.Balance += amount : null;
        }

        /// <summary>
        /// withdraws from account
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>balance after withdrawal</returns>
        public double? Withdraw(int accountNumber, double amount)
        {
            Account? _account = accounts.Find(x => x.AccountNumber == accountNumber);
            if ((_account?.Balance - amount) < 0 && _account?.AccountType == "MasterCard konto")
            {
                throw new OverdraftException("Du kan ikke overtrække");
            }
            else
            {
                return _account != null ? _account.Balance -= amount : null;
            }

        }

        /// <summary>
        /// Gets balance from account
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public double? Balance(int accountNumber)
        {
            Account? _account = accounts.Find(x => x.AccountNumber == accountNumber);
            return _account?.Balance;
        }

        /// <summary>
        /// Gets all balances
        /// </summary>
        /// <returns>sum of all</returns>
        public double BankBalance()
        {
            return accounts.Sum(x => x.Balance);
        }

        /// <summary>
        /// Adds interest to all accounts
        /// </summary>
        public void ChargeInterest()
        {
            foreach (Account acc in accounts)
            {
                acc.ChargeInterest();
            }
        }
        public List<AccountListItem> GetAccountList()
        {
            List<AccountListItem> accountList = new();
            foreach (Account acc in accounts)
            {
                accountList.Add(new AccountListItem(acc));
            }
            return accountList;
        }
    }
}
