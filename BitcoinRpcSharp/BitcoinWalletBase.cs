﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitcoinRpcSharp
{
    /// <summary>
    /// Base class for bitcoin wallets. Contains basic methods to make requests and parse responses.
    /// </summary>
    public abstract class BitcoinWalletBase
    {
        public string RpcUrl { get; set; }
        public string RpcUser { get; set; }
        public string RpcPassword { get; set; }
        public bool LogJsonResultToConsole { get; set; }

        public BitcoinWalletBase(string rpcUrl, string rpcUser, string rpcPassword, bool logJsonResultToConsole)
        {
            RpcUrl = rpcUrl;
            RpcUser = rpcUser;
            RpcPassword = rpcPassword;
            LogJsonResultToConsole = logJsonResultToConsole;
        }

        /// <summary>
        /// Make a a request. Invoke the given method with the given parameters.
        /// </summary>
        /// <typeparam name="T">
        /// Type to return as the response. 
        /// We will try to convert the JSON response to this type.
        /// </typeparam>
        /// <param name="method">Method to invoke.</param>
        /// <param name="parameters">Parameters to pass to the method.</param>
        /// <returns>The JSON RPC response deserialized as the given type.</returns>
        public T MakeRequest<T>(string method, params object[] parameters)
        {
            var rpcResponse = MakeRpcRequest<T>(new JsonRpcRequest(1, method, parameters));
            return rpcResponse.Result;
        }

        /// <summary>
        /// Make a raw JSON RPC request with the given request object. Returns raw JSON.
        /// </summary>
        /// <param name="jsonRpcRequest">The request object.</param>
        /// <returns>The raw JSON string.</returns>
        public string MakeRawRpcRequest(JsonRpcRequest jsonRpcRequest)
        {
            HttpWebRequest httpWebRequest = MakeHttpRequest(jsonRpcRequest);
            return GetJsonResponse(httpWebRequest);
        }

        /// <summary>
        /// Make an JSON RPC request, and return a JSON RPC response object with the result 
        /// deserialized as the given type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the result as.</typeparam>
        /// <param name="jsonRpcRequest">The request object.</param>
        /// <returns>A JSON RPC response with the result deserialized as the given type.</returns>
        private JsonRpcResponse<T> MakeRpcRequest<T>(JsonRpcRequest jsonRpcRequest)
        {
            HttpWebRequest httpWebRequest = MakeHttpRequest(jsonRpcRequest);
            return GetRpcResponse<T>(httpWebRequest);
        }

        /// <summary>
        /// Make the actual HTTP request to the Bitcoin RPC interface.
        /// </summary>
        /// <param name="jsonRpcRequest">The request to make.</param>
        /// <returns>The HTTP request object.</returns>
        private HttpWebRequest MakeHttpRequest(JsonRpcRequest jsonRpcRequest)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(RpcUrl);
            webRequest.Credentials = new NetworkCredential(RpcUser, RpcPassword);

            // Important, otherwise the service can't deserialse your request properly
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";
            webRequest.Timeout = 2000; // 2 seconds

            byte[] byteArray = jsonRpcRequest.GetBytes();
            webRequest.ContentLength = byteArray.Length;

            try
            {
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new BitcoinRpcException("There was a problem sending the request to the bitcoin wallet.", ex);
            }

            return webRequest;
        }

        /// <summary>
        /// Get the JSON RPC response for the given HTTP request.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the response result to.</typeparam>
        /// <param name="httpWebRequest">The web request.</param>
        /// <returns>A JSON RPC response with the result deserialized as the given type.</returns>
        private JsonRpcResponse<T> GetRpcResponse<T>(HttpWebRequest httpWebRequest)
        {
            string json = GetJsonResponse(httpWebRequest);

            try
            {
                return JsonConvert.DeserializeObject<JsonRpcResponse<T>>(json);
            }
            catch (JsonException jsonEx)
            {
                throw new BitcoinRpcException("There was a problem deserializing the response from the bitcoin wallet.", jsonEx);
            }
        }

        /// <summary>
        /// Gets the JSON response for the given HTTP request.
        /// </summary>
        /// <param name="httpWebRequest">The HTTP request send to the Bitcoin RPC interface.</param>
        /// <returns>The raw JSON string.</returns>
        private string GetJsonResponse(HttpWebRequest httpWebRequest)
        {
            try
            {
                WebResponse webResponse = httpWebRequest.GetResponse();

                // Deserialize the json response
                using (var stream = webResponse.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    reader.Close();

                    if (LogJsonResultToConsole)
                    {
                        Console.WriteLine(JsonFormatter.PrettyPrint(result));
                    }

                    return result;
                }
            }
            catch (ProtocolViolationException protoEx)
            {
                throw new BitcoinRpcException("Unable to connect to the Bitcoin server.", protoEx);
            }
            catch (WebException webEx)
            {
                HttpWebResponse webResponse = webEx.Response as HttpWebResponse;
                if (webResponse != null)
                {
                    switch (webResponse.StatusCode)
                    {
                        case HttpStatusCode.InternalServerError:
                            throw new BitcoinRpcException("The RPC request was either not understood by the Bitcoin server or there was a problem executing the request.", webEx);
                    }
                }

                throw new BitcoinRpcException("An unknown web exception occured while trying to read the JSON response.", webEx);
            }
            catch (Exception ex)
            {
                throw new BitcoinRpcException("An unknown exception occured while trying to read the JSON response.", ex);
            }
        }
    }
}
