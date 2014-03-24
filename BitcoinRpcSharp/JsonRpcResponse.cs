using Newtonsoft.Json;

namespace BitcoinRpcSharp
{
    /// <summary>
    /// Class containing the information contained in a JSON RPC response received from
    /// the Bitcoin wallet.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the result object. This can simply be a string like a transaction id, 
    /// or a more complex object like TransactionDetails.
    /// </typeparam>
    public class JsonRpcResponse<T>
    {
        /// <summary>
        /// The result object.
        /// </summary>
        [JsonProperty(PropertyName = "result", Order = 0)]
        public T Result { get; set; }

        /// <summary>
        /// The id of the corresponding request.
        /// </summary>
        [JsonProperty(PropertyName = "id", Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// The error returned by the wallet, if any.
        /// </summary>
        [JsonProperty(PropertyName = "error", Order = 2)]
        public RpcError Error { get; set; }

        /// <summary>
        /// Create a new JSON RPC response with the given id, error and result object.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="error">The error.</param>
        /// <param name="result">The result object.</param>
        //public JsonRpcResponse(int id, string error, T result)
        //{
        //    Id = id;
        //    Error = error;
        //    Result = result;
        //}
    }

    public class RpcError
    {
        [JsonProperty(PropertyName = "code")]
        public RPCErrorCode Code { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }

    public enum RPCErrorCode
    {
        // Standard JSON-RPC 2.0 errors
        RPC_INVALID_REQUEST = -32600,
        RPC_METHOD_NOT_FOUND = -32601,
        RPC_INVALID_PARAMS = -32602,
        RPC_INTERNAL_ERROR = -32603,
        RPC_PARSE_ERROR = -32700,

        // General application defined errors
        RPC_MISC_ERROR = -1,  // std::exception thrown in command handling
        RPC_FORBIDDEN_BY_SAFE_MODE = -2,  // Server is in safe mode, and command is not allowed in safe mode
        RPC_TYPE_ERROR = -3,  // Unexpected type was passed as parameter
        RPC_INVALID_ADDRESS_OR_KEY = -5,  // Invalid address or key
        RPC_OUT_OF_MEMORY = -7,  // Ran out of memory during operation
        RPC_INVALID_PARAMETER = -8,  // Invalid, missing or duplicate parameter
        RPC_DATABASE_ERROR = -20, // Database error
        RPC_DESERIALIZATION_ERROR = -22, // Error parsing or validating structure in raw format

        // P2P client errors
        RPC_CLIENT_NOT_CONNECTED = -9,  // Bitcoin is not connected
        RPC_CLIENT_IN_INITIAL_DOWNLOAD = -10, // Still downloading initial blocks
        RPC_CLIENT_NODE_ALREADY_ADDED = -23, // Node is already added
        RPC_CLIENT_NODE_NOT_ADDED = -24, // Node has not been added before

        // Wallet errors
        RPC_WALLET_ERROR = -4,  // Unspecified problem with wallet (key not found etc.)
        RPC_WALLET_INSUFFICIENT_FUNDS = -6,  // Not enough funds in wallet or account
        RPC_WALLET_INVALID_ACCOUNT_NAME = -11, // Invalid account name
        RPC_WALLET_KEYPOOL_RAN_OUT = -12, // Keypool ran out, call keypoolrefill first
        RPC_WALLET_UNLOCK_NEEDED = -13, // Enter the wallet passphrase with walletpassphrase first
        RPC_WALLET_PASSPHRASE_INCORRECT = -14, // The wallet passphrase entered was incorrect
        RPC_WALLET_WRONG_ENC_STATE = -15, // Command given in wrong wallet encryption state (encrypting an encrypted wallet etc.)
        RPC_WALLET_ENCRYPTION_FAILED = -16, // Failed to encrypt the wallet
        RPC_WALLET_ALREADY_UNLOCKED = -17, // Wallet is already unlocked
    };
}
