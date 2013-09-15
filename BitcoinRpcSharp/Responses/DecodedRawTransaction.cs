using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Responses
{
    public class DecodedRawTransaction
    {
        public string Hex { get; set; }
        public string TxId { get; set; }
        public int Version { get; set; }
        public int Locktime { get; set; }
        public List<Vin> VIn { get; set; }
        public List<Vout> VOut { get; set; }
    }

    public class Vin
    {
        public string TxId { get; set; }
        public int VOut { get; set; }
        public ScriptSig ScriptSig { get; set; }
        public long Sequence { get; set; }
    }

    public class ScriptSig
    {
        public string Asm { get; set; }
        public string Hex { get; set; }
    }

    public class Vout
    {
        public double Value { get; set; }
        public int N { get; set; }
        public ScriptPubKey ScriptPubKey { get; set; }
    }

    public class ScriptPubKey
    {
        public string Asm { get; set; }
        public string Hex { get; set; }
        public int ReqSigs { get; set; }
        public string Type { get; set; }
        public List<string> Addresses { get; set; }
    }
}
