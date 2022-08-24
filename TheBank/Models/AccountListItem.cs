namespace TheBank.Models
{
    public class AccountListItem
    {
        public Account Account { get; set; }
        public AccountListItem(Account acc)
        {
            Account = acc;
        }
    }
}
