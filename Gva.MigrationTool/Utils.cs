using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Gva.MigrationTool
{
    public static class DictionaryExtensions
    {
        public static Nullable<TValue> ByKey<TKey, TValue>(this Dictionary<TKey, TValue> dict, Nullable<TKey> key)
            where TKey : struct
            where TValue : struct
        {
            if (key == null)
            {
                return null;
            }

            return dict[key.Value];
        }

        public static Nullable<TValue> ByKeyOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, Nullable<TKey> key)
            where TKey : struct
            where TValue : struct
        {
            if (key == null || !dict.ContainsKey(key.Value))
            {
                return null;
            }

            return dict[key.Value];
        }
    }

    public class Utils
    {
        public const string DUMMY_FILE_KEY = "7C0604F9-FB44-4CCD-BE0E-66E82142AE76";
        public static readonly JObject DUMMY_PILOT_CASE_TYPE = 
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "Пилот"));
        public static readonly JObject DUMMY_APPROVED_ORG_CASE_TYPE = 
            new JObject(
                new JProperty("nomValueId", 3),
                new JProperty("name", "ОО"),
                new JProperty("alias", "approvedOrg"));

        public static JObject ToJObject(object o)
        {
            return JObject.FromObject(o);
        }

        public static JObject Pluck(JObject o, string[] keys)
        {
            if (o == null)
            {
                return null;
            }

            var keysToRemove =
                ((IDictionary<string, JToken>)o).Keys
                .Where(k => !keys.Contains(k))
                .ToList();

            foreach (var key in keysToRemove)
            {
                o.Remove(key);
            }

            return o;
        }

        public static int? TimeToMilliseconds(int? hours, int? minutes)
        {
            if (!hours.HasValue && !minutes.HasValue)
            {
                return null;
            }
            hours = hours.HasValue ? hours.Value : 0;
            minutes = minutes.HasValue ? minutes.Value : 0;

            return ((hours * 60) + minutes) * 60000;
        }
    }
}
