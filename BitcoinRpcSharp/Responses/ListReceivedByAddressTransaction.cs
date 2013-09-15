using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class ListReceivedByAddressTransaction
    {
        public string Address { get; set; }
        public string Account { get; set; }
        public double Amount { get; set; }
        public int Confirmations { get; set; }
    }
}
