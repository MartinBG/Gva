using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common;
using Common.Api;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api;
using Gva.Api;
using Gva.Api.CommonUtils;
using Regs.Api;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.ProjectionRebuilder
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Invalid arguments.");
                Console.WriteLine("Usage: Gva.ProjectionRebuilder <setAlias> <projectionName>");
                return 1;
            }

            string setAlias = args[0];
            string projectionName = args[1];

            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new CommonApiModule());
            builder.RegisterModule(new DocsApiModule());
            builder.RegisterModule(new GvaApiModule());
            builder.RegisterModule(new RegsApiModule());
            builder.Register(c => new UnathorizedUserContext()).As<UserContext>().SingleInstance();

            var container = builder.Build();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            List<int> lotIds;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                lotIds =
                    conn.CreateStoreCommand(@"
                        select
                            l.LotId
                        from
                            Lots l
                            inner join LotSets ls on ls.LotSetId = l.LotSetId
                        where {0}",
                    new DbClause("ls.Alias = {0}", setAlias))
                    .Materialize(r => r.Field<int>("LotId"))
                    .ToList();
            }

            Console.WriteLine("Found {0} lots.", lotIds.Count);

            int done = 0;
            foreach (var lotId in lotIds)
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    try
                    {
                        var unitOfWork = scope.Resolve<IUnitOfWork>();
                        var lotRepository = scope.Resolve<ILotRepository>();
                        var projection =
                            scope
                            .Resolve<ILotEventHandler[]>()
                            .Where(h => h is IProjection && h.GetType().FullName == projectionName)
                            .Cast<IProjection>()
                            .Single();

                        var lot = lotRepository.GetLotIndex(lotId, fullAccess: true);

                        projection.RebuildLot(lot);

                        unitOfWork.Save();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error at lotId: {0}.", lotId);
                        Console.WriteLine(e);
                    }
                }

                done++;
                if (done % 100 == 0)
                {
                    Console.WriteLine("Done {0} lots.", done);
                }
            }

            Console.WriteLine("Done {0} lots.", done);

            timer.Stop();
            Console.WriteLine("Rebuilding completed in - {0} minutes.", timer.Elapsed.TotalMinutes);

            return 0;
        }
    }
}
