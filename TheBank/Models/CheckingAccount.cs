namespace TheBank.Models
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(string name, int accountNumber) : base()
        {
            Name = name;
            AccountNumber = accountNumber;
            AccountType = "Lønkonto";
        }

        public override double ChargeInterest()
        {
            return Balance *= 1.005;
        }

    }
}
