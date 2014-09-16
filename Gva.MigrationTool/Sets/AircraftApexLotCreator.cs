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
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class AircraftApexLotCreator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public AircraftApexLotCreator(
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
            ConcurrentQueue<int> aircraftApexIds,
            //output
            ConcurrentDictionary<int, int> apexIdtoLotId,
            ConcurrentDictionary<string, int> apexMSNtoLotId,
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

            int aircraftApexId;
            while (aircraftApexIds.TryDequeue(out aircraftApexId))
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

                        var lot = lotRepository.CreateLot("Aircraft");
                        int aircraftCaseTypeId = caseTypeRepository.GetCaseTypesForSet("Aircraft").Single().GvaCaseTypeId;
                        caseTypeRepository.AddCaseTypes(lot, new int[] { aircraftCaseTypeId });

                        var aircraftData = this.getAircraftData(aircraftApexId, noms);
                        lot.CreatePart("aircraftDataApex", aircraftData, context);

                        lot.Commit(context, lotEventDispatcher);

                        unitOfWork.Save();
                        Console.WriteLine("Created aircraftDataApex part for aircraft with APEX id {0}", aircraftApexId);

                        if (!apexIdtoLotId.TryAdd(aircraftApexId, lot.LotId))
                        {
                            throw new Exception(string.Format("aircraftApexId {0} already present in dictionary", aircraftApexId));
                        }

                        var msn = aircraftData.Get<string>("manSN");
                        if (!apexMSNtoLotId.TryAdd(msn, lot.LotId))
                        {
                            Console.WriteLine("DUPLICATE APEX MSN: {0}", msn);//TODO
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

        private JObject getAircraftData(int aircraftId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AIRCRAFT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AIRCRAFT",
                        name = r.Field<string>("NAME"),
                        model = r.Field<string>("MODEL"),
                        series = r.Field<string>("SERIES"),
                        nameAlt = r.Field<string>("NAME_TRANS"),
                        aircraftTypeGroup = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("ID_GROUP").ToString()),
                        aircraftCategory = noms["aircraftCategories"].ByOldId(r.Field<long?>("ID_CATEGORY").ToString()),
                        aircraftSCodeType = noms["aircraftSCodeTypes"].ByOldId(r.Field<long?>("ID_STYPE").ToString()),
                        aircraftProducer = noms["aircraftProducers"].ByOldId(r.Field<long?>("ID_MANUFACTURER").ToString()),
                        manPlace = r.Field<string>("MAN_PLACE"),
                        manDate = r.Field<DateTime?>("MAN_DATE"),
                        manSN = r.Field<string>("MSN"),
                        beaconCodeELT = r.Field<string>("ELT_HEX"),
                        maxMassT = r.Field<decimal?>("MAX_MASS_T"),
                        maxMassL = r.Field<decimal?>("MAX_MASS_L"),
                        ICAO = r.Field<string>("ICAO"),
                        docRoom = r.Field<string>("DOC_ROOM"),
                        mass = new
                        {
                            mass = r.Field<decimal?>("MASS"),
                            cax = r.Field<float?>("MASS_CAX"),
                            date = r.Field<DateTime?>("MASS_DATE")
                        },
                        ultralight = new
                        {
                            color = r.Field<string>("COLOR"),
                            colorAlt = r.Field<string>("COLOR_TRANS"),
                            seats = r.Field<string>("SEATS"),
                            payload = r.Field<string>("PAYLOAD")
                        },
                        noise = new
                        {
                            flyover = r.Field<float?>("FLYOVER_NOISE"),
                            approach = r.Field<float?>("APPROACH_NOISE"),
                            lateral = r.Field<float?>("LATERAL_NOISE"),
                            overflight = r.Field<float?>("OVERFLIGHT_NOISE"),
                            takeoff = r.Field<float?>("TAKE_OFF_NOISE")
                        },
                        radio = new
                        {
                            approvalNumber = r.Field<string>("RADIO_NO"),
                            approvalDate = r.Field<DateTime?>("RADIO_DATE"),
                            incommingApprovalnumber = r.Field<string>("RADIO_DOC"),
                            incommingApprovalDate = r.Field<DateTime?>("RADIO_DOC_DATE")
                        }
                    }))
                .Single();
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
