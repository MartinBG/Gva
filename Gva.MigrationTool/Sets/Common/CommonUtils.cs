using System;
using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Oracle.DataAccess.Client;

namespace Gva.MigrationTool.Sets.Common
{
    public static class CommonUtils
    {
        public static IList<GvaApplicationStage> getApplicationStages(
            OracleConnection oracleConn,
            IDictionary<int, int> personIdToLotId,
            int newApplicationId,
            int oldApplicationId)
        {
            var gvaStageIds = new Dictionary<int, int>()
            {
                { 0, 1},
                { 1, 2},
                { 2, 5},
                { 3, 4},
                { 4, 3},
                { 5, 8},
                { 101, 6},
                { 102, 7}
            };

            return oracleConn.CreateStoreCommand(
                @"SELECT r.ID,
                         r.EXAMINER_PERSON_ID,
                         rl.STATE_ID,
                         rl.INS_DATE,
                         rl.INS_USER,
                         s.SEQ_NO,
                         row_number() OVER (ORDER BY rl.ON_DATE, rl.ID ) as ORDINAL
                    FROM CAA_DOC.REQUEST r
                    LEFT JOIN CAA_DOC.REQUEST_LOG rl on r.ID = rl.REQUEST_ID
                    JOIN CAA_DOC.NM_REQUEST_STATE s on rl.STATE_ID = s.id
                    WHERE {0}",
                    new DbClause("r.ID = {0}", oldApplicationId)
                )
                .Materialize(r =>
                    new GvaApplicationStage()
                    {
                        GvaApplicationId = newApplicationId,
                        GvaStageId = gvaStageIds[r.Field<int>("STATE_ID")],
                        StartingDate = r.Field<DateTime>("INS_DATE"),
                        InspectorLotId = !Migration.IsPartialMigration ?
                            (r.Field<int?>("EXAMINER_PERSON_ID") != null ? personIdToLotId[r.Field<int>("EXAMINER_PERSON_ID")] : (int?)null) :
                            (r.Field<int?>("EXAMINER_PERSON_ID") != null ? (personIdToLotId.ContainsKey(r.Field<int>("EXAMINER_PERSON_ID")) ? personIdToLotId[r.Field<int>("EXAMINER_PERSON_ID")] : (int?)null) : (int?)null),
                        Ordinal = r.Field<int>("ORDINAL")
                    })
                .ToList();
        }
    }
}
