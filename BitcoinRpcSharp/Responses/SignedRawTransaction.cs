using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class SignedRawTransaction
    {
        public string Hex { get; set; }
        public bool Complete { get; set; }
    }
}
