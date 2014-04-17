using Common.Api.UserContext;
using Common.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.MigrationTool.Nomenclatures;
using Common.Api.Models;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.LotEventHandlers.PersonView;
using Gva.Api.LotEventHandlers.OrganizationView;
using Gva.Api.LotEventHandlers.InventoryView;
using Gva.Api.LotEventHandlers.ApplicationView;
using Common.Api.Repositories.UserRepository;
using Regs.Api.LotEvents;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.ModelsDO;
using Common.Json;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.LotEventHandlers.EquipmentView;
using Gva.Api.LotEventHandlers.AircraftView;
using Gva.Api.LotEventHandlers.AirportView;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.OrganizationRepository;

namespace Gva.MigrationTool.Sets
{
    public static class Organization
    {
        public static Dictionary<int, int> personOldIdsLotIds;
        public static Dictionary<int, int> aircraftOldIdsLotIds;
        public static Dictionary<int, int> organizationOldIdsLotIds;
        public static Dictionary<string, Dictionary<string, NomValue>> noms;

        public static void createOrganizationsLots(OracleConnection oracleCon, Dictionary<string, Dictionary<string, NomValue>> n)
        {
            noms = n;
            var context = new UserContext(2);
            Dictionary<int, Lot> oldIdsLots = new Dictionary<int, Lot>();
            var organizationIds = Organization.getOrganizationIds(oracleCon);

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                var userContext = new UserContext(2);
                var lotRepository = new LotRepository(unitOfWork);
                var caseTypeRepository = new CaseTypeRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);
                var fileRepository = new FileRepository(unitOfWork);
                var applicationRepository = new ApplicationRepository(unitOfWork);
                var aircraftRegistrationAwRepository = new AircraftRegistrationAwRepository(unitOfWork);
                var organizationRepository = new OrganizationRepository(unitOfWork);
                var aircraftRepository = new AircraftRepository(unitOfWork);

                var lotEventDispatcher = Utils.CreateLotEventDispatcher(unitOfWork, userRepository, aircraftRegistrationAwRepository);

                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                Set organizationSet = lotRepository.GetSet("Organization");

                foreach (var organizationId in organizationIds)
                {
                    if (organizationId >= 200)
                    {
                        break;
                    }
                    var lot = organizationSet.CreateLot(context);
                    oldIdsLots.Add(organizationId, lot);
                    var organizationData = Organization.getOrganizationData(oracleCon, organizationId);
                    lot.CreatePart("organizationData", organizationData, context);
                    lot.Commit(context, lotEventDispatcher);
                    unitOfWork.Save();
                    Console.WriteLine("Created organizationData part for organization with id {0}", organizationId);
                }
            }

            organizationOldIdsLotIds = oldIdsLots.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.LotId);
        }

        public static void migrateOrganizations(OracleConnection con, Dictionary<string, Dictionary<string, NomValue>> n, Dictionary<int, int> aIdsLots, Dictionary<int, int> pIdsLots)
        {

            personOldIdsLotIds = pIdsLots;
            aircraftOldIdsLotIds = aIdsLots;
            noms = n;
        }

        public static IList<int> getOrganizationIds(OracleConnection con)
        {
            return con.CreateStoreCommand("SELECT ID FROM CAA_DOC.FIRM")
                .Materialize(r => (int)r.Field<decimal>("ID"))
                    .ToList();
        }

        public static JObject getOrganizationData(OracleConnection con, int organizationId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FIRM WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "FIRM",
                        name = r.Field<string>("NAME"),
                        nameAlt = r.Field<string>("NAME_TRANS"),
                        code = r.Field<string>("CODE"),
                        uin = r.Field<string>("BULSTAT"),
                        CAO = r.Field<string>("CAO"),
                        dateСАОFirstIssue = r.Field<DateTime?>("CAO_ISS"),
                        dateСАОLastIssue = r.Field<DateTime?>("CAO_LAST"),
                        dateСАОValidTo = r.Field<DateTime?>("CAO_VALID"),
                        ICAO = r.Field<string>("ICAO"),
                        IATA = r.Field<string>("IATA"),
                        SITA = r.Field<string>("SITA"),
                        organizationType = noms["organizationTypes"].ByOldId(r.Field<long?>("ID_FIRM_TYPE").ToString()),
                        organizationKind = noms["organizationKinds"].ByCode(r.Field<string>("TYPE_ORG")),
                        phones = r.Field<string>("PHONES"),
                        webSite = r.Field<string>("WEB_SITE"),
                        Note = r.Field<string>("REMARKS"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        dateValidTo = r.Field<DateTime?>("VALID_TO_DATE"),
                        docRoom = r.Field<string>("DOC_ROOM"),
                    }))
                .Single();
        }
    }
}
