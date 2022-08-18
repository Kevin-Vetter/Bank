using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Repository
{
    public interface IBank
    {
        /// <summary>
        /// IDFK
        /// </summary>
        public string BankName{ get; set; }
    }
}
