namespace Bank.Repository;
using Banken;
public class BankRepo : IBankRepo
{
    private List<Account> _accounts = new();
    private int _id;

    public string CreateAccount(string name)
    {
        Account account = new ConsumerAccount(name, _id++);
        _accounts.Add(account);
        return "";
    }
    public string ReadAccount(int id)
    {
        Account acc = _accounts.Find(a => a.Id == id);
        return $"Name: {acc.AccountName}, Id: {acc.Id}, Balance: {acc.Balance}";
    }
    public string UpdateAccount(Account acc, string newName)
    {
        acc.AccountName = newName;
        return "";
    }
    public void DeleteAccount(Account acc) 
    {
        _accounts.Remove(acc);
    }
    public string ReadAllAccounts()
    {
        string allIds = "";

        foreach (Account acc in _accounts)
        {
            allIds += $"{acc.Id}. {acc.AccountName}\n";
        }
        return allIds;
    }

    /// <summary>
    /// Gets the total value of account balances added together
    /// </summary>
    /// <returns>double</returns>
    public double GetTotalBankBalance()
    {
        return _accounts.Sum(a => a.Balance);
    }


    /// <summary>
    /// Prompts user for depositing into an account
    /// </summary>
    /// <param name="selectedAccount"></param>
    /// <param name="depositAmount"></param>
    /// <returns>A string used to notify user of what has happend</returns>
    public string Deposit(Account selectedAccount, double depositAmount)
    {
        selectedAccount.Balance += depositAmount;

        string ui = $"${depositAmount} has been deposited to your account,\nyour balance is now: ${selectedAccount.Balance}";
        return ui;
    }


    /// <summary>
    /// Prompts user for withdrawl from an account
    /// </summary>
    /// <param name="selectedAccount"></param>
    /// <param name="withdrawAmount"></param>
    /// <returns>A string used to notify user of what has happend</returns>
    public string Withdraw(Account selectedAccount, double withdrawAmount)
    {

        if (selectedAccount.Balance < withdrawAmount)
            throw new OverdraftException("Hey dummy, you're poor");
        else
            selectedAccount.Balance -= withdrawAmount;

        string ui = $"${withdrawAmount} has been withdrawn from your account,\nyour balance is now: ${selectedAccount.Balance}";
        return ui;
    }


    /// <summary>
    /// Used for viewing account balance
    /// </summary>
    /// <param name="selectedAccount"></param>
    /// <returns>A string used to notify of their current balance</returns>
    public string Balance(Account selectedAccount)
    {
        string ui = $"Your balance is ${selectedAccount.Balance}";
        return ui;
    }


    /// <summary>
    /// Used to apply interests to all accounts
    /// </summary>
    public void ApplyInterests()
    {
        foreach (Account acc in _accounts)
        {
            acc.ChargeInterests();
        }
        Console.WriteLine("\nInterests have been applied");
    }


    /// <summary>
    /// Finds an account based on the 'id' argument
    /// </summary>
    /// <param name="id"></param>
    /// <returns>First Account object where id matches</returns>
    public Account GetAccountById(int id)
    {
        return _accounts.First(acc => acc.Id == id);
    }


    /// <summary>
    /// Transports stuff
    /// </summary>
    /// <returns>List of account items</returns>
    public List<AccountListItem> GetAccountListItems()
    {
        List<AccountListItem> ALI = new();
        foreach (Account acc in _accounts)
        {
            ALI.Add(new AccountListItem(acc));
        }
        return ALI;
    }
}
