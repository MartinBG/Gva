using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using Autofac;
using Common.Data;
using Gva.Api.CommonUtils;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    public class ProjectionsController : ApiController
    {
        private ILifetimeScope lifetimeScope;

        public ProjectionsController(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        [HttpGet]
        [Route("api/rebuild")]
        [Authorize]
        public IHttpActionResult Rebuild(string setAlias, string projectionName)
        {
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

            foreach (var lotId in lotIds)
            {
                using (var scope = this.lifetimeScope.BeginLifetimeScope())
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
            }

            return Ok();
        }
    }
}