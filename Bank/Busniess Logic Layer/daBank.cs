using Banken;
namespace Repository.Bank;


public class daBank : IBank
{
    public readonly IBankRepo bif;

    public string BankName { get; }
    public double TotalBankBalance { get; }

    /// <summary>
    /// Constructor for Bank
    /// </summary>
    /// <param name="bankName"></param>
    public daBank(string bankName, IBankRepo bf )
    {
        BankName = bankName;
        bif = bf;
    }
   
    
}