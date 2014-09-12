using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Common.Data;
using Regs.Api.Models;
using Common.Api.Models;
using Docs.Api.Models;
using Gva.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Common.Api.UserContext;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Gva.MigrationTool.Sets;
using Gva.MigrationTool.Nomenclatures;
using Regs.Api.LotEvents;
using Newtonsoft.Json.Serialization;
using System.Data.SqlClient;
using System.Data.Common;
using Autofac;
using Common;
using Common.Api;
using Docs.Api;
using Gva.Api;
using Regs.Api;

namespace Gva.MigrationTool
{
    class Migration
    {
        private const string oracleConnStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.19)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=VENI.CAA)));User Id=system;Password=DBSYSTEMVENI;";
        private const string sqlConnStr = "Data Source=.\\;Initial Catalog=GvaAircraft;Integrated Security=True;MultipleActiveResultSets=True";

        public static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new CommonApiModule());
            builder.RegisterModule(new DocsApiModule());
            builder.RegisterModule(new GvaApiModule());
            builder.RegisterModule(new RegsApiModule());

            builder.Register(c => new OracleConnection(oracleConnStr)).InstancePerLifetimeScope();
            builder.Register(c => new SqlConnection(sqlConnStr)).InstancePerLifetimeScope();
            builder.Register(c => new UserContext(2)).InstancePerLifetimeScope();

            builder.RegisterType<Nomenclature>().InstancePerLifetimeScope();
            builder.RegisterType<Aircraft>().InstancePerLifetimeScope();
            builder.RegisterType<Person>().InstancePerLifetimeScope();
            builder.RegisterType<Organization>().InstancePerLifetimeScope();

            return builder.Build();
        }

        static void Main(string[] args)
        {
            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings()
                {
#if DEBUG
                    Formatting = Formatting.Indented,
#else
                    Formatting = Formatting.None,
#endif
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            };

            var container = CreateAutofacContainer();

            using (var scope = container.BeginLifetimeScope())
            {
                OracleConnection oracleConn = scope.Resolve<OracleConnection>();
                SqlConnection sqlConn = scope.Resolve<SqlConnection>();

                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                timer.Start();

                oracleConn.Open();
                sqlConn.Open();

                var nomenclature = scope.Resolve<Nomenclature>();
                var aircraft = scope.Resolve<Aircraft>();
                var person = scope.Resolve<Person>();
                var organization = scope.Resolve<Organization>();

                var noms = nomenclature.migrateNomenclatures();

                Dictionary<int, int> aircraftApexIdtoLotId = new Dictionary<int, int>();
                Dictionary<string, int> aircraftFmIdtoLotId = new Dictionary<string, int>();
                Dictionary<int, JObject> aircraftLotIdToAircraftNom = new Dictionary<int, JObject>();

                Dictionary<int, int> personIdToLotId = new Dictionary<int, int>();
                Dictionary<string, int> personEgnToLotId = new Dictionary<string, int>();
                Dictionary<int, JObject> personLotIdToPersonNom = new Dictionary<int, JObject>();

                Dictionary<int, int> orgIdtoLotId = new Dictionary<int, int>();
                Dictionary<string, int> orgNameEnToLotId = new Dictionary<string, int>();
                Dictionary<string, int> orgUinToLotId = new Dictionary<string, int>();
                Dictionary<int, JObject> orgLotIdToOrgNom = new Dictionary<int, JObject>();

                //create aircraft lots
                var aircrafts = aircraft.createAircraftsLots(noms);
                aircraftApexIdtoLotId = aircrafts.Item1;
                aircraftFmIdtoLotId = aircrafts.Item2;
                aircraftLotIdToAircraftNom = aircrafts.Item3;

                Func<int?, JObject> getAircraftByApexId = (apexId) =>
                {
                    if (apexId == null)
                    {
                        return null;
                    }

                    int? id = aircraftApexIdtoLotId.ByKeyOrDefault(apexId);

                    if (id == null)
                    {
                        Console.WriteLine("CANNOT FIND AIRCRAFT WITH APEX ID {0}", apexId);
                        return null;//TODO throw
                    }

                    return aircraftLotIdToAircraftNom[id.Value];
                };

                //create person lots
                var persons = person.createPersonsLots(noms);
                personIdToLotId = persons.Item1;
                personEgnToLotId = persons.Item2;
                personLotIdToPersonNom = persons.Item3;

                Func<int?, JObject> getPersonByApexId = (apexId) =>
                {
                    if (apexId == null)
                    {
                        return null;
                    }

                    int? id = personIdToLotId.ByKeyOrDefault(apexId);

                    if (id == null)
                    {
                        Console.WriteLine("CANNOT FIND PERSON WITH APEX ID {0}", apexId);
                        return null;//TODO throw
                    }

                    return personLotIdToPersonNom[id.Value];
                };

                //create organization lots
                var orgs = organization.createOrganizationsLots(noms);
                orgIdtoLotId = orgs.Item1;
                orgNameEnToLotId = orgs.Item2;
                orgUinToLotId = orgs.Item3;
                orgLotIdToOrgNom = orgs.Item4;

                Func<int?, JObject> getOrgByApexId = (apexId) =>
                {
                    if (apexId == null)
                    {
                        return null;
                    }

                    int? id = orgIdtoLotId.ByKeyOrDefault(apexId);

                    if (id == null)
                    {
                        Console.WriteLine("CANNOT FIND ORGANIZATION WITH APEX ID {0}", apexId);
                        return null; //TODO throw
                    }

                    return orgLotIdToOrgNom[id.Value];
                };

                FmOrgMatcher fmOrgMatcher = new FmOrgMatcher();
                var matches = fmOrgMatcher.Parse(
                    @"..\..\..\Gva.OrgMatchingTool\bin\Debug\Organizations.xls",
                    personEgnToLotId,
                    orgNameEnToLotId,
                    orgUinToLotId);

                Dictionary<string, int> fmOrgNameEnToPersonLotId = matches.Item1;
                Dictionary<string, int> fmOrgNameEnToOrgLotId = matches.Item2;

                Func<string, JObject> getPersonByFmOrgName = (fmOrgName) =>
                {
                    if (!fmOrgNameEnToPersonLotId.ContainsKey(fmOrgName))
                    {
                        return null;
                    }

                    return personLotIdToPersonNom[fmOrgNameEnToPersonLotId[fmOrgName]];
                };

                Func<string, JObject> getOrgByFmOrgName = (fmOrgName) =>
                {
                    if (!fmOrgNameEnToOrgLotId.ContainsKey(fmOrgName))
                    {
                        return null;
                    }

                    return orgLotIdToOrgNom[fmOrgNameEnToOrgLotId[fmOrgName]];
                };

                //migrate aircrafts
                aircraft.migrateAircrafts(
                    noms,
                    aircraftApexIdtoLotId,
                    aircraftFmIdtoLotId,
                    getPersonByApexId,
                    getOrgByApexId,
                    getPersonByFmOrgName,
                    getOrgByFmOrgName);

                //migrate persosns
                person.migratePersons(noms, personIdToLotId, getAircraftByApexId, getPersonByApexId, getOrgByApexId);

                //migrate organizations
                organization.migrateOrganizations(noms, orgIdtoLotId, getAircraftByApexId, getPersonByApexId);

                Console.WriteLine("Rebuilding lot part tokens...");
                var lotRepository = scope.Resolve<ILotRepository>();
                lotRepository.ExecSpSetLotPartTokens();

                timer.Stop();
                Console.WriteLine("Migration time - {0}", timer.Elapsed.TotalMinutes);
            }

            Console.WriteLine("Migration finished!");
        }
    }
}
