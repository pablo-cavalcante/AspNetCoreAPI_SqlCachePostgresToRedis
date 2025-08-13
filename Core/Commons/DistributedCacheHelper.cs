using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

#pragma warning disable
namespace WebApplication7.Core.Commons
{
    public class DistributedCacheHelper
    {
        public static void Store(IDistributedCache cache, string prefix, object dataPrefix, int TimeSpanValue, object dataResponse)
        {
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(TimeSpanValue));

            string onlySafeChars = Regex.Replace(
                Regex.Replace(prefix + JsonConvert.SerializeObject(dataPrefix), @"\bnull\b", "\"null\"", RegexOptions.IgnoreCase),
                @"[^a-zA-Z0-9_]", "");

            cache.Remove(onlySafeChars);
            cache.SetString(onlySafeChars, JsonConvert.SerializeObject(dataResponse), options);
        }

        public static object getData(IDistributedCache cache, string prefix, object dataPrefix)
        {
            string onlySafeChars = Regex.Replace(
                Regex.Replace(prefix + JsonConvert.SerializeObject(dataPrefix), @"\bnull\b", "\"null\"", RegexOptions.IgnoreCase),
                @"[^a-zA-Z0-9_]", "");

            return cache.GetString(onlySafeChars);
        }
    }
}
