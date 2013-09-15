using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class Transaction
    {
        public double Amount { get; set; }
        public double Fee { get; set; }
        public int Confirmations { get; set; }
        public string TxId { get; set; }
        public int Time { get; set; }
        public int TimeReceived { get; set; }
        public string Comment { get; set; }
        public string To { get; set; }
        public List<TransactionDetail> Details { get; set; }
    }
}
