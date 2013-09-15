using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class TxOutSetInfo
    {
        public int Height { get; set; }
        public string BestBlock { get; set; }
        public int Transactions { get; set; }
        public int TxOuts { get; set; }
        public int BytesSerialized { get; set; }
        public string HashSerialized { get; set; }
        public double TotalAmount { get; set; }
    }
}
