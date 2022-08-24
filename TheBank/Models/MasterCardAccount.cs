namespace TheBank.Models
{
    public class MasterCardAccount : Account
    {
        public MasterCardAccount(string name, int accountNumber) : base()
        {
            Name = name;
            AccountNumber = accountNumber;
            AccountType = "MasterCard konto";
        }
        public override decimal ChargeInterest()
        {
            if (Balance > 0)
            {
                Balance *= 1.01m;
            }
            else
            {
                Balance *= 1.2m;
            }
            return Balance;
        }
    }
}
