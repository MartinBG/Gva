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
                        p.ID as EXAMINER_ID,
                        rl.STATE_ID,
                        rl.ON_DATE,
                        rl.INS_USER,
                        rl.NOTE,
                        s.SEQ_NO,
                        row_number() OVER (ORDER BY rl.ON_DATE, rl.ID ) as ORDINAL
                    FROM CAA_DOC.REQUEST r
                    LEFT JOIN CAA_DOC.REQUEST_LOG rl on r.ID = rl.REQUEST_ID
                    JOIN CAA_DOC.NM_REQUEST_STATE s on rl.STATE_ID = s.id
                    LEFT JOIN CAA_DOC.EXAMINER e on e.PERSON_ID = rl.PERSON_ID
                    JOIN CAA_DOC.PERSON p on e.PERSON_ID = p.ID
                    WHERE {0}",
                    new DbClause("r.ID = {0}", oldApplicationId)
                )
                .Materialize(r =>
                    new GvaApplicationStage()
                    {
                        GvaApplicationId = newApplicationId,
                        GvaStageId = gvaStageIds[r.Field<int>("STATE_ID")],
                        StartingDate = r.Field<DateTime>("ON_DATE"),
                        InspectorLotId = !Migration.IsPartialMigration ?
                            (r.Field<int?>("EXAMINER_ID") != null ? personIdToLotId[r.Field<int>("EXAMINER_ID")] : (int?)null) :
                            (r.Field<int?>("EXAMINER_ID") != null ? (personIdToLotId.ContainsKey(r.Field<int>("EXAMINER_ID")) ? personIdToLotId[r.Field<int>("EXAMINER_ID")] : (int?)null) : (int?)null),
                        Ordinal = r.Field<int>("ORDINAL"),
                        Note = r.Field<string>("NOTE")
                    })
                .ToList();
        }
    }
}
