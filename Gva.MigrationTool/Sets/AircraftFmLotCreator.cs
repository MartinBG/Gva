using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class AircraftFmLotCreator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private SqlConnection sqlConn;

        public AircraftFmLotCreator(
            SqlConnection sqlConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.sqlConn = sqlConn;
        }

        public void StartCreating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            //intput
            ConcurrentQueue<string> aircraftFmIds,
            //output
            ConcurrentDictionary<string, int> fmIdtoLotId,
            ConcurrentDictionary<string, int> MSNtoLotId,
            ConcurrentDictionary<int, JObject> aircraftLotIdToAircraftNom,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.sqlConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            string aircraftFmId;
            while (aircraftFmIds.TryDequeue(out aircraftFmId))
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

                        var aircraftDataFM = this.getAircraftDataFM(aircraftFmId, noms);
                        lot.CreatePart("aircraftData", aircraftDataFM, context);
                        lot.Commit(context, lotEventDispatcher);

                        unitOfWork.Save();
                        Console.WriteLine("Created aircraftData part for aircraft with FM id {0}", aircraftFmId);

                        if (!fmIdtoLotId.TryAdd(aircraftFmId, lot.LotId))
                        {
                            throw new Exception(string.Format("aircraftFmId {0} already present in dictionary", aircraftFmId));
                        }

                        var msn = aircraftDataFM.Get<string>("manSN");
                        if (!MSNtoLotId.TryAdd(msn, lot.LotId))
                        {
                            Console.WriteLine("DUPLICATE APEX MSN: {0}", msn);//TODO
                        }

                        bool aircraftLotIdAdded = aircraftLotIdToAircraftNom.TryAdd(lot.LotId, Utils.ToJObject(
                            new
                            {
                                nomValueId = lot.LotId,
                                name = aircraftDataFM.Get<string>("model"),
                                nameAlt = aircraftDataFM.Get<string>("modelAlt"),
                                textContent =
                                    new
                                    {
                                        airCategory = aircraftDataFM.Get<JObject>("airCategory"),
                                        aircraftProducer = aircraftDataFM.Get<JObject>("aircraftProducer")
                                    }
                            }));
                        if (!aircraftLotIdAdded)
                        {
                            throw new Exception(string.Format("aircraftLotId {0} already present in dictionary", lot.LotId));
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

        private JObject getAircraftDataFM(string aircraftId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.sqlConn.CreateStoreCommand(
                @"select * from Acts where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and n_Act_ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<string>("n_Act_ID"),
                        __migrTable = "Acts",
                        aircraftProducer = noms["aircraftProducersFm"].ByOldId(r.Field<string>("n_Act_Maker_ID")),
                        aircraftCategory = noms["aircraftCategories"].ByCode(r.Field<string>("t_Act_TypeCode")),
                        icao = r.Field<string>("t_Act_ICAO"),
                        model = r.Field<string>("t_Act_Bg"),
                        modelAlt = r.Field<string>("t_Act_EN"),
                        manSN = r.Field<string>("t_Act_MSN"),
                        engine = r.Field<string>("t_Act_EngBg"),
                        engineAlt = r.Field<string>("t_Act_EngEn"),
                        propeller = r.Field<string>("t_Act_PropBg"),
                        propellerAlt = r.Field<string>("t_Act_PropEn"),
                        modifOrWingColor = r.Field<string>("t_Act_ModifOrWingColorBg"),
                        modifOrWingColorAlt = r.Field<string>("t_Act_ModifOrWingColorEn"),
                        maxMassT = (int?)Utils.FmToDecimal(r.Field<string>("n_Act_MTOM")),
                        maxMassL = (int?)Utils.FmToDecimal(r.Field<string>("n_Act_MLMorPayMass")),
                        seats = Utils.FmToNum(r.Field<string>("n_Act_SeatNo")),
                        outputDate = Utils.FmToDate(r.Field<string>("d_Act_DateOutput")),
                        docRoom = r.Field<string>("t_Act_Place_of_Documents"),
                        cofAType = noms["CofATypesFm"].ByName(r.Field<string>("t_CofA_Type").Replace("\"", string.Empty)),
                        airCategory = noms["EASATypesFm"].ByName(r.Field<string>("t_Act_EASA_Type")),
                        euRegType = noms["EURegTypesFm"].ByName(r.Field<string>("t_Act_EU_RU")),
                        easaCategory = noms["EASACategoriesFm"].ByName(r.Field<string>("t_EASA_Category")),
                        tcds = r.Field<string>("t_EASA_TCDS"),
                        noise = new
                        {
                            issueNumber = Utils.FmToNum(r.Field<string>("n_Noise_No_Issued")),
                            issueDate = Utils.FmToDate(r.Field<string>("d_CofN_45_Date")),
                            tcdsn = r.Field<string>("t_Noise_TCDS"),
                            chapter = r.Field<string>("t_Noise_Chapter"),
                            lateral = Utils.FmToDecimal(r.Field<string>("n_Noise_Literal")),
                            approach = Utils.FmToDecimal(r.Field<string>("n_Noise_Approach")),
                            flyover = Utils.FmToDecimal(r.Field<string>("n_Noise_FlyOver")),
                            overflight = Utils.FmToDecimal(r.Field<string>("n_Noise_OverFlight")),
                            takeoff = Utils.FmToDecimal(r.Field<string>("n_Noise_TakeOff")),
                            modifications = r.Field<string>("t_Noise_AddModifBg"),
                            modificationsAlt = r.Field<string>("t_Noise_AddModifEn"),
                            notes = r.Field<string>("t_Noise_Remark")
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
                    this.sqlConn.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
