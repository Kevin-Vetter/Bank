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

        public override double ChargeInterest()
        {
            if (Balance > 100000)
            {
                Balance *= 1.03;
            }
            else if (Balance >= 50000)
            {
                Balance *= 1.02;
            }
            else if (Balance < 50000)
            {
                Balance *= 1.01;
            }
            return Balance;
        }
    }
}
