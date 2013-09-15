using System;

namespace BitcoinRpcSharp
{
    /// <summary>
    /// Custom exception used when something goes wrong while sending, 
    /// receiving or parsing response from the Bitcoin wallet.
    /// </summary>
    [Serializable]
    public class BitcoinRpcException : Exception
    {
        public BitcoinRpcException() 
        { 
        }
        
        public BitcoinRpcException(string message) 
            : base(message)
        { 
        }
        
        public BitcoinRpcException(string message, Exception inner) 
            : base(message, inner) 
        { 
        }
        
        protected BitcoinRpcException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) 
        { 
        }
    }
}
