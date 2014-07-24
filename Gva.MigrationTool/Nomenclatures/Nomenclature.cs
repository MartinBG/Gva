﻿using Common.Api.Models;
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
using Autofac.Features.OwnedInstances;
using Common.Tests;
using Gva.Api.Repositories.CaseTypeRepository;

namespace Gva.MigrationTool.Nomenclatures
{
    public static class NomenclatureExtensions
    {
        public static NomValue ByCodeOrDefault(this Dictionary<string, NomValue> nomValues, string code)
        {
            return nomValues.Where(v => v.Value.Code == code).Select(v => v.Value).SingleOrDefault();
        }

        public static NomValue ByCode(this Dictionary<string, NomValue> nomValues, string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                return null;
            }

            return nomValues.Where(v => v.Value.Code == code).Select(v => v.Value).Single();
        }

        public static NomValue ByAlias(this Dictionary<string, NomValue> nomValues, string alias)
        {
            if (String.IsNullOrEmpty(alias))
            {
                return null;
            }

            return nomValues.Where(v => v.Value.Alias == alias).Select(v => v.Value).Single();
        }

        public static NomValue ByName(this Dictionary<string, NomValue> nomValues, string name)
        {
            if (String.IsNullOrEmpty(name))
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

        public static string Name(this NomValue nomValue)
        {
            return nomValue != null ? nomValue.Name : null;
        }
    }

    public class Nomenclature
    {
        private Func<Owned<DisposableTuple<IUnitOfWork, INomRepository, ICaseTypeRepository>>> dependencyFactory;
        private OracleConnection oracleConn;
        private SqlConnection sqlConn;

        public Nomenclature(
            OracleConnection oracleConn,
            SqlConnection sqlConn,
            Func<Owned<DisposableTuple<IUnitOfWork, INomRepository, ICaseTypeRepository>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
            this.sqlConn = sqlConn;
        }

        private Dictionary<string, Dictionary<string, NomValue>> noms;

        public Dictionary<string, Dictionary<string, NomValue>> migrateNomenclatures()
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            noms = new Dictionary<string, Dictionary<string, NomValue>>();

            using (var dependencies = dependencyFactory())
            {
                noms["boolean"] = dependencies.Value.Item2.GetNomValues("boolean").ToDictionary(n => Guid.NewGuid().ToString());
            }

            using (var dependencies = dependencyFactory())
            {
                noms["registers"] = dependencies.Value.Item2.GetNomValues("registers").ToDictionary(n => Guid.NewGuid().ToString());
            }

            using (var dependencies = dependencyFactory())
            {
                noms["linTypes"] = dependencies.Value.Item2.GetNomValues("linTypes").ToDictionary(n => Guid.NewGuid().ToString());
            }

            using (var dependencies = dependencyFactory())
            {
                noms["gvaCaseTypes"] = dependencies.Value.Item2.GetNomValues("linTypes").ToDictionary(n => Guid.NewGuid().ToString());
            }

            using (var dependencies = dependencyFactory())
            {
                noms["testScores"] = dependencies.Value.Item2.GetNomValues("testScores").ToDictionary(n => Guid.NewGuid().ToString());
            }

            using (var dependencies = dependencyFactory())
            {
                noms["personCaseTypes"] = dependencies.Value.Item3.GetCaseTypesForSet("Person").ToDictionary(ct => Guid.NewGuid().ToString(), ct =>
                    new NomValue
                    {
                        NomValueId = ct.GvaCaseTypeId,
                        Name = ct.Name,
                        Alias = ct.Alias
                    });
            }

            using (var dependencies = dependencyFactory())
            {
                noms["airworthinessCertificateTypes"] = dependencies.Value.Item2.GetNomValues("airworthinessCertificateTypes").ToDictionary(n => Guid.NewGuid().ToString());
            }

            using (var dependencies = dependencyFactory())
            {
                noms["airworthinessReviewTypes"] = dependencies.Value.Item2.GetNomValues("airworthinessReviewTypes").ToDictionary(n => Guid.NewGuid().ToString());
            }

            using (var dependencies = dependencyFactory())
            {
                migrateGenders(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateCountires(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateCities(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAddressTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateStaffTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateEmploymentCategories(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateGraduations(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateSchools(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateDirections(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateDocumentTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateDocumentRoles(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migratePersonStatusTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateOtherDocPublishers(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateMedDocPublishers(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateRatingTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateRatingClassGroups(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateRatingClasses(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateRatingSubClasses(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuthorizationGroups(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuthorizations(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLicenceTypeDictionary(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLicenceTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLocationIndicators(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftTCHolders(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftTypeGroups(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftGroup66(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftClases66(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLimitations66(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateMedClasses(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateMedLimitation(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migratePersonExperienceRoles(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migratePersonExperienceMeasures(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateCaa(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migratePersonRatingLevels(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLicenceActions(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateOrganizationTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateOrganizationKinds(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateApplicationTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateApplicationPaymentTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateCurrencies(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateApprovalTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateApprovalStates(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLim147classes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLim147ratings(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLim147limitations(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLim145classes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateLim145limitations(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuditParts(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuditPartRequirmants(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuditPartSections(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuditPartSectionDetails(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuditReasons(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuditTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuditStatuses(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAuditResults(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftCategories(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftProducers(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftSCodeTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftRelations(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftParts(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftPartStatuses(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftDebtTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftOccurrenceClasses(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftCertificateTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftOperTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftTypeCertificateTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftRemovalReasons(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAirportTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAirportRelations(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftRadiotypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAirportOperatorActivityTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateGroundServiceOperatorActivityTypes(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migratePersonCheckRatingValues(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migratePersonRatingModels(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateEngLangLevels(dependencies.Value.Item2, oracleConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateCountriesFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateCofATypesFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateEASATypesFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateEASACategoriesFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateEURegTypesFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftDebtTypesFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftCreditorsFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftProducersFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftLimitationsFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftRegStatsesFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            using (var dependencies = dependencyFactory())
            {
                migrateAircraftCatAWsFm(dependencies.Value.Item2, sqlConn);
                dependencies.Value.Item1.Save();
            }

            timer.Stop();
            Console.WriteLine("Nomenclatures migration time - {0}", timer.Elapsed.TotalMinutes);

            return noms;
        }

        private void migrateGenders(INomRepository repo, OracleConnection conn)
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
                        Alias = r.Field<string>("NAME_TRANS"),
                        TextContentString = null,
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

        private void migrateCountires(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateCities(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAddressTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateStaffTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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
                    TextContentString = v.TextContentString,
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

        private void migrateEmploymentCategories(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateGraduations(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateSchools(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateDirections(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["directions"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["directions"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateDocumentTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
                            new
                            {
                                direction = noms["directions"].ByOldId(r.Field<string>("ID_DIRECTION")).NomValueId(),
                                staffTypeAlias =
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

        private void migrateDocumentRoles(INomRepository repo, OracleConnection conn)
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
                        Alias = r.Field<string>("CODE") == "ENG" ? "engTraining" : null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = null,
                        TextContentString = JsonConvert.SerializeObject(
                            new
                            {
                                direction = noms["directions"].ByOldId(r.Field<string>("ID_DIRECTION")).NomValueId(),
                                staffTypeAlias =
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

        private void migratePersonStatusTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["personStatusTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["personStatusTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateOtherDocPublishers(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["otherDocPublishers"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["otherDocPublishers"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateMedDocPublishers(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["medDocPublishers"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["medDocPublishers"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateRatingTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateRatingClassGroups(INomRepository repo, OracleConnection conn)
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
                        ParentValueId = null,
                        TextContentString = JsonConvert.SerializeObject(
                            new
                            {
                                staffTypeAlias = noms["staffTypes"].ByOldId(r.Field<decimal>("STAFF_TYPE_ID").ToString()).Alias
                            })
                    })
                .ToList();

            noms["ratingClassGroups"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["ratingClassGroups"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateRatingClasses(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateRatingSubClasses(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["ratingSubClasses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["ratingSubClasses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAuthorizationGroups(INomRepository repo, OracleConnection conn)
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
                        ParentValueId = null,
                        TextContentString = JsonConvert.SerializeObject(
                            new
                            {
                                staffTypeAlias = noms["staffTypes"].ByOldId(r.Field<decimal>("STAFF_TYPE_ID").ToString()).Alias,
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

        private void migrateAuthorizations(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateLicenceTypeDictionary(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["licenceTypeDictionary"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["licenceTypeDictionary"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateLicenceTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("licenceTypes");

            var templateNames = new Dictionary<string, string>()
            {
                { "103", "flight_licence" },
                { "161", "foreign_licence" },
                { "181", "caa_steward" },
                { "242", "student_flight_licence" },
                { "261", "coordinator" },
                { "262", "flight" },
                { "281", "atcl1" },
                { "289", "convoy" },
                { "302", "AML_national" },
                { "321", "coordinator_simi" },
                { "323", "rvd_licence" },
                { "324", "AML_III" },
                { "327", "Pilot142" },
                { "328", "Pilot142_2013" },
                { "329", "CAL03_2013" },
                { "361", "pilot" },
                { "601", "AML" }
            };

            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_LICENCE_TYPE")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        NameAlt = r.Field<string>("NAME_TRANS"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()).NomValueId(),
                        TextContentString = JsonConvert.SerializeObject(
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
                                templateName = templateNames[r.Field<object>("PRT_PRINTABLE_DOCUMENT_ID").ToString()],
                                staffTypeAlias = noms["staffTypes"].ByOldId(r.Field<int>("STAFF_TYPE_ID").ToString()).Alias
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

        private void migrateLocationIndicators(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAircraftTCHolders(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftTCHolders"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftTCHolders"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAircraftTypeGroups(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("aircraftTypeGroups");
            var results = conn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.NM_AC_GROUP")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<object>("ID").ToString(),
                        Code = r.Field<string>("CODE"),
                        Name = r.Field<string>("NAME"),
                        Alias = null,
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = noms["aircraftTypes"].ByOldId(r.Field<long?>("ID_AC_TYPE").ToString()).NomValueId(),
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAircraftGroup66(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftGroup66"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftGroup66"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftClases66(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
                            new
                            {
                                alias = r.Field<string>("CATEGORY")
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

        private void migrateLimitations66(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateMedClasses(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["medClasses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["medClasses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateMedLimitation(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["medLimitation"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["medLimitation"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migratePersonExperienceRoles(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migratePersonExperienceMeasures(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateCaa(INomRepository repo, OracleConnection conn)
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
                        Alias = r.Field<string>("CODE"),
                        IsActive = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        ParentValueId = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()).NomValueId(),
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migratePersonRatingLevels(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["personRatingLevels"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["personRatingLevels"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateLicenceActions(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["licenceActions"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["licenceActions"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateOrganizationTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["organizationTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["organizationTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateOrganizationKinds(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["organizationKinds"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["organizationKinds"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateApplicationTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateApplicationPaymentTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateCurrencies(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["currencies"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["currencies"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateApprovalTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["approvalTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["approvalTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateApprovalStates(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["approvalStates"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["approvalStates"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateLim147classes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["lim147classes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["lim147classes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateLim147ratings(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["lim147ratings"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["lim147ratings"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateLim147limitations(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateLim145classes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["lim145classes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["lim145classes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateLim145limitations(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAuditParts(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAuditPartRequirmants(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAuditPartSections(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAuditPartSectionDetails(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAuditReasons(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["auditReasons"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditReasons"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAuditTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["auditTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAuditStatuses(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["auditStatuses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditStatuses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAuditResults(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["auditResults"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["auditResults"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftCategories(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            results.Add(new NomValue() {
                OldId = "11",
                Code = "VP",
                Name = "Мотопарапланер",
                NameAlt = "Paramotor-Trike",
                Alias = null,
                IsActive = true,
                ParentValueId = null,
                TextContentString = null
            });

            results.Add(new NomValue() {
                OldId = "12",
                Code = "GR",
                Name = "Автожир",
                NameAlt = "Gyroplane",
                Alias = null,
                IsActive = true,
                ParentValueId = null,
                TextContentString = null
            });
            noms["aircraftCategories"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftCategories"][row.OldId] = row;
                nom.NomValues.Add(row);
            }


        }

        private void migrateAircraftProducers(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAircraftSCodeTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAircraftRelations(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftRelations"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftRelations"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftParts(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftParts"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftParts"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftPartStatuses(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftPartStatuses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftPartStatuses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftDebtTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftDebtTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftDebtTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftOccurrenceClasses(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftOccurrenceClasses"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftOccurrenceClasses"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftCertificateTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftCertificateTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftCertificateTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftOperTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftOperTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftOperTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftTypeCertificateTypes(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftTypeCertificateTypes"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftTypeCertificateTypes"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftRemovalReasons(INomRepository repo, OracleConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftRemovalReasons"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftRemovalReasons"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }


        private void migratePersonCheckRatingValues(INomRepository repo, OracleConnection conn)
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

            noms["personCheckRatingValues"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                    {
                        OldId = null,
                        Code = ni.Key,
                        Name = ni.Value.Item1,
                        NameAlt = ni.Value.Item2,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContentString = null
                    };
                noms["personCheckRatingValues"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migratePersonRatingModels(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("personRatingModels");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "P", new Tuple<string,string>("Постоянно"         , "Permanently"         )},
                { "T", new Tuple<string,string>("Временно"          , "Temporarily"         )}
            };

            noms["personRatingModels"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["personRatingModels"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAirportTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("airportTypes");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "IA", new Tuple<string,string>("Международно летище" , "International airport" )},
                { "MA", new Tuple<string,string>("Военно летище"       , "Military airport"      )},
                { "AF", new Tuple<string,string>("Летателна площадка"  , "Airfield"              )}
            };

            noms["airportTypes"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["airportTypes"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAirportRelations(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("airportRelations");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "OW", new Tuple<string,string>("Собственик"       , "Owner"    )},
                { "TN", new Tuple<string,string>("Наемател"         , "Tenant"   )},
                { "OP", new Tuple<string,string>("Летищен оператор" , "Operator" )}
            };

            noms["airportRelations"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["airportRelations"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftRadiotypes(INomRepository repo, OracleConnection conn)
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

            noms["aircraftRadiotypes"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["aircraftRadiotypes"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAirportOperatorActivityTypes(INomRepository repo, OracleConnection conn)
        {
            Nom nom = repo.GetNom("airportoperatorActivityTypes");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "OPINT" , new Tuple<string,string>("Летищен оператор на гражданско летище за обществено ползване за обслужване на международни превози" , ""    )},
                { "OPDOM" , new Tuple<string,string>("Летищен оператор на гражданско летище за обществено ползване за обслужване на вътрешни превози"     , ""    )},
                { "OPOTH" , new Tuple<string,string>("Летищен оператор на гражданско летище за дейности, различни от вътрешни и международни превози"     , ""    )}

            };

            noms["airportoperatorActivityTypes"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["airportoperatorActivityTypes"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateGroundServiceOperatorActivityTypes(INomRepository repo, OracleConnection conn)
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

            noms["groundServiceOperatorActivityTypes"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["groundServiceOperatorActivityTypes"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateEngLangLevels(INomRepository repo, OracleConnection conn)
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
                        TextContentString = JsonConvert.SerializeObject(
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

        private void migrateAircraftLimitationsFm(INomRepository repo, SqlConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftLimitationsFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftLimitationsFm"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftRegStatsesFm(INomRepository repo, SqlConnection conn)
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
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftRegStatsesFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftRegStatsesFm"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        //private void migrateAircraftCategoriesFm(INomRepository repo, SqlConnection conn)
        //{
        //    Nom nom = repo.GetNom("aircraftCategoriesFM");
        //    var results = conn.CreateStoreCommand(@"select * from CatAct")
        //        .Materialize(r =>
        //            new NomValue
        //            {
        //                OldId = r.Field<string>("Code"),
        //                Code = r.Field<string>("Code"),
        //                Name = r.Field<string>("Category BG"),
        //                NameAlt = r.Field<string>("Category EN"),
        //                Alias = null,
        //                IsActive = true,
        //                ParentValueId = null,
        //                TextContent = null
        //            })
        //        .ToList();

        //    noms["aircraftCategoriesFM"] = new Dictionary<string, NomValue>();
        //    foreach (var row in results)
        //    {
        //        noms["aircraftCategoriesFM"][row.OldId] = row;
        //        nom.NomValues.Add(row);
        //    }
        //}

        private void migrateCountriesFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("countriesFm");
            var results = conn.CreateStoreCommand(@"select * from Cnts")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<string>("nCntsRecNo"),
                        Code = r.Field<string>("tISO"),
                        Name = r.Field<string>("tNameFullBG"),
                        NameAlt = r.Field<string>("tNameFullEN"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContentString = JsonConvert.SerializeObject(
                            new
                            {
                                code3 = r.Field<string>("tISO3"),
                                heading = r.Field<string>("tNamePrintBG"),
                                headingAlt = r.Field<string>("tNamePrintEN"),
                                shortName = r.Field<string>("tName"),
                            })
                    })
                .ToList();

            noms["countriesFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["countriesFm"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftProducersFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("aircraftProducersFm");
            var results = conn.CreateStoreCommand(@"select * from Makers")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = r.Field<string>("nMakerID"),
                        Code = null,
                        Name = r.Field<string>("tNameBG"),
                        NameAlt = r.Field<string>("tNameEN"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = noms["countriesFm"].ByCode(r.Field<string>("tCntsISO")).NomValueId(),
                        TextContentString = JsonConvert.SerializeObject(
                            new
                            {
                                address = r.Field<string>("tAdrsStreetBG"),
                                addressCity = r.Field<string>("tAdrsCityBG"),
                                addressAlt = r.Field<string>("tAdrsStreetEN"),
                                addressCityAlt = r.Field<string>("tAdrsCityEN"),
                                website = r.Field<string>("t_WebSite"),
                                email = r.Field<string>("t_eMail"),
                                notes = r.Field<string>("tRemark"),
                                printName = r.Field<string>("t_PrintBG"),
                                printNameAlt = r.Field<string>("t_PrintEN"),
                            })
                    })
                .ToList();

            noms["aircraftProducersFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftProducersFm"][row.OldId] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateCofATypesFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("CofATypesFm");
            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "E25", new Tuple<string,string>("EASA 25"          , "EASA 25"         )},
                { "E24", new Tuple<string,string>("EASA 24"          , "EASA 24"         )},
                { "BGF", new Tuple<string,string>("BG Form"          , "BG Form"         )},
                { "TEC", new Tuple<string,string>("Tech Cert"        , "Tech Cert"       )},
                { "EXP", new Tuple<string,string>("EXP"              , "EXP"             )},
                { "EA" , new Tuple<string,string>("EASA"             , "EASA"            )},
                { "OBF", new Tuple<string,string>("Old BG Form"      , "Old BG Form"     )}//,
                //{ "TC" , new Tuple<string,string>("\"Tech Cert\""    , "\"Tech Cert\""   )},
                //{ "BF" , new Tuple<string,string>("\"BG Form\""      , "\"BG Form\""     )},
            };

            noms["CofATypesFm"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["CofATypesFm"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateEASATypesFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("EASATypesFm");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "BA"  , new Tuple<string,string>("Balloon"                                 , "Balloon"                                 )},
                { "CO"  , new Tuple<string,string>("Commuter"                                , "Commuter"                                )},
                { "EX"  , new Tuple<string,string>("Experimental"                            , "Experimental"                            )},
                { "GL"  , new Tuple<string,string>("Glider"                                  , "Glider"                                  )},
                { "GY"  , new Tuple<string,string>("Gyroplane"                               , "Gyroplane"                               )},
                { "LA"  , new Tuple<string,string>("Large Aeroplane"                         , "Large Aeroplane"                         )},
                { "MH"  , new Tuple<string,string>("Motor-hanglider"                         , "Motor-hanglider"                         )},
                { "PT"  , new Tuple<string,string>("Paramotor-Trike"                         , "Paramotor-Trike"                         )},
                { "RO"  , new Tuple<string,string>("Rotorcraft"                              , "Rotorcraft"                              )},
                { "SA"  , new Tuple<string,string>("Small Aeroplane"                         , "Small Aeroplane"                         )},
                { "SR"  , new Tuple<string,string>("Small Rotorcraft"                        , "Small Rotorcraft"                        )},
                { "VLA" , new Tuple<string,string>("Very Light Aeroplane"                    , "Very Light Aeroplane"                    )},
                { "VLR" , new Tuple<string,string>("Very Light Rotorcraft"                   , "Very Light Rotorcraft"                   )}
            };

            noms["EASATypesFm"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["EASATypesFm"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateEASACategoriesFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("EASACategoriesFm");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "AW"  , new Tuple<string,string>("Aerial Work"                   , "Aerial Work"                     )},
                { "COM" , new Tuple<string,string>("Commercial"                    , "Commercial"                      )},
                { "COR" , new Tuple<string,string>("Corporate"                     , "Corporate"                       )},
                { "PR"  , new Tuple<string,string>("Private"                       , "Private"                         )},
                { "VLA" , new Tuple<string,string>("VLA"                           , "VLA"                             )}
            };

            noms["EASACategoriesFm"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["EASACategoriesFm"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateEURegTypesFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("EURegTypesFm");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                { "EU"  , new Tuple<string,string>("EU"                             , "EU"                              )},
                { "EUR" , new Tuple<string,string>("EU - Restricted"                , "EU - Restricted"                 )},
                { "OD"  , new Tuple<string,string>("Old Doc"                        , "Old Doc"                         )},
                { "AN"  , new Tuple<string,string>("Annex II"                       , "Annex II"                        )},
                { "A2"  , new Tuple<string,string>("Article-2"                      , "Article-2"                       )},
                { "A1"  , new Tuple<string,string>("Article-1"                      , "Article-1"                       )},
                { "VLA" , new Tuple<string,string>("VLA"                            , "VLA"                             )},
                { "AM"  , new Tuple<string,string>("Amateur"                        , "Amateur"                         )},
                { "EXP" , new Tuple<string,string>("EXP"                            , "EXP"                             )},
                { "RU"  , new Tuple<string,string>("RU"                             , "RU"                              )},
            };

            noms["EURegTypesFm"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["EURegTypesFm"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftDebtTypesFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("aircraftDebtTypesFm");

            var nomInfo = new Dictionary<string, Tuple<string, string>>()
            {
                {"ANN" , new Tuple<string,string>("АНЕКС"                                  , "Annex"                                      )},
                {"ENT" , new Tuple<string,string>("ВЪЗЛОЖЕН"                               , "Entrusted"                                  )},
                {"EWD" , new Tuple<string,string>("ВЪЗЛОЖЕН и снети ЗАПОРИ"                , "Entrusted and waived distraints"            )},
                {"CCON", new Tuple<string,string>("ДОГОВОР ЗА ЦЕСИЯ"                       , "Cession contract"                           )},
                {"REM" , new Tuple<string,string>("ЗАБЕЛЕЖКА"                              , "Remark"                                     )},
                {"PEST", new Tuple<string,string>("ЗАЛОГ  - УЧРЕДЕН"                       , "Pledge - established"                       )},
                {"POBL", new Tuple<string,string>("ЗАЛОГ - ЗАЛИЧЕН"                        , "Pledge - obliterated"                       )},
                {"PTRA", new Tuple<string,string>("ЗАЛОГ - ПРЕХВЪРЛЕН"                     , "Pledge - transferred"                       )},
                {"PLEE", new Tuple<string,string>("ЗАЛОГ - УЧРЕДЕН"                        , "Pledge - established"                       )},
                {"PES" , new Tuple<string,string>("ЗАЛОГ - УЧРЕДЕН - ВТОРИ"                , "Pledge - established - second"              )},
                {"PMO" , new Tuple<string,string>("ЗАЛОГ и ИПОТЕКА - ЗАЛИЧЕНИ"             , "Pledge and mortgage - obliterated"          )},
                {"PSOB", new Tuple<string,string>("ЗАЛОЗИ - ЗАЛИЧЕНИ"                      , "Pledges - obliterated"                      )},
                {"DIMP", new Tuple<string,string>("ЗАПОР - НАЛОЖЕН"                        , "Distraint - imposed"                        )},
                {"DCAN", new Tuple<string,string>("ЗАПОР - ОТМЕНЕН"                        , "Distraint - cancelled"                      )},
                {"DSCA", new Tuple<string,string>("ЗАПОРИ - ОТМЕНЕНИ"                      , "Distraints - cancelled"                     )},
                {"DPOB", new Tuple<string,string>("Запори и залог - заличени"              , "Distraints and pledge - obliterated"        )},
                {"MEST", new Tuple<string,string>("ИПОТЕКА  - УЧРЕДЕНА"                    , "Mortgage - established"                     )},
                {"MLAP", new Tuple<string,string>("ИПОТЕКА - ПОГАСЕНА"                     , "Mortgage - lapsed"                          )},
                {"MOES", new Tuple<string,string>("ИПОТЕКА - УЧРЕДЕНА"                     , "Mortgage - established"                     )},
                {"LET" , new Tuple<string,string>("Писмо"                                  , "Letter"                                     )},
                {"DCOL", new Tuple<string,string>("Постановление за обезпечение"           , "Decree collateral"                          )}
            };

            noms["aircraftDebtTypesFm"] = new Dictionary<string, NomValue>();
            foreach (var ni in nomInfo)
            {
                NomValue row = new NomValue()
                {
                    OldId = null,
                    Code = ni.Key,
                    Name = ni.Value.Item1,
                    NameAlt = ni.Value.Item2,
                    Alias = null,
                    IsActive = true,
                    ParentValueId = null,
                    TextContentString = null
                };
                noms["aircraftDebtTypesFm"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftCreditorsFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("aircraftCreditorsFm");
            var results = conn.CreateStoreCommand(@"select distinct Creditor from Morts")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = null,
                        Code = null,
                        Name = r.Field<string>("Creditor"),
                        NameAlt = null,
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftCreditorsFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                noms["aircraftCreditorsFm"][Guid.NewGuid().ToString()] = row;
                nom.NomValues.Add(row);
            }
        }

        private void migrateAircraftCatAWsFm(INomRepository repo, SqlConnection conn)
        {
            Nom nom = repo.GetNom("aircraftCatAWsFm");
            var results = conn.CreateStoreCommand(@"SELECT [Category BG], [Category EN], Code FROM CatAW")
                .Materialize(r =>
                    new NomValue
                    {
                        OldId = null,
                        Code = r.Field<string>("Code"),
                        Name = r.Field<string>("Category BG"),
                        NameAlt = r.Field<string>("Category EN"),
                        Alias = null,
                        IsActive = true,
                        ParentValueId = null,
                        TextContentString = null
                    })
                .ToList();

            noms["aircraftCatAWsFm"] = new Dictionary<string, NomValue>();
            foreach (var row in results)
            {
                if (row.Code == "0" || row.Code == "BLANK")
                {
                    continue;
                }

                noms["aircraftCatAWsFm"][Guid.NewGuid().ToString()] = row; //no old id
                nom.NomValues.Add(row);
            }
        }
    }
}
