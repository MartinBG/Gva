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
using Common.Linq;
using Common.Api.Models;
using Docs.Api.Enums;
using System.Data.SqlClient;
using Common.Api.Repositories;
using Common.Utils;
using System.Linq.Expressions;
using System.Data.Entity.Core;

namespace Docs.Api.Repositories.DocRepository
{
    public class DocRepository : Repository<Doc>, IDocRepository
    {
        public DocRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void spSetUnitTokens(int? unitId = null)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("UnitId", Helper.CastToSqlDbValue(unitId)));

            this.ExecuteSqlCommand("spSetUnitTokens @UnitId", parameters);
        }

        public void ExecSpSetDocTokens(int? docId = null)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("DocId", Helper.CastToSqlDbValue(docId)));

            this.ExecuteSqlCommand("spSetDocTokens @DocId", parameters);
        }

        public void ExecSpSetDocUnitTokens(int? docId = null, bool allCase = false)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("DocId", Helper.CastToSqlDbValue(docId)));
            parameters.Add(new SqlParameter("AllCase", Helper.CastToSqlDbValue(allCase)));

            this.ExecuteSqlCommand("spSetDocUnitTokens @DocId, @AllCase", parameters);
        }

        public List<int> fnGetSubordinateDocs(int id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("DocId", id));

            return this.ExecFunction<int>("fnGetSubordinateDocs", parameters);
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

        public int? spGetDocRegisterIdByRegisterIndexId(int registerIndexId)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("RegisterIndexId", registerIndexId));

            return this.ExecProcedure<int?>("spGetDocRegisterIdByRegisterIndexId", parameters).FirstOrDefault();
        }

        public int? GetNextReceiptOrder(int docId)
        {
            Doc doc = this.Find(docId, e => e.DocEntryType);

            if (doc.DocEntryType.Alias != "Document")
            {
                return null;
            }

            int caseId = GetCaseId(docId);

            return (this.unitOfWork.DbContext.Set<DocRelation>()
                .Include(e => e.Doc)
                .Where(e => e.RootDocId == caseId)
                .Max(e => e.Doc.ReceiptOrder) ?? 0) + 1;
        }

        public List<Doc> RearangeReceiptOrder(int inCaseDocId, int boundaryDocId, bool everything = true)
        {
            int boundary = 0;

            if (!everything)
            {
                Doc doc = this.Find(boundaryDocId);

                if (doc.ReceiptOrder.HasValue)
                {
                    boundary = doc.ReceiptOrder.Value;
                }
            }

            int caseId = GetCaseId(inCaseDocId);

            List<Doc> docs = this.unitOfWork.DbContext.Set<DocRelation>()
                .Include(e => e.Doc)
                .Where(e => e.RootDocId == caseId && e.Doc.DocEntryType.Alias == "Document")
                .Where(e => !e.Doc.ReceiptOrder.HasValue || e.Doc.ReceiptOrder.Value > boundary)
                .Select(e => e.Doc)
                .OrderBy(e => e.ReceiptOrder)
                .ToList();

            int counter = 1;

            if (!everything)
            {
                counter = boundary == 0 ? 1 : boundary;
            }

            foreach (var item in docs)
            {
                item.ReceiptOrder = counter;
                counter++;
            }

            return docs;
        }

        public List<Doc> RearangeBoundaryReceiptOrder(int inCaseDocId, int docId, int boundary)
        {
            int caseId = GetCaseId(inCaseDocId);

            List<int> docIds = this.fnGetSubordinateDocs(docId);

            List<Doc> docs = this.unitOfWork.DbContext.Set<DocRelation>()
                .Include(e => e.Doc)
                .Where(e => e.RootDocId == caseId && e.Doc.DocEntryType.Alias == "Document")
                .Where(e => docIds.Contains(e.DocId))
                .Select(e => e.Doc)
                .OrderBy(e => e.ReceiptOrder)
                .ToList();

            foreach (var item in docs)
            {
                item.ReceiptOrder = boundary;
                boundary++;
            }

            return docs;
        }

        public Doc MarkAsRead(int id, byte[] docVersion, int unitId, UserContext userContext)
        {
            Doc doc = this.Find(id, e => e.DocHasReads);

            if (doc == null)
            {
                throw new Exception("Doc not found");
            }

            doc.EnsureForProperVersion(docVersion);

            doc.MarkAsRead(unitId, userContext);

            return doc;
        }

        public Doc MarkAsUnread(int id, byte[] docVersion, int unitId, UserContext userContext)
        {
            Doc doc = this.Find(id, e => e.DocHasReads);

            if (doc == null)
            {
                throw new Exception("Doc not found");
            }

            doc.EnsureForProperVersion(docVersion);

            doc.MarkAsUnread(unitId, userContext);

            return doc;
        }

        public List<DocElectronicServiceStage> GetCaseElectronicServiceStagesByDocId(
            int id,
            params Expression<Func<DocElectronicServiceStage, object>>[] includes)
        {
            int caseId = this.GetCaseId(id);

            Tuple<string, object>[] keyValues = new Tuple<string, object>[] { new Tuple<string, object>("DocId", caseId) };

            return this.FindInStore<DocElectronicServiceStage>(keyValues, includes);
        }

        public Doc NextDocStatus(
            int id,
            byte[] docVersion,
            string targetDocStatusAlias,
            bool forceClosure,
            List<DocStatus> docStatuses,
            List<DocCasePartType> docCasePartTypes,
            int[] checkedIds,
            UserContext userContext,
            out List<DocRelation> docRelations)
        {
            Doc doc = this.Find(id,
                e => e.DocStatus,
                e => e.DocRelations);

            if (doc == null)
            {
                throw new Exception("Doc not found");
            }

            doc.EnsureForProperVersion(docVersion);

            doc.ModifyDate = DateTime.Now;
            doc.ModifyUserId = userContext.UserId;

            docRelations = new List<DocRelation>();
            DocStatus newDocStatus;

            switch (targetDocStatusAlias)
            {
                case "Prepared":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Prepared");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    break;
                case "Processed":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Processed");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    break;
                case "Finished":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Finished");
                    doc.DocStatusId = newDocStatus.DocStatusId;

                    if (doc.IsCase)
                    {
                        int caseDocId = doc.DocRelations.FirstOrDefault().RootDocId.Value;

                        DocCasePartType dcptControl = docCasePartTypes.SingleOrDefault(e => e.Alias == "Control");
                        DocStatus cancelStatus = docStatuses.SingleOrDefault(e => e.Alias == "Canceled");

                        List<DocRelation> caseDocRelations = this.GetCaseRelationsByDocId(caseDocId,
                            e => e.Doc.DocCasePartType,
                            e => e.Doc.DocDirection,
                            e => e.Doc.DocType,
                            e => e.Doc.DocStatus)
                            .Where(e => e.RootDocId == caseDocId
                                && e.Doc.DocId != id
                                && e.Doc.DocCasePartTypeId != dcptControl.DocCasePartTypeId
                                && e.Doc.DocStatusId != newDocStatus.DocStatusId
                                && e.Doc.DocStatusId != cancelStatus.DocStatusId)
                            .ToList();

                        if (caseDocRelations.Any())
                        {
                            if (forceClosure)
                            {
                                foreach (var item in caseDocRelations)
                                {
                                    if (checkedIds.Contains(item.DocId))
                                    {
                                        item.Doc.DocStatusId = newDocStatus.DocStatusId;
                                        item.Doc.DocStatus = newDocStatus;
                                    }
                                }
                            }
                            else
                            {
                                docRelations = caseDocRelations;
                            }
                        }
                    }
                    break;
                case "Draft":

                default:
                    throw new Exception("Unreachable next doc status");
            };

            return doc;
        }

        public Doc NextDocStatus(
            int id,
            byte[] docVersion,
            bool forceClosure,
            List<DocStatus> docStatuses,
            List<DocCasePartType> docCasePartTypes,
            int[] checkedIds,
            UserContext userContext,
            out string targetDocStatusAlias,
            out List<DocRelation> docRelations)
        {
            Doc doc = this.Find(id,
                e => e.DocStatus,
                e => e.DocRelations);

            if (doc == null)
            {
                throw new Exception("Doc not found");
            }

            doc.EnsureForProperVersion(docVersion);

            doc.ModifyDate = DateTime.Now;
            doc.ModifyUserId = userContext.UserId;

            docRelations = new List<DocRelation>();
            DocStatus newDocStatus;

            switch (doc.DocStatus.Alias)
            {
                case "Draft":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Prepared");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    targetDocStatusAlias = "Prepared";
                    break;
                case "Prepared":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Processed");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    targetDocStatusAlias = "Processed";
                    break;
                case "Processed":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Finished");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    targetDocStatusAlias = "Finished";

                    if (doc.IsCase)
                    {
                        int caseDocId = doc.DocRelations.FirstOrDefault().RootDocId.Value;

                        DocCasePartType dcptControl = docCasePartTypes.SingleOrDefault(e => e.Alias == "Control");
                        DocStatus cancelStatus = docStatuses.SingleOrDefault(e => e.Alias == "Canceled");

                        List<DocRelation> caseDocRelations = this.GetCaseRelationsByDocId(caseDocId,
                            e => e.Doc.DocCasePartType,
                            e => e.Doc.DocDirection,
                            e => e.Doc.DocType,
                            e => e.Doc.DocStatus)
                            .Where(e => e.RootDocId == caseDocId
                                && e.Doc.DocId != id
                                && e.Doc.DocCasePartTypeId != dcptControl.DocCasePartTypeId
                                && e.Doc.DocStatusId != newDocStatus.DocStatusId
                                && e.Doc.DocStatusId != cancelStatus.DocStatusId)
                            .ToList();

                        if (caseDocRelations.Any())
                        {
                            if (forceClosure)
                            {
                                foreach (var item in caseDocRelations)
                                {
                                    if (checkedIds.Contains(item.DocId))
                                    {
                                        item.Doc.DocStatusId = newDocStatus.DocStatusId;
                                        item.Doc.DocStatus = newDocStatus;
                                    }
                                }
                            }
                            else
                            {
                                docRelations = caseDocRelations;
                            }
                        }
                    }
                    break;
                case "Finished":
                default:
                    throw new Exception("Unreachable next doc status");
            };

            return doc;
        }

        public Doc ReverseDocStatus(
            int id,
            byte[] docVersion,
            string targetDocStatusAlias,
            List<DocStatus> docStatuses,
            UserContext userContext)
        {
            Doc doc = this.Find(id,
                e => e.DocStatus,
                e => e.DocRelations);

            if (doc == null)
            {
                throw new Exception("Doc not found");
            }

            doc.EnsureForProperVersion(docVersion);

            doc.ModifyDate = DateTime.Now;
            doc.ModifyUserId = userContext.UserId;

            DocStatus newDocStatus;

            switch (targetDocStatusAlias)
            {
                case "Draft":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Draft");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    break;
                case "Prepared":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Prepared");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    break;
                case "Processed":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Processed");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    break;
                default:
                    throw new Exception("Unreachable previous doc status");
            };

            return doc;
        }

        public Doc ReverseDocStatus(
            int id,
            byte[] docVersion,
            List<DocStatus> docStatuses,
            UserContext userContext)
        {
            Doc doc = this.Find(id,
                e => e.DocStatus,
                e => e.DocRelations);

            if (doc == null)
            {
                throw new Exception("Doc not found");
            }

            doc.EnsureForProperVersion(docVersion);

            doc.ModifyDate = DateTime.Now;
            doc.ModifyUserId = userContext.UserId;

            DocStatus newDocStatus;

            switch (doc.DocStatus.Alias)
            {
                case "Prepared":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Draft");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    break;
                case "Processed":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Prepared");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    break;
                case "Finished":
                case "Canceled":
                    newDocStatus = docStatuses.SingleOrDefault(e => e.Alias == "Processed");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    break;
                case "Draft":
                default:
                    throw new Exception("Unreachable previous doc status");
            };

            return doc;
        }

        public Doc CancelDoc(int id, byte[] docVersion, DocStatus cancelDocStatus, UserContext userContext)
        {
            Doc doc = this.Find(id,
                e => e.DocStatus,
                e => e.DocRelations);

            if (doc == null)
            {
                throw new Exception("Doc not found");
            }

            doc.EnsureForProperVersion(docVersion);

            doc.ModifyDate = DateTime.Now;
            doc.ModifyUserId = userContext.UserId;
            doc.DocStatusId = cancelDocStatus.DocStatusId;

            return doc;
        }

        public Doc UpdateDocCasePartType(int id, byte[] docVersion, int docCasePartTypeId, UserContext userContext)
        {
            Doc doc = this.Find(id);

            if (doc == null)
            {
                throw new Exception("Doc not found");
            }

            doc.EnsureForProperVersion(docVersion);

            doc.DocCasePartTypeId = docCasePartTypeId;
            doc.ModifyDate = DateTime.Now;
            doc.ModifyUserId = userContext.UserId;

            doc.CreateDocCasePartMovement(docCasePartTypeId, userContext);

            return doc;
        }

        public List<DocRelation> GetCaseRelationsByDocId(int id, params Expression<Func<DocRelation, object>>[] includes)
        {
            int caseId = this.GetCaseId(id);

            Tuple<string, object>[] keyValues = new Tuple<string, object>[] { new Tuple<string, object>("RootDocId", caseId) };

            return this.FindInStore<DocRelation>(keyValues, includes);
        }

        public int GetCaseId(int id)
        {
            DocRelation currentRelation = this.unitOfWork.DbContext.Set<DocRelation>()
                .FirstOrDefault(e => e.DocId == id);

            if (currentRelation == null)
            {
                throw new Exception(string.Format("DocRelation is missing for doc ID = {0}", id));
            }

            if (!currentRelation.RootDocId.HasValue)
            {
                throw new Exception(string.Format("DocRelation is missing RootDocId for doc ID = {0}", id));
            }

            return currentRelation.RootDocId.Value;
        }

        public List<vwDocUser> GetvwDocUsersForDocByUnitId(int docId, UnitUser unitUser)
        {
            return this.unitOfWork.DbContext.Set<vwDocUser>()
                .Include(e => e.ClassificationPermission)
                .Where(e => e.UnitId == unitUser.UnitId && e.DocId == docId)
                .ToList();
        }

        public string RegisterDoc(
            Doc doc,
            UnitUser unitUser,
            UserContext userContext,
            bool checkVersion = false,
            byte[] docVersion = null)
        {
            doc.EnsureDocRelationsAreLoaded();

            if (checkVersion)
            {
                doc.EnsureForProperVersion(docVersion);
            }

            DateTime currentDate = DateTime.Now;

            DocRegister docRegister;

            if (doc.DocRegisterId.HasValue)
            {
                docRegister = spGetDocRegisterNextNumber(doc.DocRegisterId.Value);
            }
            else
            {
                docRegister = spGetDocRegisterNextNumber(spGetDocRegisterId(doc.DocId).Value);
            }

            RegisterIndex registerIndex = this.unitOfWork.DbContext.Set<RegisterIndex>()
                .SingleOrDefault(e => e.RegisterIndexId == docRegister.RegisterIndexId);

            if (!string.IsNullOrEmpty(doc.RegUri))
            {
                throw new Exception("Document has been already registered.");
            }

            if (registerIndex == null)
            {
                throw new Exception("RegisterIndex can not be found.");
            }

            if (registerIndex.Alias == "Manual")
            {
                return "Manual";
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
                .SingleOrDefault(e => e.Alias.ToLower() == "Registration".ToLower());

            doc.CreateDocWorkflow(
                docWorkflowAction,
                currentDate,
                null,
                null,
                unitUser.UnitId,
                doc.RegUri,
                unitUser.UnitUserId,
                userContext);

            return string.Empty;
        }

        public string ManualRegisterDoc(
            Doc doc,
            UnitUser unitUser,
            UserContext userContext,
            string regUri,
            DateTime regDate,
            bool checkVersion = false,
            byte[] docVersion = null)
        {
            doc.EnsureDocRelationsAreLoaded();

            if (checkVersion)
            {
                doc.EnsureForProperVersion(docVersion);
            }

            DateTime currentDate = DateTime.Now;

            DocRegister docRegister;

            if (doc.DocRegisterId.HasValue)
            {
                docRegister = spGetDocRegisterNextNumber(doc.DocRegisterId.Value);
            }
            else
            {
                docRegister = spGetDocRegisterNextNumber(spGetDocRegisterId(doc.DocId).Value);
            }

            RegisterIndex registerIndex = this.unitOfWork.DbContext.Set<RegisterIndex>()
                .SingleOrDefault(e => e.RegisterIndexId == docRegister.RegisterIndexId);

            if (!string.IsNullOrEmpty(doc.RegUri))
            {
                throw new Exception("Document has been already registered.");
            }

            if (registerIndex == null)
            {
                throw new Exception("RegisterIndex can not be found.");
            }

            if (registerIndex.Alias != "Manual")
            {
                throw new Exception("RegisterIndex is not used.");
            }

            doc.ManualRegister(
                docRegister.DocRegisterId,
                registerIndex.Code,
                regUri,
                regDate,
                userContext);

            DocWorkflowAction docWorkflowAction = this.unitOfWork.DbContext.Set<DocWorkflowAction>()
                .SingleOrDefault(e => e.Alias.ToLower() == "Registration".ToLower());

            doc.CreateDocWorkflow(
                docWorkflowAction,
                currentDate,
                null,
                null,
                unitUser.UnitId,
                doc.RegUri,
                unitUser.UnitUserId,
                userContext);

            return string.Empty;
        }

        public string IncomingRegisterDoc(
            Doc doc,
            UnitUser unitUser,
            UserContext userContext,
            string regIndex,
            int regNumber,
            DateTime regDate,
            bool checkVersion = false,
            byte[] docVersion = null)
        {
            doc.EnsureDocRelationsAreLoaded();

            if (checkVersion)
            {
                doc.EnsureForProperVersion(docVersion);
            }

            DateTime currentDate = DateTime.Now;

            RegisterIndex registerIndex = this.unitOfWork.DbContext.Set<RegisterIndex>()
               .SingleOrDefault(e => e.Alias == "Incoming");

            var docRegisterId = this.spGetDocRegisterIdByRegisterIndexId(registerIndex.RegisterIndexId);

            if (!string.IsNullOrEmpty(doc.RegUri))
            {
                throw new Exception("Document has been already registered.");
            }

            if (registerIndex == null)
            {
                throw new Exception("RegisterIndex can not be found.");
            }

            doc.Register(
                docRegisterId.Value,
                String.Format("{0}-{1}-{2}", regIndex, regNumber.ToString(), regDate.ToString("dd.MM.yyyy")),
                regIndex,
                regNumber,
                regDate,
                userContext);

            DocWorkflowAction docWorkflowAction = this.unitOfWork.DbContext.Set<DocWorkflowAction>()
                .SingleOrDefault(e => e.Alias.ToLower() == "Registration".ToLower());

            doc.CreateDocWorkflow(
                docWorkflowAction,
                currentDate,
                null,
                null,
                unitUser.UnitId,
                doc.RegUri,
                unitUser.UnitUserId,
                userContext);

            return string.Empty;
        }

        public void GenerateAccessCode(Doc doc, UserContext userContext)
        {
            CodeGeneratorUtils codeGenerator = new CodeGeneratorUtils();
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

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.RegDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.RegDate, toDate)
                .AndStringContains(e => e.RegUri, regUri)
                .AndStringContains(e => e.DocSubject, docName)
                .AndEquals(e => e.DocTypeId.Value, docTypeId)
                .AndEquals(e => e.DocStatusId, docStatusId)
                .AndEquals(e => e.IsCase, isCase);

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
            ClassificationPermission readPermission,
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

            if (hideRead.HasValue && hideRead.Value)
            {
                predicate = predicate.And(e => !e.DocHasReads.Any(d => d.UnitId == unitUser.UnitId && d.HasRead));
            }

            IQueryable<Doc> query =
                from d in this.unitOfWork.DbContext.Set<Doc>()
                join v in this.unitOfWork.DbContext.Set<vwDocUser>() on d.DocId equals v.DocId
                where v.ClassificationPermissionId == readPermission.ClassificationPermissionId && v.UnitId == unitUser.UnitId
                select d;

            query = query.Where(predicate);

            totalCount = query.Count();

            return query
                 .OrderByDescending(e => e.RegDate)
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount)
        {
            DocStatus docStatusFinished = docStatuses.FirstOrDefault(e => e.Alias == "Finished");
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.IsCase)
                .And(e => e.DocStatusId != docStatusFinished.DocStatusId)
                .And(e => e.DocStatusId != docStatusCanceled.DocStatusId)
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId);

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
                readPermission,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetCurrentExclusiveCaseDocs(
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
            List<int> excludedDocId,
            int limit,
            int offset,
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount)
        {
            DocStatus docStatusFinished = docStatuses.FirstOrDefault(e => e.Alias == "Finished");
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.IsCase)
                .And(e => e.DocStatusId != docStatusFinished.DocStatusId)
                .And(e => e.DocStatusId != docStatusCanceled.DocStatusId)
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId)
                .And(e => !excludedDocId.Contains(e.DocId)); //? maybe written with a join to gvaapplications and move this method to specific repo

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
                readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount)
        {
            DocStatus docStatusFinished = docStatuses.FirstOrDefault(e => e.Alias == "Finished");
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.IsCase)
                .And(e => e.DocStatusId == docStatusFinished.DocStatusId || e.DocStatusId == docStatusCanceled.DocStatusId)
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId);

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
                readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount)
        {
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");
            DocStatus docStatusDraft = docStatuses.FirstOrDefault(e => e.Alias == "Draft");

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.DocStatusId != docStatusCanceled.DocStatusId)
                .And(e => e.DocStatusId != docStatusDraft.DocStatusId)
                .And(e => e.DocWorkflows.Any(dw => dw.ToUnitId.HasValue && dw.ToUnitId == unitUser.UnitId))
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId);

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
                readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
            List<DocUnitRole> docUnitRoles,
            UnitUser unitUser,
            out int totalCount)
        {
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");
            DocStatus docStatusDraft = docStatuses.FirstOrDefault(e => e.Alias == "Draft");

            List<int> docUnitRoleIds = docUnitRoles.Select(e => e.DocUnitRoleId).ToList();

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.DocStatusId != docStatusCanceled.DocStatusId)
                .And(e => e.DocStatusId != docStatusDraft.DocStatusId)
                .And(e => e.DocUnits.Any(du => du.UnitId == unitUser.UnitId && docUnitRoleIds.Contains(du.DocUnitRoleId)))
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId);

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
                readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount)
        {
            DocStatus docStatusDraft = docStatuses.FirstOrDefault(e => e.Alias == "Draft");

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.DocStatusId == docStatusDraft.DocStatusId)
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId);

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
                readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount)
        {
            DocStatus docStatusFinished = docStatuses.FirstOrDefault(e => e.Alias == "Finished");
            DocStatus docStatusCanceled = docStatuses.FirstOrDefault(e => e.Alias == "Canceled");

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.DocStatusId != docStatusFinished.DocStatusId)
                .And(e => e.DocStatusId != docStatusCanceled.DocStatusId)
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId);

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
                readPermission,
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
            DocCasePartType docCasePartType,
            List<DocStatus> docStatuses,
            ClassificationPermission readPermission,
            DocSourceType docSourceType,
            UnitUser unitUser,
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.DocSourceTypeId == docSourceType.DocSourceTypeId)
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId);

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
                readPermission,
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
            DocCasePartType docCasePartType,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount)
        {
            List<int> corrIds = Helper.GetIdListFromString(corrs);
            List<int> unitIds = Helper.GetIdListFromString(units);
            List<int> docIds = Helper.GetIdListFromString(ds);

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId);

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
                readPermission,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetDocsExclusive(
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
            List<int> excludedDocIds,
            int limit,
            int offset,
            DocCasePartType docCasePartType,
            ClassificationPermission readPermission,
            UnitUser unitUser,
            out int totalCount)
        {
            List<int> corrIds = Helper.GetIdListFromString(corrs);
            List<int> unitIds = Helper.GetIdListFromString(units);
            List<int> docIds = Helper.GetIdListFromString(ds);

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.DocCasePartTypeId != docCasePartType.DocCasePartTypeId)
                .And(e => !excludedDocIds.Contains(e.DocId)); //? maybe written with a join to aopapplications and move this method to specific repo

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
                readPermission,
                hideRead,
                isCase,
                out totalCount);
        }

        public List<Doc> GetDocsForChange(
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
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>();

            if (docRelations.Any())
            {
                predicate = predicate.And(d => !docRelations.Contains(d.DocId));
            }

            return GetDocsInternal(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                corrs,
                units,
                null,
                limit,
                offset,
                predicate,
                unitUser,
                readPermission,
                null,
                null,
                out totalCount);
        }

        public List<Doc> GetControlDocs(
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
            out int totalCount)
        {
            List<int> corrIds = Helper.GetIdListFromString(corrs);
            List<int> unitIds = Helper.GetIdListFromString(units);
            List<int> docIds = Helper.GetIdListFromString(ds);

            System.Linq.Expressions.Expression<Func<Doc, bool>> predicate = PredicateBuilder
                .True<Doc>()
                .And(e => e.DocCasePartTypeId == docCasePartType.DocCasePartTypeId);

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
                readPermission,
                hideRead,
                isCase,
                out totalCount);
        }

        public IncomingDoc GetIncomingDocByDocumentGuid(Guid documentGuid)
        {
            return
                this.unitOfWork.DbContext.Set<IncomingDoc>()
                .Include(id => id.IncomingDocFiles)
                .Include(id => id.IncomingDocStatus)
                .Include(id => id.DocIncomingDocs)
                .FirstOrDefault(id => id.DocumentGuid == documentGuid);
        }

        public Doc GetDocByRegUri(string regIndex, int regNumber, DateTime regDate)
        {
            var docs =
                this.unitOfWork.DbContext.Set<Doc>()
                .Include(d => d.DocStatus)
                .Include(d => d.DocElectronicServiceStages)
                .Where(d => d.RegIndex == regIndex && d.RegNumber == regNumber)
                .ToList();

            return docs.Where(d => d.RegDate.Value.Date == regDate.Date).SingleOrDefault();
        }

        public Doc GetDocByRegUri(string regUri)
        {
            return
                this.unitOfWork.DbContext.Set<Doc>()
                .Include(d => d.DocType)
                .Include(d => d.DocCasePartType)
                .Include(d => d.DocStatus)
                .Include(d => d.DocElectronicServiceStages)
                .Where(d => d.RegUri == regUri)
                .SingleOrDefault();
        }

        public Doc GetByRegUriAndAccessCode(string regIndex, int regNumber, DateTime regDate, string accessCode)
        {
            var docs =
                this.unitOfWork.DbContext.Set<Doc>()
                .Include(d => d.DocType)
                .Include(d => d.DocCasePartType)
                .Where(d => d.RegIndex == regIndex && d.RegNumber == regNumber && d.AccessCode == accessCode)
                .ToList();

            return docs.Where(d => d.RegDate.Value.Date == regDate.Date).SingleOrDefault();
        }

        public DocFile GetPrimaryOrFirstDocFileByDocId(int docId)
        {
            return this.unitOfWork.DbContext.Set<DocFile>()
                .Include(d => d.DocFileKind)
                .Where(d => d.DocId == docId)
                .OrderByDescending(d => d.IsPrimary)
                .ThenBy(d => d.DocFileId)
                .FirstOrDefault();
        }

        public DocElectronicServiceStage GetCurrentServiceStageByDocId(int docId)
        {
            return this.unitOfWork.DbContext.Set<DocElectronicServiceStage>()
                .Include(s => s.ElectronicServiceStage.ElectronicServiceStageExecutors)
                .FirstOrDefault(s => s.DocId == docId && s.IsCurrentStage);
        }

        public bool CheckForExistingAccessCode(string accessCode)
        {
            return this.unitOfWork.DbContext.Set<Doc>()
                .Where(d => d.AccessCode == accessCode)
                .Any();
        }

        public Doc GetDocByRegUriIncludeElectronicServiceStages(string regIndex, int regNumber, DateTime regDate)
        {
            var docs =
                this.unitOfWork.DbContext.Set<Doc>()
                .Include(d => d.DocStatus)
                .Include(d => d.DocSourceType)
                .Include(d => d.DocType)
                .Include(d => d.DocElectronicServiceStages)
                .Where(d => d.RegIndex == regIndex && d.RegNumber == regNumber)
                .ToList();

            return docs.Where(d => d.RegDate.Value.Date == regDate.Date).SingleOrDefault();
        }

        public Tuple<string, string> GetPositionAndNameById(int unitId)
        {
            string position = String.Empty;
            string name = String.Empty;

            var unit =
                this.unitOfWork.DbContext.Set<Unit>()
                .Include(u => u.UnitType)
                .Include(u => u.UnitRelations.Select(ur => ur.ParentUnit))
                .FirstOrDefault(u => u.UnitId == unitId);

            if (unit != null)
            {
                if (unit.UnitType.Alias.ToLower() == "department" || unit.UnitType.Alias.ToLower() == "position")
                {
                    position = unit.Name;
                }
                else //unit.UnitType.Alias.ToLower() == "employee"
                {
                    name = unit.Name;

                    if (unit.UnitRelations.Count > 0)
                    {
                        var parentUnit = unit.UnitRelations.First().ParentUnit;

                        position = parentUnit.Name;
                    }
                }
            }

            return new Tuple<string, string>(position, name);
        }

        public List<Doc> FindPublicLeafsByDocId(int docId)
        {
            var returnValue = new List<Doc>();

            returnValue.AddRange(
                this.unitOfWork.DbContext.Set<DocRelation>()
                .Where(d =>
                    d.DocId == docId &&
                    d.Doc.RegUri != null &&
                    d.Doc.DocCasePartType.Alias == "Public" &&
                    d.Doc.DocStatus.Alias != "Draft" &&
                    d.Doc.DocStatus.Alias != "Canceled")
                .Select(d => d.Doc)
                .Include(d => d.DocType)
                .Include(d => d.DocWorkflows.Select(dw => dw.DocWorkflowAction))
                .Include(d => d.DocCasePartType)
                .ToList());

            if (returnValue.Count == 0)
            {
                return returnValue;
            }

            int caseDocTypeId = returnValue[0].DocTypeId.Value;

            var parentIds = new List<int>();
            parentIds.Add(docId);

            while (parentIds.Count > 0)
            {
                var docs =
                    this.unitOfWork.DbContext.Set<DocRelation>()
                    .Include(d => d.Doc.DocCasePartType)
                    .Where(d =>
                        d.ParentDocId.HasValue &&
                        parentIds.Contains(d.ParentDocId.Value) &&
                        d.Doc.RegUri != null &&
                        d.Doc.DocCasePartType.Alias == "Public" &&
                        d.Doc.DocStatus.Alias != "Draft" &&
                        d.Doc.DocStatus.Alias != "Canceled")
                    .Select(d => d.Doc)
                    .Include(d => d.DocType)
                    .Include(d => d.DocWorkflows.Select(dw => dw.DocWorkflowAction))
                    .Include(d => d.DocCasePartType)
                    .ToList();

                returnValue.AddRange(docs);

                parentIds = new List<int>();
                parentIds.AddRange(docs.Select(d => d.DocId));
            }

            return returnValue;
        }

        public List<DocRelation> GetCaseRelationsByDocIdWithIncludes(int id, bool includeCasePartMovements, bool includeDocFiles = false)
        {
            int caseId = this.GetCaseId(id);

            var docRelationsSet = this.unitOfWork.DbContext.Set<DocRelation>()
                .Include(e => e.Doc.DocCasePartType)
                .Include(e => e.Doc.DocDirection)
                .Include(e => e.Doc.DocType)
                .Include(e => e.Doc.DocStatus);

            if (includeCasePartMovements)
            {
                docRelationsSet = docRelationsSet.Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User));
            }

            if (includeDocFiles)
            {
                docRelationsSet = docRelationsSet.Include(e => e.Doc.DocFiles.Select(df => df.DocFileOriginType));
            }

            return
                docRelationsSet
                .Where(dr => dr.RootDocId == caseId)
                .ToList();
        }

    }
}
