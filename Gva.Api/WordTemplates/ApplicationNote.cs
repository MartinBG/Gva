﻿using System;
using System.Data.Entity;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using System.Linq;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class ApplicationNote : IDataGenerator
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private INomRepository nomRepository;

        public ApplicationNote(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.nomRepository = nomRepository;
        }

        public string GeneratorCode
        {
            get
            {
                return "applicationNote";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Бележка за заявление";
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            PersonDataDO personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            var application = this.lotRepository.GetLotIndex(lotId).Index.GetPart<DocumentApplicationDO>(path);
            int documentDuration = this.nomRepository.GetNomValue(application.Content.ApplicationType.NomValueId).TextContent.Get<int?>("duration") ?? 30;
            string applicant = string.Format("{0}, {1} {2} {3}", personData.Lin, personData.FirstName, personData.MiddleName, personData.LastName);
            string documentNumber = !string.IsNullOrEmpty(application.Content.OldDocumentNumber) ?  string.Format("{0}/{1:dd.MM.yyyy}", application.Content.OldDocumentNumber, application.Content.DocumentDate) : application.Content.DocumentNumber;
            var termDate = application.Content.DocumentDate.HasValue? application.Content.DocumentDate.Value.AddDays(documentDuration) : DateTime.Now.AddDays(documentDuration);
            string caseType = this.fileRepository.GetFileReference(application.PartId, null).GvaCaseType.Name;

            var gvaApp = this.unitOfWork.DbContext.Set<GvaApplication>()
                    .Include(a => a.GvaAppLotPart)
                    .Include(a => a.Doc)
                    .Where(a => a.GvaAppLotPartId == application.PartId && (a.DocId.HasValue ? a.Doc.DocStatus.Alias != "Canceled" : true))
                    .First();
            
            var json = new
            {
                root = new
                {
                    TERM_DATE = string.Format("{0:dd.MM.yyyy}", termDate),
                    CASE_TYPE = caseType,
                    APPLICATION_NUMBER = documentNumber,
                    APPLICATION_NAME = application.Content.ApplicationType.Name,
                    APPLICANT = applicant,
                    ACCESS_CODE = gvaApp.Doc != null ? gvaApp.Doc.AccessCode : null
                }
            };

            return json;
        }
    }
}
