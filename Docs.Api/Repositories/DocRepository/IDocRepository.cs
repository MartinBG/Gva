using Common.Api.UserContext;
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
using Common.Api.Models;

namespace Docs.Api.Repositories.DocRepository
{
    public interface IDocRepository : IRepository<Doc>
    {
        void spSetUnitTokens(int? unitId = null);

        void ExecSpSetDocTokens(int? docId = null);

        void ExecSpSetDocUnitTokens(int? docId = null, bool allCase = false);

        List<int> fnGetSubordinateDocs(int id);

        DocRegister spGetDocRegisterNextNumber(int docRegisterId);

        int? spGetDocRegisterId(int id);

        int? spGetDocRegisterIdByRegisterIndexId(int registerIndexId);

        int? GetNextReceiptOrder(int docId);

        List<Doc> RearangeReceiptOrder(int inCaseDocId, int boundaryDocId, bool everything = true);

        List<Doc> RearangeBoundaryReceiptOrder(int inCaseDocId, int boundary);

        Doc MarkAsRead(int id, byte[] docVersion, int unitId, UserContext userContext);

        Doc MarkAsUnread(int id, byte[] docVersion, int unitId, UserContext userContext);

        List<DocElectronicServiceStage> GetCaseElectronicServiceStagesByDocId(
            int id,
            params Expression<Func<DocElectronicServiceStage, object>>[] includes);

        Doc NextDocStatus(
            int id,
            byte[] docVersion,
            string targetDocStatusAlias,
            bool forceClosure,
            List<DocStatus> docStatuses,
            List<DocCasePartType> docCasePartTypes,
            int[] checkedIds,
            UserContext userContext,
            out List<DocRelation> docRelations);

        Doc NextDocStatus(
            int id,
            byte[] docVersion,
            bool forceClosure,
            List<DocStatus> docStatuses,
            List<DocCasePartType> docCasePartTypes,
            int[] checkedIds,
            UserContext userContext,
            out string targetDocStatusAlias,
            out List<DocRelation> docRelations);

        Doc ReverseDocStatus(int id, byte[] docVersion, string targetDocStatusAlias, List<DocStatus> docStatuses, UserContext userContext);

        Doc ReverseDocStatus(int id, byte[] docVersion, List<DocStatus> docStatuses, UserContext userContext);

        Doc CancelDoc(int id, byte[] docVersion, DocStatus cancelDocStatus, UserContext userContext);

        Doc UpdateDocCasePartType(int id, byte[] docVersion, int docCasePartTypeId, UserContext userContext);

        List<DocRelation> GetCaseRelationsByDocId(int id, params Expression<Func<DocRelation, object>>[] includes);

        int GetCaseId(int id);

        List<vwDocUser> GetvwDocUsersForDocByUnitId(int docId, UnitUser unitUser);

        string RegisterDoc(
            Doc doc,
            UnitUser unitUser,
            UserContext userContext,
            bool checkVersion = false,
            byte[] docVersion = null);

        string ManualRegisterDoc(
            Doc doc,
            UnitUser unitUser,
            UserContext userContext,
            string regUri,
            DateTime regDate,
            bool checkVersion = false,
            byte[] docVersion = null);

        string IncomingRegisterDoc(
            Doc doc,
            UnitUser unitUser,
            UserContext userContext,
            string regIndex,
            int regNumber,
            DateTime regDate,
            bool checkVersion = false,
            byte[] docVersion = null);

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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
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
            DocCasePartType docCasePartType,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetDocsForChange(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            string corrs,
            string units,
            List<int> docRelations,
            int limit,
            int offset,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount);

        List<Doc> GetControlDocs(
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
            DocCasePartType docCasePartType,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount);

        IncomingDoc GetIncomingDocByDocumentGuid(Guid documentGuid);

        Doc GetDocByRegUri(string regIndex, int regNumber, DateTime regDate);

        Doc GetDocByRegUri(string regUri);

        Doc GetByRegUriAndAccessCode(string regIndex, int regNumber, DateTime regDate, string accessCode);

        DocFile GetPrimaryOrFirstDocFileByDocId(int docId);

        DocElectronicServiceStage GetCurrentServiceStageByDocId(int docId);

        bool CheckForExistingAccessCode(string accessCode);

        Doc GetDocByRegUriIncludeElectronicServiceStages(string regIndex, int regNumber, DateTime regDate);

        Tuple<string, string> GetPositionAndNameById(int unitId);

        List<Doc> FindPublicLeafsByDocId(int docId);
    }
}
