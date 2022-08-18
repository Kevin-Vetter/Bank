using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class ConsumerAccount : Account
    {
        /// <summary>
        /// Constructor for ConsumerAccount
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="id"></param>
        public ConsumerAccount(string accountName, int id)
        {
            AccountName = accountName;
            Id = id;
        }

        public override double ChargeInterests()
        {
            if (Balance <= 0)
            {
                return Balance *= 1.001;
            }
            else
            {
                return Balance *= 0.80;
            }
        }
    }
}
