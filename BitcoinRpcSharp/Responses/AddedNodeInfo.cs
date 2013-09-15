using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class AddedNodeInfo
    {
        public string AddedNode { get; set; }
        public bool Connected { get; set; }
        public List<NodeAddress> Addresses { get; set; }
    }
}
