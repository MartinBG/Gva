using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Common.Json
{
    public static class JTokenExtensions
    {
        public static JToken Get(this JToken o, string path)
        {
            return o.SelectToken(path);
        }

        public static T Get<T>(this JToken o, string path)
        {
            var token = o.SelectToken(path);
            if (token == null)
            {
                return default(T);
            }

            if (typeof(T).IsSubclassOf(typeof(JToken)))
            {
                return (T)(object)token;
            }
            else
            {
                return token.ToObject<T>();
            }
        }

        public static IEnumerable<JToken> GetItems(this JToken o, string path)
        {
            var tokens = o.SelectToken(path).Values<JToken>();
            if (tokens == null)
            {
                return Enumerable.Empty<JToken>();
            }

            return tokens;
        }

        public static IEnumerable<T> GetItems<T>(this JToken o, string path)
        {
            var tokens = o.SelectToken(path).Values<JToken>();
            if (tokens == null)
            {
                return Enumerable.Empty<T>();
            }

            return tokens.Select(t => t.ToObject<T>());
        }
    }
}
