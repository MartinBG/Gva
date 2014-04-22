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
using Autofac.Features.OwnedInstances;
using Common.Tests;

namespace Gva.MigrationTool.Sets
{
    public class Organization
    {
        public Dictionary<int, int> personOldIdsLotIds;
        public Dictionary<int, int> aircraftOldIdsLotIds;
        public Dictionary<int, int> organizationOldIdsLotIds;
        private Dictionary<string, Dictionary<string, NomValue>> noms;

        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public Organization(OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void createOrganizationsLots(Dictionary<string, Dictionary<string, NomValue>> n)
        {
            noms = n;
            Dictionary<int, Lot> oldIdsLots = new Dictionary<int, Lot>();
            var organizationIds = this.getOrganizationIds(oracleConn);

            using (var dependencies = this.dependencyFactory())
            {
                var unitOfWork = dependencies.Value.Item1;
                var lotRepository = dependencies.Value.Item2;
                var userRepository = dependencies.Value.Item3;
                var fileRepository = dependencies.Value.Item4;
                var applicationRepository = dependencies.Value.Item5;
                var lotEventDispatcher = dependencies.Value.Item6;
                var context = dependencies.Value.Item7;

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
                    var organizationData = this.getOrganizationData(oracleConn, organizationId);
                    lot.CreatePart("organizationData", organizationData, context);
                    lot.Commit(context, lotEventDispatcher);
                    unitOfWork.Save();
                    Console.WriteLine("Created organizationData part for organization with id {0}", organizationId);
                }
            }

            organizationOldIdsLotIds = oldIdsLots.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.LotId);
        }

        public void migrateOrganizations(Dictionary<string, Dictionary<string, NomValue>> n, Dictionary<int, int> aIdsLots, Dictionary<int, int> pIdsLots)
        {

            personOldIdsLotIds = pIdsLots;
            aircraftOldIdsLotIds = aIdsLots;
            noms = n;
        }

        private IList<int> getOrganizationIds(OracleConnection con)
        {
            return con.CreateStoreCommand("SELECT ID FROM CAA_DOC.FIRM")
                .Materialize(r => (int)r.Field<decimal>("ID"))
                    .ToList();
        }

        private JObject getOrganizationData(OracleConnection con, int organizationId)
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

        private IList<JObject> getOrganizationAddress(OracleConnection con, int organizationId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FIRM_ADDRESS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "FIRM_ADDRESS",
                        addressTypeId = r.Field<decimal?>("ADDRESS_TYPE_ID"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        settlementId = r.Field<decimal?>("TOWN_VILLAGE_ID"),
                        address = r.Field<string>("ADDRESS"),
                        addressAlt = r.Field<string>("ADDRESS_TRANS"),
                        phone = r.Field<string>("PHONES"),
                        fax = r.Field<string>("FAXES"),
                        postalCode = r.Field<string>("POSTAL_CODE"),
                        contactPerson = r.Field<string>("CONTACT_PERSON"),
                        email = r.Field<string>("E_MAIL")
                    }))
                .ToList();
        }
    }
}
