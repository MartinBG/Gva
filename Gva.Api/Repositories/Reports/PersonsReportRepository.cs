using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Gva.Api.CommonUtils;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Persons.Reports;

namespace Gva.Api.Repositories.Reports
{
    public class PersonsReportRepository : IPersonsReportRepository
    {
        private INomRepository nomRepository;

        public PersonsReportRepository(INomRepository nomRepository)
        {
            this.nomRepository = nomRepository;
        }

        public Tuple<int, List<PersonReportDocumentDO>> GetDocuments(
            SqlConnection conn,
            int? roleId = null,
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? typeId = null,
            int? lin = null,
            int? limitationId = null,
            string docNumber = null,
            string publisher = null,
            int? medClassId = null,
            string sortBy = null,
            int offset = 0,
            int limit = 10)
        {
            string limName = limitationId.HasValue ? this.nomRepository.GetNomValue(limitationId.Value).Name : null;
            Dictionary<string, string> sortByToTableColumn = new Dictionary<string, string>()
            {
                {"lin", "p.Lin"},
                {"role", "nv2.Name"},
                {"type", "nv1.Name"},
                {"valid", "d.Valid"},
                {"publisher", "d.Publisher"},
                {"number", "d.DocumentNumber"},
                {"medClass", "nv3.Name"},
                {"fromDate", "d.FromDate"},
                {"toDate", "d.ToDate"},
                {"limitations", "d.Limitations"},
            };
            string orderBy = !string.IsNullOrEmpty(sortBy) && sortByToTableColumn.ContainsKey(sortBy) ? sortByToTableColumn[sortBy] : "d.FromDate";

            var queryResult = conn.CreateStoreCommand(
                    @"SELECT COUNT(*) OVER() as allResultsCount,
                        p.LotId,
                        p.Lin,
                        nv2.Name as Role,
                        nv1.Name as Type,
                        d.DocumentNumber,
                        d.FromDate,
                        d.Date,
                        d.ToDate,
                        d.Publisher,
                        d.Valid,
                        d.Limitations,
                        nv3.Name as MedClass
                        FROM GvaViewPersonDocuments d
                        INNER JOIN GvaViewPersons p ON d.LotId = p.LotId
                        LEFT JOIN LotParts lp on lp.LotPartId = d.LotPartId
                        LEFT JOIN NomValues nv1 ON nv1.NomValueId = d.TypeId
                        LEFT JOIN NomValues nv2 ON nv2.NomValueId = d.RoleId
                        LEFT JOIN NomValues nv3 ON nv3.NomValueId = d.MedClassId
                    WHERE 1=1 {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}
                    ORDER BY " + orderBy + @" DESC
                    OFFSET {11} ROWS FETCH NEXT {12} ROWS ONLY",
                    new DbClause("and d.FromDate >= {0}", fromDatePeriodFrom),
                    new DbClause("and d.FromDate <= {0}", fromDatePeriodTo),
                    new DbClause("and d.ToDate >= {0}", toDatePeriodFrom),
                    new DbClause("and d.ToDate <= {0}", toDatePeriodTo),
                    new DbClause("and p.Lin = {0}", lin),
                    new DbClause("and nv1.NomValueId = {0}", typeId),
                    new DbClause("and nv2.NomValueId = {0}", roleId),
                    new DbClause("and d.DocumentNumber like '%' + {0} + '%'", docNumber),
                    new DbClause("and d.Publisher like '%' + {0} + '%'", publisher),
                    new DbClause("and (d.Limitations like {0} + '$$%' or d.Limitations like '%$$' + {0} or d.Limitations like '%$$' + {0} + '$$%' or d.Limitations like {0})", limName),
                    new DbClause("and nv3.NomValueId = {0}", medClassId),
                    new DbClause("{0}", offset),
                    new DbClause("{0}", limit))
                    .Materialize(r => new Tuple<int, PersonReportDocumentDO>
                        ( 
                        r.Field<int>("allResultsCount"),
                        new PersonReportDocumentDO()
                        {
                            LotId = r.Field<int>("LotId"),
                            Lin = r.Field<int?>("Lin"),
                            Role = r.Field<string>("Role"),
                            Type = r.Field<string>("Type"),
                            Number = r.Field<string>("DocumentNumber"),
                            FromDate = r.Field<DateTime?>("FromDate") ?? r.Field<DateTime?>("Date"),
                            ToDate = r.Field<DateTime?>("ToDate"),
                            Valid = r.Field<bool?>("Valid"),
                            Publisher = r.Field<string>("Publisher"),
                            Limitations = !string.IsNullOrEmpty(r.Field<string>("Limitations")) ? r.Field<string>("Limitations").Replace(GvaConstants.ConcatenatingExp, ", ") : null,
                            MedClass = r.Field<string>("MedClass")
                        }))
                        .ToList();

            var results = queryResult.Select(q => q.Item2).ToList();
            int count = queryResult.Select(q => q.Item1).FirstOrDefault();

            return new Tuple<int, List<PersonReportDocumentDO>>(count, results);
        }

        public Tuple<int, List<PersonReportLicenceDO>> GetLicences(
            SqlConnection conn,
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null,
            int? limitationId = null,
            string sortBy = null,
            int offset = 0,
            int limit = 10)
        {
            string limName = limitationId.HasValue ? this.nomRepository.GetNomValue(limitationId.Value).Name : null;
            Dictionary<string, string> sortByToTableColumn = new Dictionary<string, string>()
            {
                {"lin", "p.Lin"},
                {"uin", "p.Uin"},
                {"licenceTypeName", "lt.Name"},
                {"licenceCode", "l.PublisherCode + ' ' + l.LicenceTypeCaCode + ' ' + RIGHT('00000' + CAST(l.LicenceNumber AS NVARCHAR(5)),5)"},
                {"names", "p.Names"},
                {"fromDate", "le.DateValidFrom"},
                {"toDate", "le.DateValidTo"},
                {"firstIssueDate", "le.FirstDocDateValidFrom"},
                {"licenceAction", "d.ToDate"},
                {"limitations", "le.Limitations"},
                {"stampNumber", "le.StampNumber"}
            };
            string orderBy = !string.IsNullOrEmpty(sortBy) && sortByToTableColumn.ContainsKey(sortBy) ? sortByToTableColumn[sortBy] : "le.DateValidFrom";

            var queryResult = conn.CreateStoreCommand(
                        @"SELECT
                             COUNT(*) OVER() as allResultsCount,
                             p.LotId,
                             p.Lin,
                             p.Uin AS uin,
                             p.Names,
                             lt.Name AS LicenceTypeName,
                             l.PublisherCode + ' ' + l.LicenceTypeCaCode + ' ' + RIGHT('00000' + CAST(l.LicenceNumber AS NVARCHAR(5)),5) AS LicenceCode,
                             le.DateValidFrom,
                             le.DateValidTo,
                             le.FirstDocDateValidFrom AS FirstIssueDate, 
                             la.Name AS LicenceAction,
                             le.StampNumber,
                             le.Limitations
                         FROM 
                         GvaViewPersonLicenceEditions le
                         INNER JOIN GvaViewPersonLicences l ON le.LotId = l.LotId and le.LicencePartIndex = l.PartIndex
                         INNER JOIN GvaViewPersons p ON l.LotId = p.LotId
                         INNER JOIN NomValues lt ON lt.NomValueId = l.LicenceTypeId
                         INNER JOIN NomValues la ON la.NomValueId = le.LicenceActionId
                         WHERE 1=1 {0} {1} {2} {3} {4} {5} {6} {7}
                         ORDER BY " + orderBy + @" DESC 
                         OFFSET {8} ROWS FETCH NEXT {9} ROWS ONLY",
                        new DbClause("and le.DateValidFrom >= {0}", fromDatePeriodFrom),
                        new DbClause("and le.DateValidFrom <= {0}", fromDatePeriodTo),
                        new DbClause("and le.DateValidTo >= {0}", toDatePeriodFrom),
                        new DbClause("and le.DateValidTo <= {0}", toDatePeriodTo),
                        new DbClause("and p.Lin = {0}", lin),
                        new DbClause("and lt.NomValueId = {0}", licenceTypeId),
                        new DbClause("and la.NomValueId = {0}", licenceActionId),
                        new DbClause("and (le.Limitations like {0} + '$$%' or le.Limitations like '%$$' + {0} or le.Limitations like '%$$%' + {0} + '$$' or le.Limitations like {0})", limName),
                        new DbClause("{0}", offset),
                        new DbClause("{0}", limit))
                         .Materialize(r => new Tuple<int, PersonReportLicenceDO>
                             (
                             r.Field<int>("allResultsCount"),
                             new PersonReportLicenceDO()
                                {
                                    LotId = r.Field<int>("lotId"),
                                    Lin = r.Field<int?>("lin"),
                                    Uin = r.Field<string>("uin"),
                                    Names = r.Field<string>("names"),
                                    LicenceTypeName = r.Field<string>("licenceTypeName"),
                                    LicenceCode = r.Field<string>("licenceCode"),
                                    DateValidFrom = r.Field<DateTime?>("dateValidFrom"),
                                    DateValidTo = r.Field<DateTime?>("dateValidTo"),
                                    FirstIssueDate = r.Field<DateTime?>("firstIssueDate"),
                                    LicenceAction = r.Field<string>("licenceAction"),
                                    StampNumber = r.Field<string>("stampNumber"),
                                    Limitations = !string.IsNullOrEmpty(r.Field<string>("limitations")) ? r.Field<string>("limitations").Replace(GvaConstants.ConcatenatingExp, ", ") : null
                            }))
                            .ToList();

            var results = queryResult.Select(q => q.Item2).ToList();
            int count = queryResult.Select(q => q.Item1).FirstOrDefault();

            return new Tuple<int, List<PersonReportLicenceDO>>(count, results);
        }

        public Tuple<int, List<PersonReportRatingDO>> GetRatings(
            SqlConnection conn,
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null,
            int? limitationId = null,
            string sortBy = null,
            int offset = 0,
            int limit = 10)
        {
            string limCode = limitationId.HasValue? this.nomRepository.GetNomValue(limitationId.Value).Code : null;
            Dictionary<string, string> sortByToTableColumn = new Dictionary<string, string>()
            {
                {"lin", "p.Lin"},
                {"fromDate", "re.DocDateValidFrom"},
                {"toDate", "re.DocDateValidTo"},
                {"firstIssueDate", "re2.DocDateValidFrom"},
                {"personRatingLevel", "rl.Code"},
                {"ratingTypes", "r.RatingTypes"},
                {"locationIndicator", "li.Code"},
                {"sector", "r.Sector"},
                {"limitations", "re.Limitations"},
                {"authorizationCode", "a.Code"},
            };

            string orderBy = !string.IsNullOrEmpty(sortBy) && sortByToTableColumn.ContainsKey(sortBy) ? sortByToTableColumn[sortBy] : "re.DocDateValidFrom";

            var queryResult = conn.CreateStoreCommand(@"
                         SELECT 
                            COUNT(*) OVER() as allResultsCount,
                            p.Lin,
                            r.LotId,
                            lastEdition.RatingPartIndex,
                            re2.DocDateValidFrom AS firstIssueDate,
                            re.RatingSubClasses,
                            re.Limitations,
                            re.DocDateValidFrom,
                            re.DocDateValidTo,
                            r.RatingTypes,
                            r.Sector,
                            rc.Code as RatingClass,
                            a.Code as AuthorizationCode,
                            ct.Code as AircraftTypeCategory,
                            atg.Code as AircraftTypeGroup,
                            li.Code as LocationIndicator,
                            rl.Code as RatingLevel
                        FROM  GvaViewPersonRatings r
                        INNER JOIN GvaViewPersons p ON r.LotId = p.LotId
                        INNER JOIN (select 
                            r.LotId,
                            max(re.PartIndex) as edition_part_index,
                            re.RatingPartIndex
                            FROM  GvaViewPersonRatings r
                            INNER JOIn GvaViewPersonRatingEditions re on r.LotId = re.LotId and r.PartIndex = re.RatingPartIndex
                            group by re.RatingPartIndex, r.LotId) lastEdition on lastEdition.LotId = r.LotId and lastEdition.RatingPartIndex = r.PartIndex
                        INNER JOIN GvaViewPersonRatingEditions re on lastEdition.LotId = re.LotId and re.PartIndex = lastEdition.edition_part_index
                        INNER JOIN (select 
                            r.LotId,
                            min(re.PartIndex) AS edition_part_index,
                            re.RatingPartIndex
                            FROM  GvaViewPersonRatings r
                            INNER JOIn GvaViewPersonRatingEditions re on r.LotId = re.LotId and r.PartIndex = re.RatingPartIndex
                            group by re.RatingPartIndex, r.LotId) firstEdition on firstEdition.LotId = lastEdition.LotId and firstEdition.RatingPartIndex = lastEdition.RatingPartIndex
                        INNER JOIN GvaViewPersonRatingEditions re2 on firstEdition.LotId = re2.LotId and re2.PartIndex = firstEdition.edition_part_index
                        LEFT JOIN NomValues rc ON rc.NomValueId = r.RatingClassId
                        LEFT JOIN NomValues a ON a.NomValueId = r.AuthorizationId
                        LEFT JOIN NomValues ct ON ct.NomValueId = r.AircraftTypeCategoryId
                        LEFT JOIN NomValues atg ON atg.NomValueId = r.AircraftTypeGroupId
                        LEFT JOIN NomValues li ON li.NomValueId = r.LocationIndicatorId
                        LEFT JOIN NomValues rl ON rl.NomValueId = r.RatingLevelId
                        WHERE 1=1 {0} {1} {2} {3} {4} {5} {6} {7} {8}
                        ORDER BY " + orderBy + @" DESC
                        OFFSET {9} ROWS FETCH NEXT {10} ROWS ONLY",
                        new DbClause("and re.DocDateValidFrom >= {0}", fromDatePeriodFrom),
                        new DbClause("and re.DocDateValidFrom <= {0}", fromDatePeriodTo),
                        new DbClause("and re.DocDateValidTo >= {0}", toDatePeriodFrom),
                        new DbClause("and re.DocDateValidTo <= {0}", toDatePeriodTo),
                        new DbClause("and p.Lin = {0}", lin),
                        new DbClause("and r.RatingClassId = {0}", ratingClassId),
                        new DbClause("and r.AuthorizationId = {0}", authorizationId),
                        new DbClause("and r.AircraftTypeGroupId = {0}", aircraftTypeCategoryId),
                        new DbClause("and (re.Limitations like {0} + '$$%' re.Limitations like '%$$' + {0} or re.Limitations like '%$$' + {0} + '$$%' or re.Limitations like {0})", limCode),
                        new DbClause("{0}", offset),
                        new DbClause("{0}", limit))
                        .Materialize(r => new Tuple<int, PersonReportRatingDO>
                             (
                             r.Field<int>("allResultsCount"),
                             new PersonReportRatingDO()
                             {
                                 Lin = r.Field<int?>("lin"),
                                 LotId = r.Field<int>("lotId"),
                                 RatingSubClasses = r.Field<string>("RatingSubClasses"),
                                 Limitations = !string.IsNullOrEmpty(r.Field<string>("Limitations")) ? r.Field<string>("Limitations").Replace(GvaConstants.ConcatenatingExp, ", ") : null,
                                 FirstIssueDate = r.Field<DateTime?>("FirstIssueDate"),
                                 DateValidFrom = r.Field<DateTime?>("DocDateValidFrom"),
                                 DateValidTo = r.Field<DateTime?>("DocDateValidTo"),
                                 RatingTypes = r.Field<string>("RatingTypes"),
                                 Sector = r.Field<string>("Sector"),
                                 RatingClass = r.Field<string>("RatingClass"),
                                 AuthorizationCode = r.Field<string>("AuthorizationCode"),
                                 AircraftTypeCategory = r.Field<string>("AircraftTypeCategory"),
                                 AircraftTypeGroup = r.Field<string>("AircraftTypeGroup"),
                                 LocationIndicator = r.Field<string>("LocationIndicator"),
                                 RatingLevel = r.Field<string>("RatingLevel")
                             }))
                             .ToList();

            var results = queryResult.Select(q => q.Item2).ToList();
            int count = queryResult.Select(q => q.Item1).FirstOrDefault();
            
            return new Tuple<int, List<PersonReportRatingDO>>(count, results);
        }
    }
}
