namespace TheBank.Models
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(string name, int accountNumber) : base()
        {
            Name = name;
            AccountNumber = accountNumber;
            AccountType = "Opsparingskonto";
        }

        public override decimal ChargeInterest()
        {
            if (Balance > 100000)
            {
                Balance *= 1.03m;
            }
            else if (Balance >= 50000)
            {
                Balance *= 1.02m;
            }
            else if (Balance < 50000)
            {
                Balance *= 1.01m;
            }
            return Balance;
        }
    }
}
