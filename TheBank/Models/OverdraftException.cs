namespace TheBank.Models
{
    public class OverdraftException : Exception
    {
        public OverdraftException(string s) : base(s)
        {

        }
    }
}
