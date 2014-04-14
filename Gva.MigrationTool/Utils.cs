using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.Api.Repositories.FileRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Common.Json;
using Gva.Api.ModelsDO;
using Common.Api.UserContext;
using Common.Data;
using Common.Api.Models;
using Docs.Api.Models;
using Gva.Api.Models;

namespace Gva.MigrationTool
{
    public class Utils
    {
        public const string DUMMY_FILE_KEY = "7C0604F9-FB44-4CCD-BE0E-66E82142AE76";
        public static readonly JObject DUMMY_PILOT_CASE_TYPE = 
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "Пилот"));
        public static readonly JObject DUMMY_ORGANIZATION =
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "org1"));
        public static readonly JObject DUMMY_AIRCRAFT =
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "ac1"));

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

        public static IUnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(
                new IDbConfiguration[]
                {
                    new RegsDbConfiguration(),
                    new CommonDbConfiguration(),
                    new DocsDbConfiguration(),
                    new GvaDbConfiguration()
                });
        }
    }
}
