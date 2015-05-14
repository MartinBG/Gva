using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.FileRepository;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class AircraftRadioCertsMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;

        public AircraftRadioCertsMigrator(
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
        }

        public void StartMigrating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            //input
            ConcurrentDictionary<int, List<AircraftCertRadioDO>> radiosByAircraftLotId,
            ConcurrentQueue<int> radiosByLotId,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            int aircraftLotId;
            while (radiosByLotId.TryDequeue(out aircraftLotId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var userRepository = dependencies.Value.Item3;
                        var fileRepository = dependencies.Value.Item4;
                        var lotEventDispatcher = dependencies.Value.Item5;
                        var context = dependencies.Value.Item6;

                        List<AircraftCertRadioDO> aircraftRadioCerts = new List<AircraftCertRadioDO>();
                        if (!radiosByAircraftLotId.TryGetValue(aircraftLotId, out aircraftRadioCerts))
                        {
                            continue;
                        }

                        var lot = lotRepository.GetLotIndex(aircraftLotId, fullAccess: true);

                        Func<string, JObject, PartVersion> addPartWithFiles = (path, content) =>
                        {
                            var pv = lot.CreatePart(path, content.Get<JObject>("part"), context);
                            fileRepository.AddFileReferences(pv.Part, content.GetItems<CaseDO>("files"));
                            return pv;
                        };

                        foreach (var aircraftRadioCert in aircraftRadioCerts)
                        {
                            var radioPart = new JObject(
                                new JProperty("part", Utils.ToJObject(aircraftRadioCert)),
                                new JProperty("files",
                                    new JArray(
                                        new JObject(
                                            new JProperty("isAdded", true),
                                            new JProperty("file", null),
                                            new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                            new JProperty("bookPageNumber", null),
                                            new JProperty("pageCount", null),
                                            new JProperty("applications", new JArray())))));

                            addPartWithFiles("aircraftCertRadios/*", radioPart);
                        }

                        try
                        {
                            lot.Commit(context, lotEventDispatcher);
                        }
                        // ignore empty commit exceptions
                        catch (InvalidOperationException)
                        {
                        }

                        unitOfWork.Save();

                        Console.WriteLine("Migrated radio certs of aircraftId: {0}", aircraftLotId);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in radio certs of aircraftId: {0}", aircraftLotId);

                    cts.Cancel();
                    throw;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            this.disposed = true;
        }
    }
}
