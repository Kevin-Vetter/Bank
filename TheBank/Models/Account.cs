namespace TheBank
{
    abstract public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public int AccountNumber { get; set; }
        public string AccountType { get; set; }

        public abstract double ChargeInterest();
    }
}
