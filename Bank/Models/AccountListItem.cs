
namespace Bank.Models
{
    public class AccountListItem
    {
        public string AccountName { get; set; }
        public double Balance { get; set; }

        public AccountListItem(Account acc)
        {
            AccountName = acc.AccountName;
            Balance = acc.Balance;
        }
    }
}
