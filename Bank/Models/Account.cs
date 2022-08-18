using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Models
{
    public abstract class Account
    {
        public string AccountName { get; set; }
        public int Id { get; init; }
        public double Balance { get; set; }
        public Type AccountType { get; init; }
        /// <summary>
        /// Charges interests to the different account types
        /// </summary>
        /// <returns></returns>
        public abstract double ChargeInterests();
    }
}
