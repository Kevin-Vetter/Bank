
namespace Banken;

public interface IBankRepo
{
    public string CreateAccount(string name);
    public string ReadAccount(int id);
    public string UpdateAccount(Account acc, string newName);
    public void DeleteAccount(Account acc);
    public string ReadAllAccounts();
}
