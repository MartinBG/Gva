using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using Autofac;
using Common;
using Common.Api;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api;
using Gva.Api;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.MigrationTool.AircarftRadios;
using Gva.MigrationTool.Blobs;
using Gva.MigrationTool.Nomenclatures;
using Gva.MigrationTool.Sets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using Regs.Api;
using Regs.Api.Repositories.LotRepositories;
using Autofac.Extras.Attributed;

namespace Gva.MigrationTool
{
    class Migration
    {
        public static object isPartialSyncRoot = new object();
        public static bool? isPartial = null;
        public static bool IsPartialMigration
        {
            get
            {
                if (isPartial == null)
                {
                    lock (isPartialSyncRoot)
                    {
                        if (isPartial == null)
                        {
                            bool isPartialSetting;
                            if (bool.TryParse(ConfigurationManager.AppSettings["PartialMigration"], out isPartialSetting))
                            {
                                isPartial = isPartialSetting;
                            }
                            else
                            {
                                isPartial = false;
                            }
                        }
                    }
                }

                return isPartial.Value;
            }
        }

        private static ContainerBuilder CreateGvaContainerBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new CommonApiModule());
            builder.RegisterModule(new DocsApiModule());
            builder.RegisterModule(new GvaApiModule());
            builder.RegisterModule(new RegsApiModule());

            builder.Register(c => new UserContext(2)).InstancePerLifetimeScope();
            builder.Register(c =>
            {
                var unitOfWork = new UnitOfWork(c.Resolve<IEnumerable<IDbConfiguration>>(), c.Resolve<IEnumerable<IDbContextInitializer>>());
                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;
                return unitOfWork;
            }).As<IUnitOfWork>().InstancePerLifetimeScope();

            return builder;
        }

        private static IContainer CreateBlobContainer()
        {
            ContainerBuilder builder = CreateGvaContainerBuilder();

            builder.Register(c => new OracleConnection(ConfigurationManager.ConnectionStrings["Apex"].ConnectionString)).InstancePerLifetimeScope();
            builder.Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString)).InstancePerLifetimeScope();

            builder.RegisterType<Gva.MigrationTool.Blobs.Blob>().InstancePerLifetimeScope();
            builder.RegisterType<BlobUploader>().InstancePerLifetimeScope();
            builder.RegisterType<BlobDownloader>().InstancePerLifetimeScope();

            return builder.Build();
        }

        private static IContainer CreateSetContainer()
        {
            ContainerBuilder builder = CreateGvaContainerBuilder();

            builder.Register(c => new OracleConnection(ConfigurationManager.ConnectionStrings["Apex"].ConnectionString)).InstancePerLifetimeScope();
            builder.Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["GvaAircraft"].ConnectionString)).Keyed<SqlConnection>("gvaAircraft").InstancePerLifetimeScope();
            builder.Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["SCodes"].ConnectionString)).Keyed<SqlConnection>("sCodes").InstancePerLifetimeScope();

            builder.RegisterType<Nomenclature>().InstancePerLifetimeScope().WithAttributeFilter();

            builder.RegisterType<Person>().InstancePerLifetimeScope();
            builder.RegisterType<PersonLotCreator>().InstancePerLifetimeScope();
            builder.RegisterType<PersonLotMigrator>().InstancePerLifetimeScope();
            builder.RegisterType<PersonLicenceDocMigrator>().InstancePerLifetimeScope();
            builder.RegisterType<ExaminationSystemDataMigrator>().InstancePerLifetimeScope();

            builder.RegisterType<Organization>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationLotCreator>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationFmLotCreator>().InstancePerLifetimeScope().WithAttributeFilter();
            builder.RegisterType<OrganizationLotMigrator>().InstancePerLifetimeScope();

            builder.RegisterType<Aircraft>().InstancePerLifetimeScope().WithAttributeFilter();
            builder.RegisterType<AircraftApexLotCreator>().InstancePerLifetimeScope();
            builder.RegisterType<AircraftApexLotMigrator>().InstancePerLifetimeScope();
            builder.RegisterType<AircraftFmLotCreator>().InstancePerLifetimeScope().WithAttributeFilter();
            builder.RegisterType<AircraftFmLotMigrator>().InstancePerLifetimeScope().WithAttributeFilter();
            builder.RegisterType<AircraftRadioCertsMigrator>().InstancePerLifetimeScope();

            builder.RegisterType<SModeCode>().InstancePerLifetimeScope().WithAttributeFilter();
            builder.RegisterType<SModeCodeLotCreator>().InstancePerLifetimeScope().WithAttributeFilter();

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

            var blobContainer = CreateBlobContainer();
            Dictionary<int, string> blobIdsToFileKeys;
            using (var scope = blobContainer.BeginLifetimeScope())
            {
                var blob = scope.Resolve<Gva.MigrationTool.Blobs.Blob>();
                blobIdsToFileKeys = blob.migrateBlobs();
            }

            var setContainer = CreateSetContainer();
            using (var scope = setContainer.BeginLifetimeScope())
            {
                OracleConnection oracleConn = scope.Resolve<OracleConnection>();
                SqlConnection sqlConnGvaAircraft = scope.ResolveKeyed<SqlConnection>("gvaAircraft");
                SqlConnection sqlConnSCodes = scope.ResolveKeyed<SqlConnection>("sCodes");

                Stopwatch timer = new Stopwatch();
                timer.Start();

                oracleConn.Open();
                sqlConnGvaAircraft.Open();
                sqlConnSCodes.Open();

                var nomenclature = scope.Resolve<Nomenclature>();
                var aircraft = scope.Resolve<Aircraft>();
                var person = scope.Resolve<Person>();
                var organization = scope.Resolve<Organization>();
                var sModeCode = scope.Resolve<SModeCode>();

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
                Dictionary<int, int> orgOperatorIdToLotId = new Dictionary<int, int>();

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
              

                Func<int?, JObject> getOrgByApexId = (apexId) =>
                {
                    if (!apexId.HasValue)
                    {
                        return null;
                    }

                    int id = -1;
                    if (!orgIdtoLotId.TryGetValue(apexId.Value, out id))
                    {
                        Console.WriteLine("CANNOT FIND ORGANIZATION WITH APEX ID {0}", apexId);
                        return null; //TODO throw
                    }

                    return orgLotIdToOrgNom[id];
                };

                FmOrgMatcher fmOrgMatcher = new FmOrgMatcher();
                var result = fmOrgMatcher.Parse(
                    @"..\..\..\Gva.OrgMatchingTool\bin\Debug\Organizations.xls",
                    personEgnToLotId,
                    orgs.Item2,
                    orgs.Item3);

                ConcurrentDictionary<string, int> orgNameToLotId = new ConcurrentDictionary<string, int>(result.Item1);
                ConcurrentQueue<int> notCreatedFmOrgIds = new ConcurrentQueue<int>(result.Item2);

                var res = organization.createOrganizationsFmLots(noms, notCreatedFmOrgIds, orgNameToLotId, orgs.Item3, orgs.Item4);
                orgNameEnToLotId = res.Item1;
                orgUinToLotId = res.Item2;
                orgLotIdToOrgNom = res.Item3;
                orgOperatorIdToLotId = res.Item4;

                Func<string, JObject> getPersonByFmOrgName = (fmOrgName) =>
                {
                    if (!orgNameToLotId.ContainsKey(fmOrgName))
                    {
                        return null;
                    }

                    return personLotIdToPersonNom[orgNameToLotId[fmOrgName]];
                };

                Func<string, JObject> getOrgByFmOrgName = (fmOrgName) =>
                {
                    if (!orgNameToLotId.ContainsKey(fmOrgName))
                    {
                        return null;
                    }

                    if (!orgLotIdToOrgNom.ContainsKey(orgNameToLotId[fmOrgName]))
                    {
                        return null;
                    }

                    return orgLotIdToOrgNom[orgNameToLotId[fmOrgName]];
                };

                Func<int, JObject> getOrgBySModeCodeOperId = (operId) =>
                {
                    if (!orgOperatorIdToLotId.ContainsKey(operId))
                    {
                        return null;
                    }

                    if (!orgLotIdToOrgNom.ContainsKey(orgOperatorIdToLotId[operId]))
                    {
                        return null;
                    }

                    return orgLotIdToOrgNom[orgOperatorIdToLotId[operId]];
                };
                
                //migrate aircrafts
                ConcurrentDictionary<string, int> regMarkToLotId = aircraft.migrateAircrafts(
                    noms,
                    personIdToLotId,
                    aircraftApexIdtoLotId,
                    aircraftFmIdtoLotId,
                    getPersonByApexId,
                    getOrgByApexId,
                    getPersonByFmOrgName,
                    getOrgByFmOrgName,
                    blobIdsToFileKeys);

                AircraftRadios radios = new AircraftRadios();
                var resultRadios = radios.Get(
                    @"..\..\..\Gva.MigrationTool\Sets\AircraftRadios\Radio_Certificates.xls",
                    noms,
                    regMarkToLotId,
                    getOrgByFmOrgName);

                //migrate aircraft radio certificates
                aircraft.migrateAircraftRadioCerts(noms, resultRadios);

                //create s-mode codes
                sModeCode.createSModeCodesLots(noms, regMarkToLotId, getOrgBySModeCodeOperId);

                //migrate persons
                person.migratePersons(noms, personIdToLotId, getAircraftByApexId, getPersonByApexId, getOrgByApexId, blobIdsToFileKeys);

                //migrate organizations
                organization.migrateOrganizations(noms, personIdToLotId, orgIdtoLotId, getAircraftByApexId, getPersonByApexId, blobIdsToFileKeys);

                bool migrateExaminationData;
                bool.TryParse(ConfigurationManager.AppSettings["MigrateExaminationData"], out migrateExaminationData);
                if (migrateExaminationData)
                {
                    Stopwatch examinationSystemDataTimer = new Stopwatch();
                    person.migrateExaminationSystemData(personIdToLotId);
                    examinationSystemDataTimer.Stop();
                    Console.WriteLine("Migration of data from examination system time - {0}", examinationSystemDataTimer.Elapsed.TotalMinutes);
                }

                var lotRepository = scope.Resolve<ILotRepository>();
                var unitOfWork = scope.Resolve<IUnitOfWork>();
                ((IObjectContextAdapter)unitOfWork.DbContext).ObjectContext.CommandTimeout = 0;//wait indefinitely

                // rebuild indexes
                Stopwatch indexRebuildTimer = new Stopwatch();
                indexRebuildTimer.Start();
                Console.WriteLine("Rebuilding indexes...");

                unitOfWork.DbContext.Database.ExecuteSqlCommand("exec sp_MSforeachtable 'SET QUOTED_IDENTIFIER ON; ALTER INDEX ALL ON ? REBUILD'");

                indexRebuildTimer.Stop();
                Console.WriteLine("Rebuilding indexes time - {0}", indexRebuildTimer.Elapsed.TotalMinutes);

                // rebuild lot part tokens
                Stopwatch tokenRebuildTimer = new Stopwatch();
                tokenRebuildTimer.Start();
                Console.WriteLine("Rebuilding lot part tokens...");

                lotRepository.ExecSpRebuildLotPartTokens();

                tokenRebuildTimer.Stop();
                Console.WriteLine("Rebuilding lot part tokens time - {0}", tokenRebuildTimer.Elapsed.TotalMinutes);

                bool migrateLicences;
                bool.TryParse(ConfigurationManager.AppSettings["MigrateLicences"], out migrateLicences);
                if (migrateLicences)
                {
                    Stopwatch personLicenceDocumentsTimer = new Stopwatch();
                    person.migrateLicenceDocuments(personIdToLotId);
                    personLicenceDocumentsTimer.Stop();
                    Console.WriteLine("Migration of person licences documents time - {0}", personLicenceDocumentsTimer.Elapsed.TotalMinutes);
                }

                timer.Stop();
                Console.WriteLine("Migration time - {0}", timer.Elapsed.TotalMinutes);
            }

            Console.WriteLine("Migration finished!");
        }
    }
}
