using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinRpcSharp.Requests
{
    public class SignRawTransactionInput
    {
        [JsonProperty("txid")]
        public string TransactionId { get; set; }

        [JsonProperty("vout")]
        public int Output { get; set; }

        [JsonProperty("scriptPubKey")]
        public string ScriptPubKey { get; set; }
    }
}
