using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class MultisigAddress
    {
        public string Address { get; set; }
        public string RedeemScript { get; set; }
    }
}
