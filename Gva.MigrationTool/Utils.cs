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
using Gva.Api.LotEventHandlers;
using Regs.Api.LotEvents;
using Gva.Api.LotEventHandlers.AircraftView;
using Gva.Api.LotEventHandlers.InventoryView;
using Gva.Api.LotEventHandlers.ApplicationView;
using Gva.Api.LotEventHandlers.PersonView;
using Gva.Api.LotEventHandlers.OrganizationView;
using Gva.Api.LotEventHandlers.EquipmentView;
using Gva.Api.LotEventHandlers.AirportView;
using Common.Api.Repositories.UserRepository;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Newtonsoft.Json;

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
        public static readonly JObject DUMMY_PERSON =
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "Персонал 1"));
        public static readonly JObject DUMMY_ORGANIZATION =
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "Организация 1"));
        public static readonly JObject DUMMY_AIRCRAFT =
            new JObject(
                new JProperty("nomValueId", 1),
                new JProperty("name", "Въздухоплавателно Средство 1"));

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

        public static JObject GetPerson(int? oldId, IPersonRepository personRepository, Dictionary<int, int> personOldIdsLotIds)
        {
            if (oldId == null)
            {
                return null;
            }

            int? id = personOldIdsLotIds.ByKeyOrDefault(oldId);

            if (id == null)
            {
                Console.WriteLine("CANNOT FIND PERSON WITH ID {0}", oldId);
                return DUMMY_PERSON;//TODO remove
            }

            var person = personRepository.GetPerson((int)id);
            return JObject.FromObject(new
            {
                nomValueId = person.LotId,
                name = person.Names
            });
        }

        public static JObject GetAircraft(int? oldId, IAircraftRepository aircraftRepository, Dictionary<int, int> aircraftOldIdsLotIds)
        {
            if (oldId == null)
            {
                return null;
            }

            int? id = aircraftOldIdsLotIds.ByKeyOrDefault(oldId);

            if (id == null)
            {
                Console.WriteLine("CANNOT FIND AIRCRAFT WITH ID {0}", oldId);
                return DUMMY_AIRCRAFT; //TODO remove
            }

            var aircraft = aircraftRepository.GetAircraft((int)id);
            return JObject.FromObject(new
            {
                nomValueId = aircraft.LotId,
                name = aircraft.Model
            });
        }

        public static JObject GetOrganization(int? oldId, IOrganizationRepository organizationRepository, Dictionary<int, int> organizationOldIdsLotIds)
        {
            if (oldId == null)
            {
                return null;
            }

            int? id = organizationOldIdsLotIds.ByKeyOrDefault(oldId);

            if (id == null)
            {
                Console.WriteLine("CANNOT FIND ORGANIZATION WITH ID {0}", oldId);
                return DUMMY_ORGANIZATION; //TODO remove
            }

            var organization = organizationRepository.GetOrganization((int)id);
            return JObject.FromObject(new
            {
                nomValueId = organization.LotId,
                name = organization.Name
            });
        }
    }
}
