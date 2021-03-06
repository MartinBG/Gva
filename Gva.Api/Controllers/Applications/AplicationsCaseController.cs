﻿using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api.DataObjects;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.ModelsDO.Common;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Applications
{
    [RoutePrefix("api/apps/{id:int}")]
    [Authorize]
    public class AplicationsCaseController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IDocRepository docRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public AplicationsCaseController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IDocRepository docRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.docRepository = docRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.userContext = userContext;
        }

        [Route("~/api/apps/parts/{lotId}/{setPartAlias}/new")]
        public IHttpActionResult GetNewPart(int lotId, string setPartAlias, int docId, int caseTypeId, int? docFileId = null)
        {
            var caseType = this.caseTypeRepository.GetCaseType(caseTypeId);

            DocFile docFile = null;
            if (docFileId.HasValue)
            {
                docFile = this.unitOfWork.DbContext.Set<DocFile>()
                    .Include(e => e.DocFileOriginType)
                    .Include(e => e.DocFileKind)
                    .SingleOrDefault(e => e.DocFileId == docFileId);
            }

            if (docFile != null &&
                docFile.DocFileOriginType != null &&
                docFile.DocFileOriginType.Alias == "EApplicationAttachedFile")
            {
                // TODO get part
            }

            var newPart = new PartDO<JObject>()
            {
                Part = new JObject(),
                Case = new DocCaseDO(docFile, caseType)
            };

            return Ok(newPart);
        }

        [Route("parts/{setPartAlias}/linkNew")]
        public IHttpActionResult PostCreatePartAndLink(int id, string setPartAlias, int docId, PartDO<JObject> linkNewPart)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaApplication application = this.applicationRepository.Find(id);
                Lot lot = this.lotRepository.GetLotIndex(application.LotId);

                SetPart setPart = this.unitOfWork.DbContext.Set<SetPart>().FirstOrDefault(e => e.Alias == setPartAlias);
                string path = setPart.PathRegex.Remove(setPart.PathRegex.IndexOf("\\"), 4).Remove(0, 1) + "*";

                PartVersion<JObject> partVersion = lot.CreatePart(path, linkNewPart.Part, this.userContext);
                lot.Commit(this.userContext, lotEventDispatcher);

                DocFile docFile;
                if (linkNewPart.Case.DocFileId.HasValue)
                {
                    docFile = this.unitOfWork.DbContext.Set<DocFile>().FirstOrDefault(e => e.DocFileId == linkNewPart.Case.DocFileId);
                }
                else
                {
                    var doc = this.docRepository.Find(docId);
                    var docFileTypes = this.unitOfWork.DbContext.Set<DocFileType>().ToList();

                    var docFileType = docFileTypes.FirstOrDefault(e => e.Extention == Path.GetExtension(linkNewPart.Case.File.Name));
                    if (docFileType == null)
                    {
                        docFileType = docFileTypes.FirstOrDefault(e => e.Alias == "UnknownBinary");
                    }

                    docFile = doc.CreateDocFile(
                        linkNewPart.Case.DocFileKind.NomValueId,
                        docFileType.DocFileTypeId,
                        null,
                        linkNewPart.Case.Name,
                        linkNewPart.Case.File.Name,
                        String.Empty,
                        linkNewPart.Case.File.Key,
                        true,
                        true,
                        this.userContext);
                }

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = docFile,
                    GvaCaseTypeId = linkNewPart.Case.CaseType.NomValueId,
                    PageIndex = linkNewPart.Case.BookPageNumber,
                    PageIndexInt = this.fileRepository.GetPageIndexInt(linkNewPart.Case.BookPageNumber),
                    PageNumber = linkNewPart.Case.PageCount
                };

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = application,
                    GvaLotFile = lotFile,
                    DocFile = docFile
                };

                this.applicationRepository.AddGvaLotFile(lotFile);
                this.applicationRepository.AddGvaAppLotFile(gvaAppLotFile);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("parts/linkExisting")]
        public IHttpActionResult PostLinkPart(int id, int docFileId, int partId)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaApplication application = this.applicationRepository.Find(id);
                DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().FirstOrDefault(e => e.DocFileId == docFileId);

                GvaLotFile gvaLotFile = this.unitOfWork.DbContext.Set<GvaLotFile>()
                    .Include(e => e.LotPart.SetPart)
                    .FirstOrDefault(e => e.LotPartId == partId);

                if (gvaLotFile != null)
                {
                    GvaAppLotFile gvaAppLotFile = this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                        .Include(e => e.DocFile)
                        .FirstOrDefault(e => e.GvaApplicationId == id && e.GvaLotFileId == gvaLotFile.GvaLotFileId);

                    if (gvaAppLotFile == null)
                    {
                        GvaAppLotFile addGvaAppLotFile = new GvaAppLotFile()
                        {
                            GvaApplication = application,
                            GvaLotFile = gvaLotFile,
                            DocFile = docFile
                        };

                        this.applicationRepository.AddGvaAppLotFile(addGvaAppLotFile);
                    }
                    else if (gvaAppLotFile.DocFile == null)
                    {
                        gvaAppLotFile.DocFile = docFile;
                    }
                }

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("~/api/apps/docFiles/{docId}/new")]
        public IHttpActionResult GetNewDocFile(int docId)
        {
            var docFileKind = this.unitOfWork.DbContext.Set<DocFileKind>()
                .Single(fk => fk.Alias == "PrivateAttachedFile");

            DocFileDO docFile = new DocFileDO()
            {
                IsNew = true,
                DocFileKindId = docFileKind.DocFileKindId,
                File = null
            };

            return Ok(docFile);
        }

        [Route("~/api/apps/docFiles/{docId}/create")]
        public IHttpActionResult PostCreateDocFile(int docId, DocFileDO file)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var doc = this.docRepository.Find(docId);
                var docFileTypes = this.unitOfWork.DbContext.Set<DocFileType>().ToList();

                var docFileType = docFileTypes.FirstOrDefault(e => e.Extention == Path.GetExtension(file.File.Name));
                if (docFileType == null)
                {
                    docFileType = docFileTypes.FirstOrDefault(e => e.Alias == "UnknownBinary");
                }

                doc.CreateDocFile(file.DocFileKindId, docFileType.DocFileTypeId, file.Name, file.File.Name, String.Empty, file.File.Key, this.userContext);

                this.unitOfWork.Save();
                transaction.Commit();
            }

            return Ok();
        }
    }
}