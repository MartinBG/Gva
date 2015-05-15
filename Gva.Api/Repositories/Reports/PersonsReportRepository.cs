using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Api.Repositories.NomRepository;
using Gva.Api.CommonUtils;
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

        public List<PersonReportDocumentDO> GetDocuments(
            SqlConnection conn,
            string documentRole = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? typeId = null,
            int? lin = null)
        {
            return conn.CreateStoreCommand(
                    @"SELECT
                        p.LotId,
                        p.Lin,
                        ii.Name,
                        nv.Name as Type,
                        ii.Number,
                        ii.FromDate,
                        ii.Date,
                        ii.ToDate,
                        ii.Publisher,
                        ii.Valid
                    FROM 
                    GvaViewInventoryItems ii
                    INNER JOIN GvaViewPersons p ON ii.LotId = p.LotId
                    LEFT JOIN NomValues nv ON nv.NomValueId = ii.TypeId
                    WHERE 1=1 {0} {1} {2} {3} {4}
                    ORDER BY ii.FromDate DESC",
                    new DbClause("and ii.FromDate >= {0}", fromDate),
                    new DbClause("and ii.ToDate <= {0}", toDate),
                    new DbClause("and p.Lin = {0}", lin),
                    new DbClause("and nv.NomValueId = {0}", typeId),
                    new DbClause("and ii.Name = {0}", documentRole))
                .Materialize(r =>
                        new PersonReportDocumentDO()
                        {
                            LotId = r.Field<int>("LotId"),
                            Lin = r.Field<int?>("Lin"),
                            Name = r.Field<string>("Name"),
                            Type = r.Field<string>("Type"),
                            Number = r.Field<string>("Number"),
                            FromDate = r.Field<DateTime?>("FromDate") ?? r.Field<DateTime?>("Date"),
                            ToDate = r.Field<DateTime?>("ToDate"),
                            Valid = r.Field<bool?>("Valid"),
                            Publisher = r.Field<string>("Publisher")
                        })
                .ToList();
        }

        public List<PersonReportLicenceDO> GetLicences(
            SqlConnection conn,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null,
            int? limitationId = null)
        {
            var result = conn.CreateStoreCommand(
                        @"SELECT
                             p.LotId,
                             p.Lin,
                             p.Uin AS uin,
                             p.Names,
                             lt.Name AS LicenceTypeName,
                             l.PublisherCode + ' ' + l.LicenceTypeCaCode + ' ' + RIGHT('00000'+CAST(l.LicenceNumber AS NVARCHAR(5)),5) AS LicenceCode,
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
                         WHERE 1=1 {0} {1} {2} {3} {4}
                         ORDER BY le.DateValidFrom DESC",
                        new DbClause("and le.DateValidFrom >= {0}", fromDate),
                        new DbClause("and le.DateValidTo <= {0}", toDate),
                        new DbClause("and p.Lin = {0}", lin),
                        new DbClause("and lt.NomValueId = {0}", licenceTypeId),
                        new DbClause("and la.NomValueId = {0}", licenceActionId))
                    .Materialize(r =>
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
                                Limitations = r.Field<string>("limitations")
                            })
                    .ToList();

            if (limitationId.HasValue)
            {
                string limName = this.nomRepository.GetNomValue(limitationId.Value).Name;
                result = result
                    .Where(r => !string.IsNullOrEmpty(r.Limitations) && Regex.Split(r.Limitations, ", ").Any(l => l == limName))
                    .ToList();
            }

            return result;
        }

        public List<PersonReportRatingDO> GetRatings(
            SqlConnection conn,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null,
            int? limitationId = null)
        {
            var result = conn.CreateStoreCommand(@"
                         SELECT 
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
                        WHERE 1=1 {0} {1} {2} {3} {4}
                        ORDER BY re.DocDateValidFrom DESC",
                        new DbClause("and re.DocDateValidFrom >= {0}", fromDate),
                        new DbClause("and re.DocDateValidTo <= {0}", toDate),
                        new DbClause("and p.Lin = {0}", lin),
                        new DbClause("and r.RatingClassId = {0}", ratingClassId),
                        new DbClause("and r.AuthorizationId = {0}", authorizationId),
                        new DbClause("and r.AircraftTypeGroupId = {0}", aircraftTypeCategoryId))
                    .Materialize(r =>
                            new PersonReportRatingDO()
                            {
                                Lin = r.Field<int?>("lin"),
                                LotId = r.Field<int>("lotId"),
                                RatingSubClasses = r.Field<string>("RatingSubClasses"),
                                Limitations = r.Field<string>("Limitations"),
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
                            })
                    .ToList();

            if (limitationId.HasValue)
            {
                string limCode = this.nomRepository.GetNomValue(limitationId.Value).Code;
                result = result
                    .Where(r => !string.IsNullOrEmpty(r.Limitations) && Regex.Split(r.Limitations, ", ").Any(l => l == limCode))
                    .ToList();
            }

            return result;
        }
    }
}
