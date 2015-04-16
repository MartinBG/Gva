using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.CommonUtils;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.FixFlyingExpMigrationTool.Sets
{
    public class PersonLotFixMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public PersonLotFixMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public List<int> GetPersonsLotIdsWithFlyingExperiences()
        {
            using (var dependencies = this.dependencyFactory())
            {
                return dependencies.Value.Item1.DbContext.Set<Part>()
                    .Where(p => p.Path.Contains("personFlyingExperiences"))
                    .Select(l => l.LotId)
                    .Distinct()
                    .ToList();
            }
        }

        public void StartFixOfMigration(
            //intput
            ConcurrentQueue<int> personLotIds,
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

            int lotId;
            while (personLotIds.TryDequeue(out lotId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = this.dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var lotEventDispatcher = dependencies.Value.Item3;
                        var context = dependencies.Value.Item4;

                        Lot lot = lotRepository.GetLotIndex(lotId, fullAccess: true);

                        var flyingExperiencePartVersions = lot.Index.GetParts<JObject>("personFlyingExperiences");

                        bool hasSomethingToCommit = false;
                        foreach (var flyingExperience in flyingExperiencePartVersions)
                        {
                            var updatedFlyingExp = this.fixPersonFlyingExperience(flyingExperience);
                            if (updatedFlyingExp != null)
                            {
                                string partVersionPath = string.Format("{0}/{1}", "personFlyingExperiences", updatedFlyingExp.Part.Index);
                                lot.UpdatePart<JObject>(partVersionPath, updatedFlyingExp.Content, context);
                                hasSomethingToCommit = true;
                            }
                        }

                        try
                        {
                            if (hasSomethingToCommit)
                            {
                                lot.Commit(context, lotEventDispatcher);

                                unitOfWork.Save();
                            }
                            else
                            {
                                Console.WriteLine("No flying experience part to update of person with lotId: {0}", lotId);
                            }
                        }
                        catch (Exception)
                        {
                            cts.Cancel();
                            throw;
                        }

                        Console.WriteLine("Fix migration of flying experiences of person with lotId: {0}", lotId);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in migration of flying experiences of person with lotId: {0}", lotId);

                    cts.Cancel();
                    throw;
                }
            }
        }

        private PartVersion<JObject> fixPersonFlyingExperience(PartVersion<JObject> flyingExperiencePartVersion)
        {
            int? oldId = flyingExperiencePartVersion.Content.Get<int?>("__oldId");
            if (oldId.HasValue)
            {
                var result = this.oracleConn.CreateStoreCommand(
                @"SELECT AMOUNT,
                    AMOUNT_M,
                    AMOUNT_SUM, 
                    AMOUNT_M_SUM,
                    AMOUNT_12,
                    AMOUNT_M_12
                    FROM CAA_DOC.FLYING_EXPERIENCE WHERE {0}",
                new DbClause("ID = {0}", oldId.Value)
                )
                .Materialize(r =>
                        Utils.ToJObject(new
                        {
                            total = Utils.TimeToMilliseconds(r.Field<int?>("AMOUNT"), r.Field<short?>("AMOUNT_M")),
                            totalDoc = Utils.TimeToMilliseconds(r.Field<int?>("AMOUNT_SUM"), r.Field<short?>("AMOUNT_M_SUM")),
                            totalLastMonths = Utils.TimeToMilliseconds(r.Field<int?>("AMOUNT_12"), r.Field<short?>("AMOUNT_M_12"))
                        }))
                         .Single();

                flyingExperiencePartVersion.Content["total"] = result.Get<long?>("total");
                flyingExperiencePartVersion.Content["totalDoc"] = result.Get<long?>("totalDoc");
                flyingExperiencePartVersion.Content["totalLastMonths"] = result.Get<long?>("totalLastMonths");

                return flyingExperiencePartVersion;
            }
            else
            {
                return null;
            }
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
