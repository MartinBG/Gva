﻿using Common.Api.UserContext;
using Docs.Api.Enums;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Repositories;
using System.Linq.Expressions;

namespace Docs.Api.Repositories.DocRepository
{
    public interface IDocRepository : IRepository<Doc>
    {
        string spSetDocUsers(int id);

        DocRegister spGetDocRegisterNextNumber(int docRegisterId);

        int? spGetDocRegisterId(int id);

        List<DocRelation> GetCaseRelationsByDocId(int id, params Expression<Func<DocRelation, object>>[] includes);

        List<DocUser> GetActiveDocUsersForDocByUnitId(int docId, UnitUser unitUser);

        void RegisterDoc(Doc doc, UnitUser unitUser, UserContext userContext);

        void GenerateAccessCode(Doc doc, UserContext userContext);

        Doc CreateDoc(
            int docDirectionId,
            int docEntryTypeId,
            int docStatusId,
            string docSubject,
            int? docCasePartTypeId,
            int? docSourceTypeId,
            int? docDestinationTypeId,
            int? docTypeId,
            int? docFormatTypeId,
            int? docRegisterId,
            UserContext userContext); 

        List<Doc> GetCurrentCaseDocs(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? hideRead,
            bool? isCase,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            List<DocStatus> docStatuses,
            DocUnitPermission docUnitPermissionRead,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetFinishedCaseDocs(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? hideRead,
            bool? isCase,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            List<DocStatus> docStatuses,
            DocUnitPermission docUnitPermissionRead,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetDocsForManagement(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? hideRead,
            bool? isCase,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            List<DocStatus> docStatuses,
            DocUnitPermission docUnitPermissionRead,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetDocsForControl(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? hideRead,
            bool? isCase,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            List<DocStatus> docStatuses,
            DocUnitPermission docUnitPermissionRead,
            List<DocUnitRole> docUnitRoles,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetDraftDocs(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? hideRead,
            bool? isCase,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            List<DocStatus> docStatuses,
            DocUnitPermission docUnitPermissionRead,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetUnfinishedDocs(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? hideRead,
            bool? isCase,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            List<DocStatus> docStatuses,
            DocUnitPermission docUnitPermissionRead,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetPortalDocs(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? hideRead,
            bool? isCase,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            List<DocStatus> docStatuses,
            DocUnitPermission docUnitPermissionRead,
            DocSourceType docSourceType,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetDocs(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? hideRead,
            bool? isCase,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            DocUnitPermission docUnitPermissionRead,
            UnitUser unitUser,
            out int totalCount);
    }
}