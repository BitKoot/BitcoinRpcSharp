using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class NodeAddress
    {
        public string Address { get; set; }
        public bool Connected { get; set; }
    }
}
