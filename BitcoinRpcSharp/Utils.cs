using System;

namespace BitcoinRpcSharp
{
    /// <summary>
    /// Utility class containing static utility methods.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Convert a timestamp given as a number to a DateTime object.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
