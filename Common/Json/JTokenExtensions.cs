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
            var token = o.SelectToken(path);
            if (token == null || token.Type == JTokenType.Null)
            {
                return null;
            }

            return token;
        }

        public static T Get<T>(this JToken o, string path)
        {
            var token = o.SelectToken(path);
            if (token == null || token.Type == JTokenType.Null)
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
            var token = o.SelectToken(path);
            if (token == null || token.Type == JTokenType.Null)
            {
                return Enumerable.Empty<JToken>();
            }

            return token;
        }

        public static IEnumerable<T> GetItems<T>(this JToken o, string path)
        {
            var token = o.SelectToken(path);
            if (token == null || token.Type == JTokenType.Null)
            {
                return Enumerable.Empty<T>();
            }

            return token.Select(t => t.ToObject<T>());
        }
    }
}
