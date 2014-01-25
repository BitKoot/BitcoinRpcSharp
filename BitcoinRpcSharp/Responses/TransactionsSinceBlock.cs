using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class TransactionsSinceBlock
    {
        public List<TransactionSinceBlock> Transactions { get; set; }
        public string Lastblock { get; set; }
    }
}
