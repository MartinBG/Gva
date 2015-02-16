using System;
using System.Collections.Generic;
using System.Linq;
using Gva.Api.CommonUtils;
using Gva.Api.Models;
using Oracle.ManagedDataAccess.Client;

namespace Gva.MigrationTool.Sets.Common
{
    public static class CommonUtils
    {
        public static IList<GvaApplicationStage> GetApplicationStages(
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
                        s.CODE,
                        stages.ON_DATE as start_term_date,
                        rt.TIME_LIMIT,
                        row_number() OVER (ORDER BY rl.ON_DATE, rl.ID ) as ORDINAL
                    FROM CAA_DOC.REQUEST r
                    LEFT JOIN CAA_DOC.NM_REQUEST_TYPE rt on rt.id = r.request_type_id
                    LEFT JOIN CAA_DOC.REQUEST_LOG rl on r.ID = rl.REQUEST_ID
                    JOIN CAA_DOC.NM_REQUEST_STATE s on rl.STATE_ID = s.id
                    LEFT JOIN CAA_DOC.EXAMINER e on e.PERSON_ID = rl.PERSON_ID
                    LEFT JOIN CAA_DOC.PERSON p on e.PERSON_ID = p.ID
                    LEFT JOIN (select on_date, id from (select row_number() over (partition by rl.request_id order by rl.on_date desc, rl.id desc) r_number,
                                                rl.id, rl.on_date, rl.request_id
                                                      from CAA_DOC.REQUEST_LOG rl,
                                                          CAA_DOC.nm_request_state s
                                                      where s.code in ('NEW', 'NWD')) 
                                                       where r_number = 1) stages on stages.id = rl.id
                    WHERE {0}",
                    new DbClause("r.ID = {0}", oldApplicationId)
                )
                .Materialize(r => new
                    {
                            GvaApplicationId = newApplicationId,
                            GvaStageId = gvaStageIds[r.Field<int>("STATE_ID")],
                            StartingDate = r.Field<DateTime>("ON_DATE"),
                            start_term_date = r.Field<DateTime?>("start_term_date"),
                            TIME_LIMIT = r.Field<int?>("TIME_LIMIT"),
                            InspectorLotId = !Migration.IsPartialMigration ?
                                (r.Field<int?>("EXAMINER_ID") != null ? personIdToLotId[r.Field<int>("EXAMINER_ID")] : (int?)null) :
                                (r.Field<int?>("EXAMINER_ID") != null ? (personIdToLotId.ContainsKey(r.Field<int>("EXAMINER_ID")) ? personIdToLotId[r.Field<int>("EXAMINER_ID")] : (int?)null) : (int?)null),
                            Ordinal = r.Field<int>("ORDINAL"),
                            Note = r.Field<string>("NOTE")
                    })
                .ToList()
                .Select(s => 
                {
                    dynamic stageTermDate = null;
                    if(s.start_term_date.HasValue && s.TIME_LIMIT.HasValue && s.GvaStageId < 6)
                    {
                        stageTermDate = s.start_term_date.Value.AddDays(s.TIME_LIMIT.Value);
                    }

                    return new GvaApplicationStage
                    {
                        GvaApplicationId = s.GvaApplicationId,
                        StageTermDate = stageTermDate,
                        GvaStageId = s.GvaStageId,
                        StartingDate = s.StartingDate,
                        InspectorLotId = s.InspectorLotId,
                        Ordinal = s.Ordinal,
                        Note = s.Note
                    };
                })
                .ToList();
        }
    }
}
