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
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    //NullValueHandling = NullValueHandling.Ignore, //TODO remove before final migration
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
                Dictionary<int, int> personIdToLotId = new Dictionary<int, int>();
                Dictionary<int, int> organizationIdtoLotId = new Dictionary<int, int>();

                //create aircraft lots
                var aircraftIds = aircraft.createAircraftsLots(noms);
                aircraftApexIdtoLotId = aircraftIds.Item1;
                aircraftFmIdtoLotId = aircraftIds.Item2;

                //create person lots
                personIdToLotId = person.createPersonsLots(noms);

                //create organization lots
                organizationIdtoLotId = organization.createOrganizationsLots(noms);

                //migrate aircrafts
                aircraft.migrateAircrafts(
                    noms,
                    aircraftApexIdtoLotId,
                    aircraftFmIdtoLotId,
                    personIdToLotId,
                    organizationIdtoLotId);

                //migrate persosns
                person.migratePersons(noms, aircraftApexIdtoLotId, organizationIdtoLotId, personIdToLotId);

                //migrate organizations
                organization.migrateOrganizations(noms, aircraftApexIdtoLotId, personIdToLotId, organizationIdtoLotId);

                timer.Stop();
                Console.WriteLine("Migration time - {0}", timer.Elapsed.TotalMinutes);
            }

            Console.WriteLine("Migration finished!");
        }
    }
}
