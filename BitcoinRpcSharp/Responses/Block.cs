using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class Block
    {
        public string Hash { get; set; }
        public int Confirmations { get; set; }
        public int Size { get; set; }
        public int Height { get; set; }
        public int Version { get; set; }
        public string MerkleRoot { get; set; }
        public List<string> Tx { get; set; }
        public int Time { get; set; }
        public int Nonce { get; set; }
        public string Bits { get; set; }
        public double Difficulty { get; set; }
        public string NextBlockHash { get; set; }
    }
}
