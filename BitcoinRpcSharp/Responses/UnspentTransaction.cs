using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class UnspentTransaction
    {
        public string TxId { get; set; }
        public int VOut { get; set; }
        public string Address { get; set; }
        public string ScriptPubKey { get; set; }
        public double Amount { get; set; }
        public int Confirmations { get; set; }
    }
}
