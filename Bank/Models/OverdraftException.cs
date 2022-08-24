
namespace Bank.Models
{
    public class OverdraftException : Exception
    {
        public OverdraftException(string s) : base(s)
        {
        }
    }
}
