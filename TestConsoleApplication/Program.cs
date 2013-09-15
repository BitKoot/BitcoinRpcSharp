using BitcoinRpcSharp;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace TestConsoleApplication
{
    /// <summary>
    /// Shows a list of methods which can be invoked on the BitcoinWallet. Most methods can be called
    /// from the commandline. Some (those with complex types likes lists or custom objects) can not 
    /// be called from the command line.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinWallet wallet = new BitcoinWallet(
                ConfigurationManager.AppSettings["rpc_host"],
                ConfigurationManager.AppSettings["rpc_user"],
                ConfigurationManager.AppSettings["rpc_pw"], 
                true);

            ShowCommands(wallet);
        }

        /// <summary>
        /// Print a list of all public methods in the wallet. The methods can be invoked from the command line, 
        /// except for the ones with complex types (like lists, classes and dictionaries) as input parameters.
        /// </summary>
        /// <param name="wallet">Wallet to list commands for.</param>
        public static void ShowCommands(BitcoinWallet wallet)
        {
            Type type = wallet.GetType();
            var methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.IsPublic && !m.IsConstructor && !m.IsSpecialName)
                .OrderBy(m => m.Name)
                .ToList();

            string input = "h";
            while (input != "q")
            {
                Console.Clear();

                int index;
                string[] inputParts = input.Split(new[] { "," }, StringSplitOptions.None);
                if (!string.IsNullOrEmpty(input) && int.TryParse(inputParts[0], out index) && methods.Count > index)
                {
                    InvokeMethode(wallet, methods[index], inputParts);
                }
                else
                {
                    Console.WriteLine("[h] Show this menu");
                    Console.WriteLine("===================");

                    for (int methodIndex = 0; methodIndex < methods.Count; methodIndex++)
                    {
                        Console.Write("[{0}] ", methodIndex);
                        PrintMethod(methods[methodIndex]);
                    }

                    Console.WriteLine("===================");
                    Console.WriteLine("[q] Quit");
                }

                input = Console.ReadLine();
            }
        }

        /// <summary>
        /// Invoke the chosen method with the entered parameters. Try to convert the parameters
        /// to the appropriate type before invoking the method.
        /// </summary>
        /// <param name="wallet">The wallet to invoke the method on.</param>
        /// <param name="method">The method to invoke.</param>
        /// <param name="inputParts">Parameters to pass to the method.</param>
        private static void InvokeMethode(BitcoinWallet wallet, MethodInfo method, string[] inputParts)
        {
            var parameterInfos = method.GetParameters();

            if (parameterInfos.Count() != inputParts.Length - 1)
            {
                Console.WriteLine("The number of given parameters does not match the number of method parameters.");
                Console.WriteLine("Note: Default parameters have to be entered as well!");
                return;
            }

            object[] parameters = new object[inputParts.Length - 1];
            for (int i = 1; i < inputParts.Length; i++)
            {
                if (parameterInfos[i - 1].ParameterType == typeof(string))
                {
                    parameters[i - 1] = Convert.ToString(inputParts[i]);
                }
                else if (parameterInfos[i - 1].ParameterType == typeof(decimal))
                {
                    parameters[i - 1] = Convert.ToDecimal(inputParts[i]);
                }
                else if (parameterInfos[i - 1].ParameterType == typeof(int))
                {
                    parameters[i - 1] = Convert.ToInt32(inputParts[i]);
                }
                else if (parameterInfos[i - 1].ParameterType == typeof(long))
                {
                    parameters[i - 1] = Convert.ToInt64(inputParts[i]);
                }
                else if (parameterInfos[i - 1].ParameterType == typeof(bool))
                {
                    parameters[i - 1] = Convert.ToBoolean(inputParts[i]);
                }
                else
                {
                    parameters[i - 1] = inputParts[i];
                }
            }

            try
            {
                method.Invoke(wallet, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong while invoking the method on the wallet: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Remember that invoking methods from this console application with complex input parameters (lists, object, etc.) is not supported).");
            }
        }


        /// <summary>
        /// Print a readable version of the given method to the command line.
        /// </summary>
        /// <param name="method">Method to print.</param>
        private static void PrintMethod(MethodInfo method)
        {
            Console.Write("{0}(", method.Name);

            var parameters = method.GetParameters();
            for (int j = 0; j < parameters.Count(); j++)
            {
                PrintMethodParameter(parameters[j]);
                
                if (j < parameters.Count() - 1)
                {
                    Console.Write(", ");
                }
            }

            Console.WriteLine(")");
        }

        /// <summary>
        /// Print a readable version of the given parameter to the command line.
        /// </summary>
        /// <param name="parameter">Parameter to print.</param>
        private static void PrintMethodParameter(ParameterInfo parameter)
        {
            if (parameter.ParameterType == typeof(System.Boolean))
            {
                Console.Write("bool");
            }
            else if (parameter.ParameterType == typeof(System.Int32))
            {
                Console.Write("int");
            }
            else if (parameter.ParameterType == typeof(System.Decimal))
            {
                Console.Write("decimal");
            }
            else if (parameter.ParameterType == typeof(System.String))
            {
                Console.Write("string");
            }
            else
            {
                Console.Write(parameter.ParameterType.Name);
            }

            Console.Write(" " + parameter.Name);

            if (parameter.HasDefaultValue)
            {
                Console.Write("=");
                if (parameter.ParameterType == typeof(System.String)
                    && (string)parameter.DefaultValue == string.Empty)
                {
                    Console.Write("\"\"");
                }
                else if (parameter.DefaultValue == null)
                {
                    Console.Write("null");
                }
                else
                {
                    Console.Write(parameter.DefaultValue);
                }
            }
        }
    }
}
