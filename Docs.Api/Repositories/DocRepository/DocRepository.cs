using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Docs.Api.Models;
using Common.Api.UserContext;
using Common.Extensions;
using Common.Utils.Expressions;
using Common.Api.Models;
using Docs.Api.Enums;
using System.Data.SqlClient;
using Common.Api.Repositories;
using Common.Utils;
using System.Linq.Expressions;

namespace Docs.Api.Repositories.DocRepository
{
    public class DocRepository : Repository<Doc>, IDocRepository
    {
        //private IUnitOfWork unitOfWork;

        public DocRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            //this.unitOfWork = unitOfWork;
        }

        public string spSetDocUsers(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("DocId", id));

            return this.ExecProcedure<string>("spSetDocUsers", parameters).FirstOrDefault();
        }

        public DocRegister spGetDocRegisterNextNumber(int docRegisterId)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("DocRegisterId", docRegisterId));

            return this.ExecProcedure<DocRegister>("spGetDocRegisterNextNumber", parameters).FirstOrDefault();
        }

        public int? spGetDocRegisterId(int id)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("DocId", id));

            return this.ExecProcedure<int?>("spGetDocRegisterId", parameters).FirstOrDefault();
        }

        public List<DocRelation> GetCaseRelationsByDocId(int id, params Expression<Func<DocRelation, object>>[] includes)
        {
            DocRelation currentRelation = this.unitOfWork.DbContext.Set<DocRelation>()
                .FirstOrDefault(e => e.DocId == id);

            if (currentRelation == null)
            {
                throw new Exception(string.Format("DocRelation is missing for doc ID = {0}", id));
            }

            Tuple<string, object>[] keyValues = new Tuple<string, object>[] { new Tuple<string, object>("RootDocId", currentRelation.RootDocId) };

            return this.FindInStore<DocRelation>(keyValues, includes);
        }

        public List<DocUser> GetActiveDocUsersForDocByUnitId(int docId, UnitUser unitUser)
        {
            return this.unitOfWork.DbContext.Set<DocRelation>()
                .Join(this.unitOfWork.DbContext.Set<DocRelation>(), dr => dr.RootDocId, dr2 => dr2.RootDocId, (dr, dr2) => new { OrgDocId = dr.DocId, DocId = dr2.DocId })
                .Join(this.unitOfWork.DbContext.Set<DocUser>(), dr => dr.DocId, du => du.DocId, (dr, du) => new { OrgDocId = dr.OrgDocId, DocUser = du })
                .Where(e => e.DocUser.UnitId == unitUser.UnitId && e.DocUser.IsActive && e.OrgDocId == docId)
                .Join(this.unitOfWork.DbContext.Set<Doc>(), du => du.OrgDocId, d => d.DocId, (du, d) => du.DocUser)
                .Include(du => du.DocUnitPermission)
                .Where(e => e.IsActive)
                .Distinct()
                .ToList();
        }

        public void RegisterDoc(Doc doc, UnitUser unitUser, UserContext userContext)
        {
            doc.EnsureDocRelationsAreLoaded();

            DateTime currentDate = DateTime.Now;

            DocRegister docRegister = spGetDocRegisterNextNumber(spGetDocRegisterId(doc.DocId).Value);
            RegisterIndex registerIndex = this.unitOfWork.DbContext.Set<RegisterIndex>()
                .SingleOrDefault(e => e.RegisterIndexId == docRegister.RegisterIndexId);

            if (doc.DocRegisterId.HasValue)
            {
                throw new Exception("Document has been already registered.");
            }

            if (registerIndex == null)
            {
                throw new Exception("RegisterIndex can not be found.");
            }

            int? caseId = doc.DocRelations.FirstOrDefault().RootDocId;
            int? caseRegNumber = null;

            if (caseId.HasValue)
            {
                Doc caseDoc = this.unitOfWork.DbContext.Set<Doc>().Find(caseId.Value);
                if (caseDoc != null)
                {
                    caseRegNumber = caseDoc.RegNumber;
                }
            }

            // 0 - doc.RegNumber
            // 1 - doc.RegDate
            // 2 - master doc.RegNumber
            // 3 - doc.ExternalRegNumber
            doc.Register(
                docRegister.DocRegisterId,
                string.Format(registerIndex.NumberFormat, docRegister.CurrentNumber, currentDate, caseRegNumber, doc.ExternalRegNumber),
                registerIndex.Code,
                docRegister.CurrentNumber,
                currentDate,
                userContext);

            DocWorkflowAction docWorkflowAction = this.unitOfWork.DbContext.Set<DocWorkflowAction>()
                .SingleOrDefault(e => e.Alias.ToLower() == "registration");

            doc.CreateDocWorkflow(
                docWorkflowAction,
                currentDate,
                null,
                null,
                null,
                doc.RegUri,
                unitUser.UnitUserId,
                userContext);
        }

        public void GenerateAccessCode(Doc doc, UserContext userContext)
        {
            CodeGenerator codeGenerator = new CodeGenerator();
            codeGenerator.Minimum = 10;
            codeGenerator.Maximum = 10;
            codeGenerator.ConsecutiveCharacters = true;
            codeGenerator.RepeatCharacters = true;

            while (true)
            {
                string code = codeGenerator.Generate();

                if (!this.unitOfWork.DbContext.Set<Doc>().Any(e => e.AccessCode == code))
                {
                    doc.ModifyDate = DateTime.Now;
                    doc.ModifyUserId = userContext.UserId;
                    doc.AccessCode = code;
                    break;
                }
            }
        }

        public Doc CreateDoc(
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
            UserContext userContext)
        {
            DateTime currentDate = DateTime.Now;

            Doc doc = new Doc();
            doc.DocDirectionId = docDirectionId;
            doc.DocEntryTypeId = docEntryTypeId;
            doc.DocStatusId = docStatusId;
            doc.DocSubject = docSubject;
            doc.DocCasePartTypeId = docCasePartTypeId;
            doc.DocDestinationTypeId = docDestinationTypeId;
            doc.DocTypeId = docTypeId;
            doc.DocFormatTypeId = docFormatTypeId;
            doc.DocRegisterId = docRegisterId;
            doc.RegDate = currentDate;

            doc.IsActive = true;
            doc.ModifyDate = currentDate;
            doc.ModifyUserId = userContext.UserId;

            this.unitOfWork.DbContext.Set<Doc>().Add(doc);

            return doc;
        }

        private System.Linq.Expressions.Expression<Func<Doc, bool>> BuildPredicate(
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate,
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            bool? isCase,
            string corrs,
            string units,
            string ds)
        {
            List<int> corrIds = Helper.GetIdListFromString(corrs);
            List<int> unitIds = Helper.GetIdListFromString(units);
            List<int> docIds = Helper.GetIdListFromString(ds);

            if (fromDate.HasValue)
            {
                predicate = predicate.And(d => d.RegDate.HasValue && d.RegDate.Value >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                predicate = predicate.And(d => d.RegDate.HasValue && d.RegDate.Value <= toDate.Value);
            }

            if (!String.IsNullOrWhiteSpace(regUri))
            {
                predicate = predicate.And(d => d.RegUri.Contains(regUri));
            }

            if (!String.IsNullOrWhiteSpace(docName))
            {
                predicate = predicate.And(d => d.DocSubject.Contains(docName));
            }

            if (docTypeId.HasValue)
            {
                predicate = predicate.And(d => d.DocTypeId == docTypeId.Value);
            }

            if (docStatusId.HasValue)
            {
                predicate = predicate.And(d => d.DocStatusId == docStatusId.Value);
            }

            if (isCase.HasValue)
            {
                predicate = predicate.And(d => d.IsCase == isCase.Value);
            }

            if (corrIds.Any())
            {
                predicate = predicate.And(d => d.DocCorrespondents.Any(dc => corrIds.Contains(dc.CorrespondentId)));
            }

            if (unitIds.Any())
            {
                predicate = predicate.And(d => d.DocUnits.Any(du => unitIds.Contains(du.UnitId)));
            }

            if (docIds.Any())
            {
                predicate = predicate.And(d => docIds.Contains(d.DocId));
            }

            return predicate;
        }

        private List<Doc> GetDocsInternal(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            string corrs,
            string units,
            string ds,
            int limit,
            int offset,
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate,
            UnitUser unitUser,
            DocUnitPermission docUnitPermissionRead,
            bool? hideRead,
            bool? isCase,
            out int totalCount
            )
        {
            predicate = BuildPredicate(
                predicate,
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                isCase,
                corrs,
                units,
                ds);

            IQueryable<Doc> query = this.unitOfWork.DbContext.Set<DocRelation>()
                .Join(this.unitOfWork.DbContext.Set<DocRelation>(), dr => dr.RootDocId, dr2 => dr2.RootDocId, (dr, dr2) => new { OrgDocId = dr.DocId, DocId = dr2.DocId })
                .Join(this.unitOfWork.DbContext.Set<DocUser>(), dr => dr.DocId, du => du.DocId, (dr, du) => new { OrgDocId = dr.OrgDocId, DocUser = du })
                .Where(du => du.DocUser.UnitId == unitUser.UnitId && du.DocUser.IsActive && du.DocUser.DocUnitPermissionId == docUnitPermissionRead.DocUnitPermissionId)
                .Join(this.unitOfWork.DbContext.Set<Doc>(), du => du.OrgDocId, d => d.DocId, (du, d) => d)
                .Distinct()
                .AsQueryable();

            if (hideRead.HasValue && hideRead.Value)
            {
                query = query.Where(d => !d.DocUsers.Any(du => du.UnitId == unitUser.UnitId && du.HasRead));
            }

            query = query
                .Where(predicate)
                .OrderByDescending(e => e.RegDate)
                .Take(10000);

            totalCount = query.Count();

            return query
                 .Skip(offset)
                 .Take(limit)
                 .Include(e => e.DocType)
                 .Include(e => e.DocDirection)
                 .Include(e => e.DocStatus)
                 .Include(e => e.DocUnits)
                 .Include(e => e.DocWorkflows)
                 .Include(e => e.DocSourceType)
                 .ToList();
        }

        public List<Doc> GetCurrentCaseDocs(
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
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder.True<Doc>();

            DocStatus docStatusFinished = docStatuses.FirstOrDefault(e => e.Alias == "Finished");
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");

            predicate = predicate.And(e => e.DocStatusId != docStatusFinished.DocStatusId
                && e.DocStatusId != docStatusCanceled.DocStatusId)
                .And(e => e.IsCase);

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                ds,
                limit,
                offset,
                predicate,
                unitUser,
                docUnitPermissionRead,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetFinishedCaseDocs(
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
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder.True<Doc>();

            DocStatus docStatusFinished = docStatuses.FirstOrDefault(e => e.Alias == "Finished");
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");

            predicate = predicate.And(e => e.DocStatusId == docStatusFinished.DocStatusId
                || e.DocStatusId == docStatusCanceled.DocStatusId)
                .And(e => e.IsCase);

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                ds,
                limit,
                offset,
                predicate,
                unitUser,
                docUnitPermissionRead,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetDocsForManagement(
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
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder.True<Doc>();

            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");
            DocStatus docStatusDraft = docStatuses.FirstOrDefault(e => e.Alias == "Draft");

            predicate = predicate.And(e => e.DocStatusId != docStatusCanceled.DocStatusId
                && e.DocStatusId != docStatusDraft.DocStatusId)
                .And(e => e.DocWorkflows.Any(dw => dw.ToUnitId.HasValue && dw.ToUnitId == unitUser.UnitId));

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                ds,
                limit,
                offset,
                predicate,
                unitUser,
                docUnitPermissionRead,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetDocsForControl(
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
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder.True<Doc>();

            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");
            DocStatus docStatusDraft = docStatuses.FirstOrDefault(e => e.Alias == "Draft");

            List<int> docUnitRoleIds = docUnitRoles.Select(e => e.DocUnitRoleId).ToList();

            predicate = predicate.And(e => e.DocStatusId != docStatusCanceled.DocStatusId
                && e.DocStatusId != docStatusDraft.DocStatusId)
                .And(e => e.DocUnits.Any(du => du.UnitId == unitUser.UnitId && docUnitRoleIds.Contains(du.DocUnitRoleId)));

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                ds,
                limit,
                offset,
                predicate,
                unitUser,
                docUnitPermissionRead,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetDraftDocs(
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
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder.True<Doc>();
            DocStatus docStatusDraft = docStatuses.FirstOrDefault(e => e.Alias == "Draft");

            predicate = predicate.And(e => e.DocStatusId == docStatusDraft.DocStatusId);

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                ds,
                limit,
                offset,
                predicate,
                unitUser,
                docUnitPermissionRead,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetUnfinishedDocs(
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
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder.True<Doc>();
            DocStatus docStatusFinished = docStatuses.FirstOrDefault(e => e.Alias == "Finished");
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");

            predicate = predicate.And(e => e.DocStatusId != docStatusFinished.DocStatusId && e.DocStatusId != docStatusCanceled.DocStatusId);

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                ds,
                limit,
                offset,
                predicate,
                unitUser,
                docUnitPermissionRead,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetPortalDocs(
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
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder.True<Doc>();
            DocStatus docStatusFinished = docStatuses.FirstOrDefault(e => e.Alias == "Finished");
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");

            predicate = predicate.And(e => e.DocSourceTypeId == docSourceType.DocSourceTypeId);

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                ds,
                limit,
                offset,
                predicate,
                unitUser,
                docUnitPermissionRead,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetDocs(
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
            out int totalCount)
        {
            List<int> corrIds = Helper.GetIdListFromString(corrs);
            List<int> unitIds = Helper.GetIdListFromString(units);
            List<int> docIds = Helper.GetIdListFromString(ds);

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder.True<Doc>();

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                ds,
                limit,
                offset,
                predicate,
                unitUser,
                docUnitPermissionRead,
                hideRead,
                isCase,
                out totalCount);
        }
    }
}
