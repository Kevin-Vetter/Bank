using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(string accountName, int id)
        {
            AccountName = accountName;
            Id = id;
        }
        public override double ChargeInterests()
        {
            return Balance *= 1.005;
        }
    }
}
