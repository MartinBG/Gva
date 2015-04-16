using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Gva.FixFlyingExpMigrationTool
{
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

        public static long? TimeToMilliseconds(int? hours, int? minutes)
        {
            if (!hours.HasValue && !minutes.HasValue)
            {
                return null;
            }
            hours = hours.HasValue ? hours.Value : 0;
            minutes = minutes.HasValue ? minutes.Value : 0;

            return long.Parse(((hours * 60) + minutes).ToString()) * 60000;
        }
    }
}
