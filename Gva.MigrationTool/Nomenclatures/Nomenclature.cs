using Common.Api.Models;
using Common.Data;
using Docs.Api.Models;
using Gva.Api.Models;
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
using Common.Api.Repositories.NomRepository;
using System.Data.SqlClient;

namespace Gva.MigrationTool.Nomenclatures
{
    public static class Nomenclature
    {
        public static NomValue ByCodeOrDefault(this Dictionary<string, NomValue> nomValues, string code)
        {
            return nomValues.Where(v => v.Value.Code == code).Select(v => v.Value).SingleOrDefault();
        }

        public static NomValue ByCode(this Dictionary<string, NomValue> nomValues, string code)
        {
            if (code == null)
            {
                return null;
            }

            return nomValues.Where(v => v.Value.Code == code).Select(v => v.Value).Single();
        }

        public static NomValue ByName(this Dictionary<string, NomValue> nomValues, string name)
        {
            if (name == null)
            {
                return null;
            }

            return nomValues.Where(v => v.Value.Name == name).Select(v => v.Value).Single();
        }

        public static NomValue ByOldId(this Dictionary<string, NomValue> nomValues, string OldId)
        {
            if (OldId == null || !nomValues.ContainsKey(OldId))
            {
                return null;
            }

            return nomValues[OldId];
        }

        public static string Code(this NomValue nomValue)
        {
            return nomValue != null ? nomValue.Code : null;
        }

        public static string Alias(this NomValue nomValue)
        {
            return nomValue != null ? nomValue.Alias : null;
        }

        public static int? NomValueId(this NomValue nomValue)
        {
            return nomValue != null ? (int?)nomValue.NomValueId : null;
        }

        public static Dictionary<string, Dictionary<string, NomValue>> noms;

        public static void migrateNomenclatures(OracleConnection oracleConn, SqlConnection sqlConn)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            noms = new Dictionary<string, Dictionary<string, NomValue>>();

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                noms["boolean"] = repo.GetNomValues("boolean").ToDictionary(n => "no old id " + n.NomValueId);
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                noms["registers"] = repo.GetNomValues("registers").ToDictionary(n => "no old id " + n.NomValueId);
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                noms["linTypes"] = repo.GetNomValues("linTypes").ToDictionary(n => "no old id " + n.NomValueId);
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateGenders(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateCountires(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateCities(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAddressTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateStaffTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateEmploymentCategories(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateGraduations(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateSchools(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateDirections(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateDocumentTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateDocumentRoles(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migratePersonStatusTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateOtherDocPublishers(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateMedDocPublishers(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateRatingTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateRatingClassGroups(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateRatingClasses(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateRatingSubClasses(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuthorizationGroups(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuthorizations(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLicenceTypeDictionary(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLicenceTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLocationIndicators(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftTCHolders(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftTypeGroups(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftGroup66(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftClases66(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLimitations66(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateMedClasses(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateMedLimitation(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migratePersonExperienceRoles(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migratePersonExperienceMeasures(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateCaa(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migratePersonRatingLevels(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLicenceActions(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateOrganizationTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateOrganizationKinds(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateApplicationTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateApplicationPaymentTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateCurrencies(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateApprovalTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateApprovalStates(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLim147classes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLim147ratings(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLim147limitations(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLim145classes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateLim145limitations(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuditParts(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuditPartRequirmants(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuditPartSections(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuditPartSectionDetails(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuditReasons(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuditTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuditStatuses(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAuditResults(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftCategories(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftProducers(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftSCodeTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftRelations(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftParts(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftPartStatuses(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftDebtTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftOccurrenceClasses(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftCertificateTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftOperTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftTypeCertificateTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftRemovalReasons(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAirportTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAirportRelations(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftRadiotypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAirportOperatorActivityTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateGroundServiceOperatorActivityTypes(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migratePersonCheckRatingValues(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migratePersonRatingModels(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateEngLangLevels(repo, oracleConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftProducersFm(repo, sqlConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftLimitationsFm(repo, sqlConn);
                unitOfWork.Save();
            }

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                NomRepository repo = new NomRepository(unitOfWork);
                migrateAircraftRegStatsesFm(repo, sqlConn);
                unitOfWork.Save();
            }

            timer.Stop();
            Console.WriteLine("Nomenclatures migration time - {0}", timer.Elapsed.TotalMinutes);
        }

        public static void migrateGenders(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("gender");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SEX")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        ParentValueId = null,
                        Alias = null,
                        TextContent = null,
                        IsActive = true
                    })
                .ToList();

            noms["gender"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["gender"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateCountires(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("countries");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_COUNTRY")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        ParentValueId = null,
                        Alias = null,
                        TextContent = JsonConvert.SerializeObject(
                            new {
                                NationalityCodeCA = r.Field<string>("CA_NATIONALITY_CODE"),
                                Heading = r.Field<string>("HEADING"),
                                HeadingAlt =  r.Field<string>("HEADING_TRANS"),
                                LicenceCodeCA = r.Field<string>("CA_LICENCE_CODE"),
                            }),
                        IsActive = true
                    })
                .ToList();

            noms["countries"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["countries"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateCities(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("cities");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_TOWN_VILLAGE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        ParentValueId = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()).NomValueId(),
                        Alias = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                type = r.Field<string>("TV_TYPE"),
                                notes = r.Field<string>("NOTES"),
                                postCode = r.Field<string>("POSTAL_CODE_DEFAULT"),
                                oblCode = r.Field<string>("OBL_CODE"),
                                obstCode = r.Field<string>("OBST_CODE")
                            }),
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false
                    })
                .ToList();

            noms["cities"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["cities"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAddressTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("addressTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_ADDRESS_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        ParentValueId = null,
                        Alias = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                type = r.Field<string>("ADDRESS_TYPE")
                            }),
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false
                    })
                .ToList();

            noms["addressTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["addressTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateStaffTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("staffTypes");
            var aliases = new Dictionary<string, string>()
            {
                { "F", "flightCrew" },
                { "T", "ovd"},
                { "G", "to_vs"},
                { "M", "to_suvd"}
            };
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_STAFF_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        ParentValueId = null,
                        Alias = aliases[r.Field<string>("CODE")],
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                codeCA = r.Field<string>("CA_CODE")
                            }),
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false
                    })
                .ToList();

            noms["staffTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["staffTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }

            Nom trainingStaffTypesNom = repo.GetNom("trainingStaffTypes");
            var copiedResults = results.Select(v =>
                new NomValue
                {
                    OldId = v.OldId,
                    Code = v.Code,
                    Name = v.Name,
                    NameAlt = v.NameAlt,
                    ParentValueId = v.ParentValueId,
                    Alias = v.Alias,
                    TextContent = v.TextContent,
                    IsActive = v.IsActive
                });

            noms["trainingStaffTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in copiedResults)
            {
                noms["trainingStaffTypes"][row.OldId] = row;
                trainingStaffTypesNom.NomValues.Add(row);
            }

            NomValue generalDocument =
                new NomValue
                {
                    NomValueId = 0,
                    Name = "Общ документ",
                    Alias = "general"
                };

            noms["trainingStaffTypes"]["0"] = generalDocument;
            trainingStaffTypesNom.NomValues.Add(generalDocument);
        }

        public static void migrateEmploymentCategories(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("employmentCategories");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_JOB_CATEGORY")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        ParentValueId = (r.Field<decimal?>("STAFF_TYPE_ID") != null) ? noms["staffTypes"][r.Field<decimal?>("STAFF_TYPE_ID").ToString()].NomValueId : (int?)null,
                        Alias = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                codeCA = r.Field<string>("CA_CODE"),
                                dateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                                dateValidTo = r.Field<DateTime?>("DATE_TO")
                            }),
                        IsActive = true
                    })
                .ToList();

            noms["employmentCategories"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["employmentCategories"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateGraduations(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("graduations");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_GRADUATION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                rating = r.Field<short?>("RATING")
                            })
                    })
                .ToList();

            noms["graduations"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["graduations"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateSchools(INomRepository repo, OracleConnection conn)
        {
            Func<string, int?> getGradId = (oldId) => noms["graduations"].ByOldId(oldId).NomValueId();

            Nom nom = repo.GetNom("schools");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.SCHOOL")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                isPilotTraining = r.Field<string>("PILOT_TRAINING")== "Y" ? true : false,
                                graduationId = getGradId(r.Field<decimal?>("GRADUATION_ID").ToString()),
                                graduationIds = r.Field<string>("GRADUATION_ID_LIST")
                                    .Split(':')
                                    .Select(gi => getGradId(gi))
                                    .Where(gi => gi.HasValue)
                                    .Select(gi => gi.Value)
                                    .ToArray()
                            })
                    })
                .ToList();

            noms["schools"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["schools"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateDirections(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("directions");
            var nameAlts = new Dictionary<string, string>()
            {
                { "F", "Ekipaji" },
                { "T", "OVD"},
                { "G", "TO(AML)"},
                { "M", "TO(SUVD)"},
                { "C", "VS"},
                { "O", "Organizacii"},
            };
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_DIRECTION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("ID"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = nameAlts[r.Field<string>("ID")],
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["directions"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["directions"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateDocumentTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("documentTypes");
            string[] codes = {"3","4","5","115"};
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_DOCUMENT_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                direction = noms["directions"].ByOldId(r.Field<string>("ID_DIRECTION")).NomValueId(),
                                staffAlias =
                                    noms["staffTypes"]
                                    .ByCodeOrDefault(
                                        noms["directions"]
                                        .ByOldId(r.Field<string>("ID_DIRECTION"))
                                        .Code())
                                    .Alias(),
                                isPersonsOnly = r.Field<string>("PERSON_ONLY") == "Y" ? true : false,
                                isIdDocument = codes.Contains(r.Field<string>("CODE")) ? true : false
                            })
                    })
                .ToList();

            noms["documentTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["documentTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateDocumentRoles(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("documentRoles");
            var categoryAliases = new Dictionary<string, string>()
            {
                { "T", "check" },
                { "O", "training"},
                { "A", "other"},
                { "P", "docId"},
                { "F", "flying"},
                { "E", "graduation"},
            };
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_DOCUMENT_ROLE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                direction = noms["directions"].ByOldId(r.Field<string>("ID_DIRECTION")).NomValueId(),
                                staffAlias =
                                    noms["staffTypes"]
                                    .ByCodeOrDefault(
                                        noms["directions"]
                                        .ByOldId(r.Field<string>("ID_DIRECTION"))
                                        .Code())
                                    .Alias(),
                                isPersonsOnly = r.Field<string>("PERSON_ONLY") == "Y" ? true : false,
                                categoryCode = r.Field<string>("CATEGORY_CODE"),
                                categoryAlias = categoryAliases[r.Field<string>("CATEGORY_CODE")]
                            })
                    })
                .ToList();

            noms["documentRoles"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["documentRoles"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migratePersonStatusTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("personStatusTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'STATE_REASON'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["personStatusTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["personStatusTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateOtherDocPublishers(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("otherDocPublishers");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_OTHER_PUBLISHERS")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = null,
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["otherDocPublishers"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["otherDocPublishers"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateMedDocPublishers(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("medDocPublishers");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_MED_PUBLISHERS")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = null,
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["medDocPublishers"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["medDocPublishers"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateRatingTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("ratingTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_RATING_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = null,
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                codeCA = r.Field<string>("CA_CODE"),
                                dateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                                dateValidTo = r.Field<DateTime?>("DATE_TO")
                            })
                    })
                .ToList();

            noms["ratingTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["ratingTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateRatingClassGroups(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("ratingClassGroups");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_RATING_CLASS_GROUP")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["staffTypes"].ByOldId(r.Field<decimal>("STAFF_TYPE_ID").ToString()).NomValueId(),
                        TextContent = null
                    })
                .ToList();

            noms["ratingClassGroups"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["ratingClassGroups"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateRatingClasses(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("ratingClasses");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_RATING_CLASS")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["ratingClassGroups"].ByOldId(r.Field<decimal>("GROUP_ID").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                codeCA = r.Field<string>("CA_CODE"),
                                dateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                                dateValidTo = r.Field<DateTime?>("DATE_TO")
                            })
                    })
                .ToList();

            noms["ratingClasses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["ratingClasses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateRatingSubClasses(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("ratingSubClasses");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SUBCLASS_ATSM")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("DESCRIPTION"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = noms["ratingClasses"].ByOldId(r.Field<decimal>("CLASS_ID").ToString()).NomValueId(),
                        TextContent = null
                    })
                .ToList();

            noms["ratingSubClasses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["ratingSubClasses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuthorizationGroups(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("authorizationGroups");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AUTHORIZATION_GROUP")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                ratingClassGroupId = noms["ratingClassGroups"].ByOldId(r.Field<decimal?>("CLASS_GROUP_ID").ToString()).NomValueId()
                            })
                    })
                .ToList();

            noms["authorizationGroups"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["authorizationGroups"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuthorizations(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("authorizations");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AUTHORIZATION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["authorizationGroups"].ByOldId(r.Field<decimal?>("GROUP_ID").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                codeCA = r.Field<string>("CA_CODE"),
                                dateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                                dateValidTo = r.Field<DateTime?>("DATE_TO")
                            })
                    })
                .ToList();

            noms["authorizations"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["authorizations"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLicenceTypeDictionary(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("licenceTypeDictionary");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.LICENCE_DICTIONARY")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()).NomValueId(),
                        TextContent = null
                    })
                .ToList();

            noms["licenceTypeDictionary"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["licenceTypeDictionary"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLicenceTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("licenceTypes");
            var aliases = new Dictionary<string, string>()
            {
                { "1", "flightCrew" },
                { "2", "ovd"},
                { "4", "to_vs"},
                { "5", "to_suvd"}
            };

            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_LICENCE_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                codeCA = r.Field<string>("CA_CODE"),
                                dateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                                dateValidTo = r.Field<DateTime?>("DATE_TO"),
                                seqNo = r.Field<decimal?>("SEQ_NO"),
                                prtMaxRatingCount = r.Field<decimal?>("PRT_MAX_RATING_COUNT"),
                                prtMaxMedCertCount = r.Field<decimal?>("PRT_MAX_MED_CERT_COUNT"),
                                licenceTypeDictionaryId = noms["licenceTypeDictionary"].ByOldId(r.Field<decimal?>("LICENCE_DICTIONARY_ID").ToString()).NomValueId(),
                                licenceTypeDictionary1Id = noms["licenceTypeDictionary"].ByOldId(r.Field<decimal?>("LICENCE_DICTIONARY1_ID").ToString()).NomValueId(),
                                licenceTypeDictionary2Id = noms["licenceTypeDictionary"].ByOldId(r.Field<decimal?>("LICENCE_DICTIONARY2_ID").ToString()).NomValueId(),
                                licenceCode = r.Field<string>("LICENCE_CODE"),
                                prtPrintableDocId = r.Field<decimal?>("PRT_PRINTABLE_DOCUMENT_ID"),
                                qlfCode = r.Field<string>("QLF_CODE"),
                                staffTypeAlias = aliases[r.Field<object>("STAFF_TYPE_ID").ToString()]
                            })
                    })
                .ToList();

            noms["licenceTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["licenceTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLocationIndicators(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("locationIndicators");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_INDICATOR")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("POSITION"),
                        NameAlt = r.Field<string>("POSITION_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                type = r.Field<string>("FLAG_YN"),
                            })
                    })
                .ToList();

            noms["locationIndicators"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["locationIndicators"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftTCHolders(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftTCHolders");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_TC_HOLDER")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = null,
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftTCHolders"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftTCHolders"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                description = r.Field<string>("DESCRIPTION"),
                            })
                    })
                .ToList();

            noms["aircraftTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftTypeGroups(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftTypeGroups");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_GROUP")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = noms["aircraftTypes"].ByOldId(r.Field<long?>("ID_AC_TYPE").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                aircraftTCHolderId = noms["aircraftTCHolders"].ByOldId(r.Field<long?>("ID_TC_HOLD").ToString()).NomValueId()
                            })
                    })
                .ToList();

            noms["aircraftTypeGroups"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftTypeGroups"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftGroup66(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftGroup66");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_66")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftGroup66"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftGroup66"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftClases66(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftClases66");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_CATEGORY_66")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("DESCRIPTION"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = noms["aircraftGroup66"].ByOldId(r.Field<long?>("ID_AC_66").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                alias = r.Field<string>("CATEGORY"),
                            })
                    })
                .ToList();

            noms["aircraftClases66"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftClases66"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLimitations66(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("limitations66");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_LIMITATIONS_66")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                general = r.Field<string>("GENERAL"),
                            })
                    })
                .ToList();

            noms["limitations66"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["limitations66"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateMedClasses(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("medClasses");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_MED_CLASS")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["medClasses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["medClasses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateMedLimitation(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("medLimitation");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_MED_LIMITATION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["medLimitation"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["medLimitation"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migratePersonExperienceRoles(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("experienceRoles");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_EXPERIENCE_ROLE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                codeCA = r.Field<string>("CA_CODE"),
                            })
                    })
                .ToList();

            noms["experienceRoles"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["experienceRoles"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migratePersonExperienceMeasures(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("experienceMeasures");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_EXPERIENCE_MEASURE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                codeCA = r.Field<string>("CA_CODE"),
                            })
                    })
                .ToList();

            noms["experienceMeasures"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["experienceMeasures"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateCaa(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("caa");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.CAA")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                heading = r.Field<string>("HEADING"),
                                headingAlt = r.Field<string>("HEADING_TRANS"),
                                subHeading = r.Field<string>("SUBHEADING"),
                                subHeadingAlt = r.Field<string>("SUBHEADING_TRANS"),
                            })
                    })
                .ToList();

            noms["caa"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["caa"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migratePersonRatingLevels(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("personRatingLevels");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'ATSML_STEPEN'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["personRatingLevels"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["personRatingLevels"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLicenceActions(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("licenceActions");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_LICENCE_ACTION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["licenceActions"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["licenceActions"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateOrganizationTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("organizationTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_FIRM_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["organizationTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["organizationTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateOrganizationKinds(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("organizationKinds");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'TYPE_ORG'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["organizationKinds"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["organizationKinds"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateApplicationTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("applicationTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_REQUEST_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                dateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                                dateValidTo = r.Field<DateTime?>("DATE_TO"),
                                duration = r.Field<decimal?>("TIME_LIMIT"),
                                direction = r.Field<string>("DIRECTION_ID"),
                                licenceTypeIds = (r.Field<string>("LICENCE_TYPES") != null) ? r.Field<string>("LICENCE_TYPES").Split(':').Select(gi => int.Parse(gi)).ToArray() : null
                            })
                    })
                .ToList();

            noms["applicationTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["applicationTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateApplicationPaymentTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("applicationPaymentTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_PAYMENT_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                dateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                                dateValidTo = r.Field<DateTime?>("DATE_TO")
                            })
                    })
                .ToList();

            noms["applicationPaymentTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["applicationPaymentTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateCurrencies(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("currencies");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_CURRENCY")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["currencies"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["currencies"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateApprovalTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("approvalTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'APPROVAL_TYPE'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["approvalTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["approvalTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateApprovalStates(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("approvalStates");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'APPROVAL_STATE'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["approvalStates"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["approvalStates"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLim147classes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("lim147classes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_147_CLASS")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("SORT_ORDER"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["lim147classes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["lim147classes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLim147ratings(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("lim147ratings");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_147_RATING")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("SORT_ORDER"),
                        Name = r.Field<string>("RATING"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["lim147classes"].ByOldId(r.Field<long?>("ID_147_CLASS").ToString()).NomValueId(),
                        TextContent = null
                    })
                .ToList();

            noms["lim147ratings"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["lim147ratings"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLim147limitations(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("lim147limitations");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_147_LIMITATION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = noms["lim147ratings"].ByOldId(r.Field<long?>("ID_147_RATING").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                SortOrder = r.Field<string>("SORT_ORDER"),
                            })
                    })
                .ToList();

            noms["lim147limitations"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["lim147limitations"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLim145classes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("lim145classes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_MF145_CLASS")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("SORT_ORDER"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["lim145classes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["lim145classes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateLim145limitations(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("lim145limitations");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_MF145_LIMITATION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = noms["lim145classes"].ByOldId(r.Field<long?>("ID_MF145_CLASS").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                SortOrder = r.Field<string>("SORT_ORDER"),
                            })
                    })
                .ToList();

            noms["lim145limitations"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["lim145limitations"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuditParts(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("auditParts");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_PART")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                description = r.Field<string>("DESCRIPTION"),
                            })
                    })
                .ToList();

            noms["auditParts"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditParts"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuditPartRequirmants(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("auditPartRequirements");
            var idPartAliases = new Dictionary<string, string>()
            {
                { "1", "145"},
                { "2", "MF"},
                { "3", "MG"},
                { "4","147"},
                { "21", "aircrafts"},
                { "22", "TR"},
                { "42", "ACAM"}
            };

            for(int i = 200; i <= 211; i++)
            {
                idPartAliases.Add(i.ToString(), "ACAM");
            }

            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_REQUIREMENT")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("SUBJECT"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["auditParts"].ByOldId(r.Field<long?>("ID_PART").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                idPart = r.Field<long?>("ID_PART") != null ? idPartAliases[r.Field<long>("ID_PART").ToString()] : "",
                                sortOrder = r.Field<decimal?>("SORT_ORDER").ToString()
                            })
                    })
                .ToList();

            noms["auditPartRequirements"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditPartRequirements"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuditPartSections(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("auditPartSections");
            var idPartAliases = new Dictionary<string, string>()
            {
                { "1", "145"},
                { "2", "MF"},
                { "3", "MG"},
                { "4", "147"}
            };

            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SECTION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["auditParts"].ByOldId(r.Field<long?>("ID_PART").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                idPart = r.Field<long?>("ID_PART") != null ? idPartAliases[r.Field<long>("ID_PART").ToString()] : "",
                                sortOrder = r.Field<short?>("SORT_ORDER").ToString()
                            })
                    })
                .ToList();

            noms["auditPartSections"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditPartSections"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuditPartSectionDetails(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("auditPartSectionDetails");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SECTION_DETAIL")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["auditPartSections"].ByOldId(r.Field<long?>("ID_SECTION").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                sortOrder = r.Field<decimal?>("SORT_ORDER").ToString(),
                            })
                    })
                .ToList();

            noms["auditPartSectionDetails"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditPartSectionDetails"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuditReasons(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("auditReasons");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'AUDIT_REASON'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["auditReasons"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditReasons"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuditTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("auditTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'AUDIT_MODE'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["auditTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuditStatuses(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("auditStatuses");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'AUDIT_STATE'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["auditStatuses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditStatuses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAuditResults(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("auditResults");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'CC_RESULT'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["auditResults"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditResults"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftCategories(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftCategories");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_CATEGORY")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftCategories"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftCategories"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftProducers(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftProducers");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_MANUFACTURER")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["countries"].ByOldId(r.Field<decimal?>("ID_COUNTRY").ToString()).NomValueId(),
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                address = r.Field<string>("ADDRESS"),
                                addressAlt = r.Field<string>("ADDRESS_TRANS"),
                                website = r.Field<string>("WEB_SITE"),
                                email = r.Field<string>("E_MAIL"),
                                makeAircraft = r.Field<string>("MAKE_AC") == "Y" ? true : false,
                                makeEngine = r.Field<string>("MAKE_ENG") == "Y" ? true : false,
                                makePropeller = r.Field<string>("MAKE_PROP") == "Y" ? true : false,
                                makeRadio = r.Field<string>("MAKE_RAD") == "Y" ? true : false,
                            })
                    })
                .ToList();

            noms["aircraftProducers"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftProducers"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftSCodeTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftSCodeTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_STYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                scodeGroup = r.Field<string>("S_CODE_GROUP"),
                                scodeStart = r.Field<string>("S_CODE_START"),
                                scodeEnd = r.Field<string>("S_CODE_END"),
                                scodeLastUsed = r.Field<string>("S_CODE_LAST_USED")
                            })
                    })
                .ToList();

            noms["aircraftSCodeTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftSCodeTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftRelations(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftRelations");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_RELATION")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftRelations"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftRelations"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftParts(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftParts");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_PART")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftParts"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftParts"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftPartStatuses(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftPartStatuses");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'NEW_USED'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftPartStatuses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftPartStatuses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftDebtTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftDebtTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'REC_TYPE'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftDebtTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftDebtTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftOccurrenceClasses(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftOccurrenceClasses");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'INCIDENT_CLASS'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftOccurrenceClasses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftOccurrenceClasses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftCertificateTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftCertificateTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'CERTIFICATE_TYPE'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftCertificateTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftCertificateTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftOperTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftOperTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_OPER")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftOperTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftOperTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftTypeCertificateTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftTypeCertificateTypes");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'TC_TYPE'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftTypeCertificateTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftTypeCertificateTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftRemovalReasons(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftRemovalReasons");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_SHORT_LISTS where domain = 'REMOVAL_REASON'")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("MEANING"),
                        NameAlt = r.Field<string>("MEANING_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftRemovalReasons"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftRemovalReasons"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }


        public static void migratePersonCheckRatingValues(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("personCheckRatingValues");

            var nomInfo = new Dictionary<string, Tuple<string,string>>()
            {
                { "G", new Tuple<string,string>("Добро"         , "Good"         )},
                { "S", new Tuple<string,string>("Задоволително" , "Satisfactory" )},
                { "I", new Tuple<string,string>("Недостатъчно"  , "Insufficient" )},
                { "U", new Tuple<string,string>("Неприемливо"   , "Unacceptable" )},
                { "К", new Tuple<string,string>("Компетентен"   , "Competent"    )},
                { "Н", new Tuple<string,string>("Некомпетентен" , "Incompetent"  )},

                //TODO
                { "C", new Tuple<string,string>("C"             , ""             )}
            };

            int OldId = 1;
            noms["personCheckRatingValues"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                    {
                        OldId = OldId.ToString(),
                        Code = ni.Key,
                        Name = ni.Value.Item1,
                        NameAlt = ni.Value.Item2,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    };
                noms["personCheckRatingValues"][OldId.ToString()] = row;
                nom.NomValues.Add(row);
                OldId++;
            }
        }

        public static void migratePersonRatingModels(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("personRatingModels");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "P", new Tuple<string,string>("Постоянно"         , "Permanently"         )},
                { "T", new Tuple<string,string>("Временно"          , "Temporarily"         )}
            };
            int OldId = 1;
            noms["personRatingModels"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = OldId.ToString(),
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContent = null
                };
                noms["personRatingModels"][OldId.ToString()] = row;
                nom.NomValues.Add(row);
                OldId++;
            }
        }

        public static void migrateAirportTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("airportTypes");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "IA", new Tuple<string,string>("Международно летище" , "International airport" )},
                { "MA", new Tuple<string,string>("Военно летище"       , "Military airport"      )},
                { "AF", new Tuple<string,string>("Летателна площадка"  , "Airfield"              )}
            };
            int OldId = 1;
            noms["airportTypes"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = OldId.ToString(),
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContent = null
                };
                noms["airportTypes"][OldId.ToString()] = row;
                nom.NomValues.Add(row);
                OldId++;
            }
        }

        public static void migrateAirportRelations(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("airportRelations");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "OW", new Tuple<string,string>("Собственик"       , "Owner"    )},
                { "TN", new Tuple<string,string>("Наемател"         , "Tenant"   )},
                { "OP", new Tuple<string,string>("Летищен оператор" , "Operator" )}
            };
            int OldId = 1;
            noms["airportRelations"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = OldId.ToString(),
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContent = null
                };
                noms["airportRelations"][OldId.ToString()] = row;
                nom.NomValues.Add(row);
                OldId++;
            }
        }

        public static void migrateAircraftRadiotypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftRadiotypes");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "TRM" , new Tuple<string,string>("Transmitters"    , "Transmitters"    )},
                { "ELT" , new Tuple<string,string>("E L T"           , "E L T"           )},
                { "TRS" , new Tuple<string,string>("Transponders"    , "Transponders"    )},
                { "WR"  , new Tuple<string,string>("Weather radar"   , "Weather radar"   )},
                { "TCAS", new Tuple<string,string>("TCAS"            , "TCAS"            )},
                { "DME" , new Tuple<string,string>("DME"             , "DME"             )},
                { "RA"  , new Tuple<string,string>("Radio Altimeter" , "Radio Altimeter" )}

            };
            int OldId = 1;
            noms["aircraftRadiotypes"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = OldId.ToString(),
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContent = null
                };
                noms["aircraftRadiotypes"][OldId.ToString()] = row;
                nom.NomValues.Add(row);
                OldId++;
            }
        }

        public static void migrateAirportOperatorActivityTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("airportoperatorActivityTypes");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "OPINT" , new Tuple<string,string>("Летищен оператор на гражданско летище за обществено ползване за обслужване на международни превози" , ""    )},
                { "OPDOM" , new Tuple<string,string>("Летищен оператор на гражданско летище за обществено ползване за обслужване на вътрешни превози"     , ""    )},
                { "OPOTH" , new Tuple<string,string>("Летищен оператор на гражданско летище за дейности, различни от вътрешни и международни превози"     , ""    )}

            };
            int OldId = 1;
            noms["airportoperatorActivityTypes"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = OldId.ToString(),
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContent = null
                };
                noms["airportoperatorActivityTypes"][OldId.ToString()] = row;
                nom.NomValues.Add(row);
                OldId++;
            }
        }

        public static void migrateGroundServiceOperatorActivityTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("groundServiceOperatorActivityTypes");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "GAS"   , new Tuple<string,string>("Наземно администриране и надзор"                           , ""    )},
                { "CS"    , new Tuple<string,string>("Обслужване на пътници"                                     , ""    )},
                { "BP"    , new Tuple<string,string>("Обработка на багажи"                                       , ""    )},
                { "CMP"   , new Tuple<string,string>("Обработка на товари и поща"                                , ""    )},
                { "ACPS"  , new Tuple<string,string>("Перонно обслужване на въздухоплавателни средства"          , ""    )},
                { "ACS"   , new Tuple<string,string>("Обслужване на въздухоплавателни средства"                  , ""    )},
                { "ACSFO" , new Tuple<string,string>("Обслужване на въздухоплавателни средства с горива и масла" , ""    )},
                { "ACTS"  , new Tuple<string,string>("Техническо обслужване на въздухоплавателни средства"       , ""    )},
                { "FOCA"  , new Tuple<string,string>("Полетни операции и администриране на екипажите"            , ""    )},
                { "GT"    , new Tuple<string,string>("Наземен транспорт"                                         , ""    )},
                { "BBS"   , new Tuple<string,string>("Обслужване на бордния бюфет"                               , ""    )}

            };
            int OldId = 1;
            noms["groundServiceOperatorActivityTypes"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = OldId.ToString(),
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContent = null
                };
                noms["groundServiceOperatorActivityTypes"][OldId.ToString()] = row;
                nom.NomValues.Add(row);
                OldId++;
            }
        }

        public static void migrateEngLangLevels(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("engLangLevels");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_ENG_LANG")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("LANG_LEVEL"),
                        NameAlt = r.Field<string>("LANG_LEVEL_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = JsonConvert.SerializeObject(
                            new
                            {
                                seqNumber = r.Field<decimal?>("SEQ_NUMBER").ToString(),
                            })
                    })
                .ToList();

            noms["engLangLevels"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["engLangLevels"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        //TODO
        public static void migrateAircraftProducersFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("aircraftProducersFm");
            var results = conn.CreateStoreCommand(@"SELECT * FROM Makers")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("nMakerID").ToString(),
                        Code = null,
                        Name = r.Field<string>("tNameBG"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftProducersFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftProducersFm"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftLimitationsFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("aircraftLimitationsFm");
            var results = conn.CreateStoreCommand(@"
                SELECT [Code] code, [Limitation BG] name, [Limitation EN] nameAlt
                FROM [GvaAircraft].[dbo].[LimitAW]")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<string>("code"),
                        Code = r.Field<string>("code"),
                        Name = r.Field<string>("name"),
                        NameAlt = r.Field<string>("nameAlt"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftLimitationsFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftLimitationsFm"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        public static void migrateAircraftRegStatsesFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("aircraftRegStatsesFm");
            var results = conn.CreateStoreCommand(@"
                SELECT [Code] code, [Registration Status] name
                FROM [GvaAircraft].[dbo].[RegStatus]")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<string>("code"),
                        Code = r.Field<string>("code"),
                        Name = r.Field<string>("name"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContent = null
                    })
                .ToList();

            noms["aircraftRegStatsesFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftRegStatsesFm"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }
    }
}
