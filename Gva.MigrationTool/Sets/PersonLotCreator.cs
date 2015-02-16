using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.CommonUtils;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class PersonLotCreator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public PersonLotCreator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void StartCreating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            //intput
            ConcurrentQueue<int> personIds,
            //output
            ConcurrentDictionary<int, int> personIdToLotId,
            ConcurrentDictionary<string, int> personEgnToLotId,
            ConcurrentDictionary<int, JObject> personLotIdToPersonNom,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.oracleConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            int personId;
            while (personIds.TryDequeue(out personId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = this.dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var caseTypeRepository = dependencies.Value.Item3;
                        var lotEventDispatcher = dependencies.Value.Item4;
                        var context = dependencies.Value.Item5;

                        var personCaseTypes = this.getPersonCaseTypes(personId, noms);

                        var inspectorData = this.getInspectorData(personId, noms);
                        if (inspectorData != null)
                        {
                            personCaseTypes.Add(noms["personCaseTypes"].ByAlias("inspector"));
                        }

                        var examinerData = this.getExaminerData(personId, noms);
                        if (examinerData != null)
                        {
                            personCaseTypes.Add(noms["personCaseTypes"].ByAlias("staffExaminer"));
                        }

                        var personData = this.getPersonData(personId, noms, personCaseTypes);

                        var lot = lotRepository.CreateLot("Person");

                        caseTypeRepository.AddCaseTypes(lot, personData.GetItems<JObject>("caseTypes").Select(t => t.Get<int>("nomValueId")));

                        lot.CreatePart("personData", personData, context);
                        if (inspectorData != null)
                        {
                            lot.CreatePart("inspectorData", inspectorData, context);
                        }

                        if (examinerData != null)
                        {
                            lot.CreatePart("examinerData", examinerData, context);
                        }

                        lot.Commit(context, lotEventDispatcher);
                        unitOfWork.Save();
                        Console.WriteLine("Created personData part for person with id {0}", personId);

                        int lotId = lot.LotId;
                        if (!personIdToLotId.TryAdd(personId, lotId))
                        {
                            throw new Exception(string.Format("personId {0} already present in personIdToLotId dictionary", personId));
                        }

                        var egn = personData.Get<string>("uin");
                        if (!string.IsNullOrWhiteSpace(egn))
                        {
                            if (!personEgnToLotId.TryAdd(egn, lotId))
                            {
                                throw new Exception(string.Format("egn {0} already present in personEgnToLotId dictionary", egn));
                            }
                        }

                        string name = string.Format("{0} {1} {2}", personData.Get<string>("firstName"), personData.Get<string>("middleName"), personData.Get<string>("lastName"));
                        string nameAlt = string.Format("{0} {1} {2}", personData.Get<string>("firstNameAlt"), personData.Get<string>("middleNameAlt"), personData.Get<string>("lastNameAlt"));

                        bool lotIdAdded = personLotIdToPersonNom.TryAdd(lotId, Utils.ToJObject(
                            new
                            {
                                nomValueId = lotId,
                                name = name,
                                nameAlt = nameAlt
                            }));

                        if (!lotIdAdded)
                        {
                            throw new Exception(string.Format("lotId {0} already present in personLotIdToPersonNom dictionary", lotId));
                        }
                    }
                }
                catch (Exception)
                {
                    cts.Cancel();
                    throw;
                }
            }
        }

        private List<NomValue> getPersonCaseTypes(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT CODE FROM CAA_DOC.NM_STAFF_TYPE WHERE ID IN 
                    (SELECT STAFF_TYPE_ID FROM CAA_DOC.LICENCE L INNER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON L.LICENCE_TYPE_ID = LT.ID WHERE {0}
                    UNION ALL SELECT STAFF_TYPE_ID FROM CAA_DOC.RATING_CAA WHERE {0}
                    UNION ALL SELECT PD.STAFF_TYPE_ID FROM CAA_DOC.PERSON_DOCUMENT PD INNER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON PD.LICENCE_TYPE_ID = LT.ID WHERE {0})
                 UNION (SELECT 'awExaminer' as CODE FROM CAA_DOC.EXAMINER WHERE {0} AND PERMITED_AW = 'Y')",
                new DbClause("PERSON_ID = {0}", personId))
                .Materialize(r => r.Field<string>("CODE"))
                .Select(c => PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, c))
                .ToList();
        }

        private JObject getPersonData(int personId, Dictionary<string, Dictionary<string, NomValue>> noms, List<NomValue> caseTypes)
        {
            var lins = new Dictionary<string, string>()
            {
                { "1", "pilots"           },
                { "2", "flyingCrew"       },
                { "3", "crewStaff"        },
                { "4", "headFlights"      },
                { "5", "airlineEngineers" },
                { "6", "dispatchers"      },
                { "7", "paratroopers"     },
                { "8", "engineersRVD"     },
                { "9", "deltaplaner"      }
            };

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON WHERE {0}",
                new DbClause("ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "PERSON",
                        caseTypes = caseTypes,
                        lin = r.Field<int?>("LIN"),
                        linType = r.Field<string>("LIN") != null ?
                            noms["linTypes"].ByCode(lins[r.Field<string>("LIN").Substring(0, 1)]) :
                            noms["linTypes"].ByCode("none"),
                        uin = r.Field<string>("EGN"),
                        firstName = r.Field<string>("NAME"),
                        firstNameAlt = r.Field<string>("NAME_TRANS"),
                        middleName = r.Field<string>("SURNAME"),
                        middleNameAlt = r.Field<string>("SURNAME_TRANS"),
                        lastName = r.Field<string>("FAMILY"),
                        lastNameAlt = r.Field<string>("FAMILY_TRANS"),
                        dateOfBirth = r.Field<DateTime>("DATE_OF_BIRTH"),
                        placeOfBirth = noms["cities"].ByOldId(r.Field<decimal?>("PLACE_OF_BIRTH_ID").ToString()),
                        country = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()),
                        sex = noms["gender"].ByOldId(r.Field<decimal?>("SEX_ID").ToString()),
                        email = r.Field<string>("E_MAIL"),
                        fax = r.Field<string>("FAX"),
                        phone1 = r.Field<string>("PHONE1"),
                        phone2 = r.Field<string>("PHONE2"),
                        phone3 = r.Field<string>("PHONE3"),
                        phone4 = r.Field<string>("PHONE4"),
                        phone5 = r.Field<string>("PHONE5")
                    }))
                .Single();
        }

        private JObject getInspectorData(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT E.ID,
                        E.EXAMINER_CODE,
                        E.VALID_YN,
                        E.CAA_ID,
                        E.STAMP_NUM
                    FROM CAA_DOC.EXAMINER E
                    JOIN CAA_DOC.PERSON P ON P.ID = E.PERSON_ID
                    WHERE E.CAA_ID IS NOT NULL {0}",
                new DbClause("AND P.ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "EXAMINER",

                        examinerCode = r.Field<string>("EXAMINER_CODE"),
                        caa = noms["caa"].ByOldId(r.Field<int>("CAA_ID").ToString()),
                        stampNum = r.Field<string>("STAMP_NUM"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                    }))
                .SingleOrDefault();
        }

        private JObject getExaminerData(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT E.ID,
                        E.EXAMINER_CODE,
                        E.VALID_YN,
                        E.STAMP_NUM
                    FROM CAA_DOC.EXAMINER E
                    WHERE E.CAA_ID IS NULL AND PERMITED_CHECK = 'Y' AND {0}",
                new DbClause("E.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "EXAMINER",

                        examinerCode = r.Field<string>("EXAMINER_CODE"),
                        stampNum = r.Field<string>("STAMP_NUM"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                    }))
                .SingleOrDefault();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            try
            {
                if (disposing && !disposed)
                {
                    this.oracleConn.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
