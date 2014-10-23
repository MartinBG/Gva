using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public static Task RunParallel<TActionContext>(string numberOfParallelActionsKey, CancellationToken ct, Func<TActionContext> actionContextFactory, Action<TActionContext> action)
        {
            int numberOfParallelTasks = int.Parse(ConfigurationManager.AppSettings[numberOfParallelActionsKey]);
            var actionContexts = Enumerable.Range(0, numberOfParallelTasks).Select(i => actionContextFactory());
            var parallelTasks = actionContexts
                .Select(ac => Task.Run(() => action(ac), ct))
                .ToArray();

            return Task.WhenAll(parallelTasks);
        }

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

        public static bool FmCheckValue(string val)
        {
            return !String.IsNullOrEmpty(val) && val.Trim() != "n/a" && val.Trim() != "n / a";
        }

        public static int? FmToNum(string val)
        {
            int value;
            if (FmCheckValue(val) && Int32.TryParse(val.Trim(), out value))
            {
                return value;
            }

            return null;
        }

        public static decimal? FmToDecimal(string val)
        {
            decimal value;
            if (FmCheckValue(val) && decimal.TryParse(val.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }

            return null;
        }

        public static DateTime? FmToDate(string val)
        {
            DateTime value;
            if (FmCheckValue(val) && DateTime.TryParseExact(val.Trim(), "d.M.yyyy", null, System.Globalization.DateTimeStyles.None, out value))
            {
                return value;
            }

            return null;
        }
    }
}
