

namespace Bank.Models
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(string accountName, int id)
        {
            AccountName = accountName;
            Id = id;
        }

        public override double ChargeInterests()
        {
            if (Balance < 50000)
            {
                return Balance *= 1.01;
            }
            else if (Balance < 100000)
            {
                return Balance *= 1.02;
            }
            else
            {
                return Balance *= 1.03;
            }
        }
    }
}
