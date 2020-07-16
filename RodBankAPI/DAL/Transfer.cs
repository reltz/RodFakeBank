using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodBankAPI.DAL
{
    public class Transfer
    {
        public int sendingAccountId { get; set; }
        public int receivingAccountId { get; set; }
        public double amountToTransfer { get; set; }
    }
}
