using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class ValidateAddress
    {
        public bool IsValid { get; set; }
        public string Address { get; set; }
        public bool IsMine { get; set; }
        public bool IsScript { get; set; }
        public string Script { get; set; }
        public List<string> Addresses { get; set; }
        public int SigsRequired { get; set; }
        public string Account { get; set; }
    }
}
