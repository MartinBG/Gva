﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Linq;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.Models.Views;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.Models.Views.Airport;
using Gva.Api.Models.Views.Equipment;
using Gva.Api.Models.Views.Organization;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.ModelsDO.Common;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Models.Enums;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using Regs.Api.LotEvents;
using Common.Api.UserContext;
using Docs.Api.Models.UnitModels;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public class ApplicationRepository : Repository<GvaApplication>, IApplicationRepository
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private IDocRepository docRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public ApplicationRepository(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            IDocRepository docRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(unitOfWork)
        {
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.nomRepository = nomRepository;
            this.docRepository = docRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        public ApplicationsDO GetApplications(
            string lotSetAlias = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? personLin = null,
            string aircraftIcao = null,
            string organizationUin = null,
            int? stage = null,
            int? inspectorId = null,
            int? applicationTypeId = null,
            int offset = 0,
            int? limit = null)
        {
            ApplicationsDO result = null;
            if (lotSetAlias == "person")
            {
                result = this.GetPersonApplications(fromDate, toDate, personLin, stage, inspectorId, applicationTypeId, offset, limit);
            }
            else if (lotSetAlias == "aircraft")
            {
                result = this.GetAircraftApplications(fromDate, toDate, aircraftIcao, stage, inspectorId, applicationTypeId, offset, limit);
            }
            else if (lotSetAlias == "organization")
            {
                result = this.GetOrganizationApplications(fromDate, toDate, organizationUin, stage, inspectorId, applicationTypeId, offset, limit);
            }
            else if (lotSetAlias == "equipment")
            {
                result = this.GetEquipmentApplications(fromDate, toDate, stage, inspectorId, applicationTypeId, offset, limit);
            }
            else if (lotSetAlias == "airport")
            {
                result = this.GetAirportApplications(fromDate, toDate, stage, inspectorId, applicationTypeId, offset, limit);
            }
            else
            {
                throw new NotSupportedException("Cannot get applications with not selected lot set alias!");
            }

            return result;
        }

        public IEnumerable<ApplicationExamListDO> GetPersonApplicationExams(int offset = 0, int? limit = null)
        {
            this.unitOfWork.DbContext.Set<GvaStage>().Load();

            var applications =
                (from a in this.unitOfWork.DbContext.Set<GvaViewPersonApplicationExam>()
                     .Include(a => a.Application)
                     .Include(a => a.Person)

                join gas in this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                                .GroupBy(ap => ap.GvaApplicationId)
                                .Select(s => s.OrderByDescending(ap => ap.GvaAppStageId).FirstOrDefault())
                         on a.AppPartId equals gas.GvaApplication.GvaAppLotPartId into gas1
                from gas2 in gas1.DefaultIfEmpty()

                select new
                {
                    Exam = a,
                    Application = a.Application,
                    Person = a.Person,
                    Stage = gas2
                })
                .Select(ap => new ApplicationExamListDO()
                {
                    AppExamId = ap.Exam.AppExamId,
                    AppPartId = ap.Exam.AppPartId,
                    LotId = ap.Exam.LotId,
                    DocumentNumber = ap.Application.DocumentNumber,
                    DocumentDate = ap.Application.DocumentDate,
                    CertCampCode = ap.Exam.CertCampCode,
                    CertCampName = ap.Exam.CertCampName,
                    ExamCode = ap.Exam.ExamCode,
                    ExamName = ap.Exam.ExamName,
                    ExamDate = ap.Exam.ExamDate,
                    PersonLin = ap.Person.Lin,
                    PersonNames = ap.Person.Names,
                    PersonUin = ap.Person.Uin,
                    StageName = ap.Stage.GvaStage.Name
                });

            return applications;

        }

        public ApplicationsDO GetPersonApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? personLin = null,
            int? stageId = null,
            int? inspectorId = null,
            int? applicationTypeId = null,
            int offset = 0,
            int? limit = null)
        {
            this.unitOfWork.DbContext.Set<GvaStage>().Load();

            var applicationsJoin =
                from a in this.unitOfWork.DbContext.Set<GvaApplication>()
                    .Include(a => a.GvaAppLotPart)
                    .Include(a => a.Lot.Set)
                    .Include(a => a.Stages)
                    .Include(a => a.GvaViewApplication)
                    .Include(a => a.GvaViewApplication.ApplicationType)
                    .Where(a => a.Lot.Set.Alias == "person" && (a.DocId.HasValue ? a.Doc.DocStatus.Alias != "Canceled" : true))

                join p in this.unitOfWork.DbContext.Set<GvaViewPerson>() on a.LotId equals p.LotId into p1
                from p2 in p1.DefaultIfEmpty()

                select new
                {
                    GApplication = a,
                    Set = a.Lot.Set,
                    GApplicationPart = a.GvaAppLotPart,
                    GViewApplication = a.GvaViewApplication,
                    GViewApplicationType = a.GvaViewApplication.ApplicationType,
                    GViewPerson = p2,
                    GvaAppStage = a.Stages.OrderByDescending(ap => ap.GvaAppStageId).FirstOrDefault()
                };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewPerson = new GvaViewPerson(),
                GvaAppStage = new GvaApplicationStage()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndEquals(e => e.GViewPerson.Lin, personLin)
                .AndEquals(e => e.GvaAppStage.GvaStage.GvaStageId, stageId)
                .AndEquals(e => e.GvaAppStage.InspectorLotId, inspectorId)
                .AndEquals(e => e.GViewApplicationType.NomValueId, applicationTypeId);

            var query = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId);

            var applications = query
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var result =  applications
                .Select(e => 
                    new ApplicationListDO()
                    {
                        ApplicationId = e.GApplication.GvaApplicationId,
                        DocId = e.GApplication.DocId,
                        LotSetName = e.Set.Name,
                        AppPartId = e.GApplication.GvaAppLotPartId,
                        LotId = e.GApplication.LotId,
                        AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                        AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                        AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                        AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                        PersonId = e.GViewPerson != null ? (int?)e.GViewPerson.LotId : null,
                        PersonLin = e.GViewPerson != null ? (int?)e.GViewPerson.Lin : null,
                        PersonNames = e.GViewPerson != null ? e.GViewPerson.Names : null,
                        StageName = e.GvaAppStage != null ? e.GvaAppStage.GvaStage.Name : null,
                        StageTermDate = e.GvaAppStage != null ? e.GvaAppStage.StageTermDate : null
                    })
                    .ToList();

            return new ApplicationsDO()
            {
                Applications = result.OrderByDescending(r => r.AppPartDocumentDate),
                ApplicationsCount = query.Count()
            };
        }

        public ApplicationsDO GetAircraftApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string aircraftIcao = null,
            int? stageId = null,
            int? inspectorId = null,
            int? applicationTypeId = null,
            int offset = 0,
            int? limit = null)
        {
            this.unitOfWork.DbContext.Set<GvaStage>().Load();

            var applicationsJoin =
                    from a in this.unitOfWork.DbContext.Set<GvaApplication>()
                        .Include(a => a.GvaAppLotPart)
                        .Include(a => a.Lot.Set)
                        .Include(a => a.Stages)
                        .Include(a => a.GvaViewApplication)
                        .Include(a => a.GvaViewApplication.ApplicationType)
                    where a.Lot.Set.Alias == "aircraft" && (a.DocId.HasValue ? a.Doc.DocStatus.Alias != "Canceled" : true)

                    join ac in this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                    .Include(a => a.AirCategory)
                    .Include(a => a.AircraftProducer) on a.LotId equals ac.LotId into ac1
                    from ac2 in ac1.DefaultIfEmpty()
                    select new
                    {
                        GApplication = a,
                        Set = a.Lot.Set,
                        GApplicationPart = a.GvaAppLotPart,
                        GViewApplication = a.GvaViewApplication,
                        GViewApplicationType = a.GvaViewApplication.ApplicationType,
                        GViewAircraft = ac2,
                        GViewAircraftCat = ac2.AirCategory,
                        GViewAircraftProd = ac2.AircraftProducer,
                        GvaAppStage = a.Stages.OrderByDescending(ap => ap.GvaAppStageId).FirstOrDefault()
                    };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewAircraft = new GvaViewAircraft(),
                GViewAircraftCat = new NomValue(),
                GViewAircraftProd = new NomValue(),
                GvaAppStage = new GvaApplicationStage()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndStringContains(e => e.GViewAircraft.ICAO, aircraftIcao)
                .AndEquals(e => e.GvaAppStage.GvaStage.GvaStageId, stageId)
                .AndEquals(e => e.GvaAppStage.InspectorLotId, inspectorId)
                .AndEquals(e => e.GViewApplicationType.NomValueId, applicationTypeId);

            var query = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId);

            var applications = query
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var result = applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    GvaAircraftId = e.GViewAircraft != null ? (int?)e.GViewAircraft.LotId : null,
                    GvaAirCategory = e.GViewAircraftCat != null ? e.GViewAircraftCat.Name : null,
                    GvaAircraftProducer = e.GViewAircraftProd != null ? e.GViewAircraftProd.Name : null,
                    GvaAircraftICAO = e.GViewAircraft != null ? e.GViewAircraft.ICAO : null,
                    StageName = e.GvaAppStage != null ? e.GvaAppStage.GvaStage.Name : null,
                    StageTermDate = e.GvaAppStage != null ? e.GvaAppStage.StageTermDate : null
                })
                .ToList();

            return new ApplicationsDO()
            {
                Applications = result.OrderByDescending(r => r.AppPartDocumentDate),
                ApplicationsCount = query.Count()
            };
        }

        public ApplicationsDO GetOrganizationApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string organizationUin = null,
            int? stageId = null,
            int? inspectorId = null,
            int? applicationTypeId = null,
            int offset = 0,
            int? limit = null)
        {
            this.unitOfWork.DbContext.Set<GvaStage>().Load();

            var applicationsJoin =
                    from a in this.unitOfWork.DbContext.Set<GvaApplication>()
                        .Include(a => a.GvaAppLotPart)
                        .Include(a => a.Lot.Set)
                        .Include(a => a.Stages)
                        .Include(a => a.GvaViewApplication)
                        .Include(a => a.GvaViewApplication.ApplicationType)

                    where a.Lot.Set.Alias == "organization" && (a.DocId.HasValue ? a.Doc.DocStatus.Alias != "Canceled" : true)

                    join o in this.unitOfWork.DbContext.Set<GvaViewOrganization>().Include(o => o.OrganizationType) on a.LotId equals o.LotId into o1
                    from o2 in o1.DefaultIfEmpty()
                    select new
                    {
                        GApplication = a,
                        Set = a.Lot.Set,
                        GApplicationPart = a.GvaAppLotPart,
                        GViewApplication = a.GvaViewApplication,
                        GViewApplicationType = a.GvaViewApplication.ApplicationType,
                        GViewOrganization = o2,
                        GViewOrganizationType = o2.OrganizationType,
                        GvaAppStage = a.Stages.OrderByDescending(ap => ap.GvaAppStageId).FirstOrDefault()
                    };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewOrganization = new GvaViewOrganization(),
                GViewOrganizationType = new NomValue(),
                GvaAppStage = new GvaApplicationStage()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndStringContains(e => e.GViewOrganization.Uin, organizationUin)
                .AndEquals(e => e.GvaAppStage.GvaStage.GvaStageId, stageId)
                .AndEquals(e => e.GvaAppStage.InspectorLotId, inspectorId)
                .AndEquals(e => e.GViewApplicationType.NomValueId, applicationTypeId);

            var query = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId);

            var applications = query
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var result = applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    GvaOrganizationId = e.GViewOrganization != null ? (int?)e.GViewOrganization.LotId : null,
                    GvaOrganizationName = e.GViewOrganization != null ? e.GViewOrganization.Name : null,
                    GvaOrganizationUin = e.GViewOrganization != null ? e.GViewOrganization.Uin : null,
                    StageName = e.GvaAppStage != null ? e.GvaAppStage.GvaStage.Name : null,
                    StageTermDate = e.GvaAppStage != null ? e.GvaAppStage.StageTermDate : null
                })
                .ToList();

            return new ApplicationsDO()
            {
                Applications = result.OrderByDescending(r => r.AppPartDocumentDate),
                ApplicationsCount = query.Count()
            };
        }

        public ApplicationsDO GetEquipmentApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? stageId = null,
            int? inspectorId = null,
            int? applicationTypeId = null,
            int offset = 0,
            int? limit = null)
        {
            this.unitOfWork.DbContext.Set<GvaStage>().Load();

            var applicationsJoin =
                    from a in this.unitOfWork.DbContext.Set<GvaApplication>()
                        .Include(a => a.GvaAppLotPart)
                        .Include(a => a.Lot.Set)
                        .Include(a => a.Stages)
                        .Include(a => a.GvaViewApplication)
                        .Include(a => a.GvaViewApplication.ApplicationType)
                    where a.Lot.Set.Alias == "equipment" && (a.DocId.HasValue ? a.Doc.DocStatus.Alias != "Canceled" : true)

                    join eq in this.unitOfWork.DbContext.Set<GvaViewEquipment>().Include(e => e.EquipmentType).Include(e => e.EquipmentProducer) on a.LotId equals eq.LotId into eq1
                    from eq2 in eq1.DefaultIfEmpty()
                    select new
                    {
                        GApplication = a,
                        Set = a.Lot.Set,
                        GApplicationPart = a.GvaAppLotPart,
                        GViewApplication = a.GvaViewApplication,
                        GViewApplicationType = a.GvaViewApplication.ApplicationType,
                        GViewEquipment = eq2,
                        GViewEquipmentType = eq2.EquipmentType,
                        GViewEquipmentProducer = eq2.EquipmentProducer,
                        GvaAppStage = a.Stages.OrderByDescending(ap => ap.GvaAppStageId).FirstOrDefault()
                    };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewEquipment = new GvaViewEquipment(),
                GViewEquipmentType = new NomValue(),
                GViewEquipmentProducer = new NomValue(),
                GvaAppStage = new GvaApplicationStage()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndEquals(e => e.GvaAppStage.GvaStage.GvaStageId, stageId)
                .AndEquals(e => e.GvaAppStage.InspectorLotId, inspectorId)
                .AndEquals(e => e.GViewApplicationType.NomValueId, applicationTypeId);

            var query = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId);

            var applications = query
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var result = applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    GvaEquipmentId = e.GViewEquipment != null ? (int?)e.GViewEquipment.LotId : null,
                    GvaEquipmentName = e.GViewEquipment != null ? e.GViewEquipment.Name : null,
                    GvaEquipmentType = e.GViewEquipmentType != null ? e.GViewEquipmentType.Name : null,
                    GvaEquipmentProducer = e.GViewEquipmentProducer != null ? e.GViewEquipmentProducer.Name : null,
                    StageName = e.GvaAppStage != null ? e.GvaAppStage.GvaStage.Name : null,
                    StageTermDate = e.GvaAppStage != null ? e.GvaAppStage.StageTermDate : null
                })
                .ToList();

            return new ApplicationsDO()
            {
                Applications = result.OrderByDescending(r => r.AppPartDocumentDate),
                ApplicationsCount = query.Count()
            };
        }

        public ApplicationsDO GetAirportApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? stageId = null,
            int? inspectorId = null,
            int? applicationTypeId = null,
            int offset = 0,
            int? limit = null)
        {
            this.unitOfWork.DbContext.Set<GvaStage>().Load();

            var applicationsJoin =
                    from a in this.unitOfWork.DbContext.Set<GvaApplication>()
                        .Include(a => a.GvaAppLotPart)
                        .Include(a => a.Lot.Set)
                        .Include(a => a.Stages)
                        .Include(a => a.GvaViewApplication)
                        .Include(a => a.GvaViewApplication.ApplicationType)
                    where a.Lot.Set.Alias == "airport" && (a.DocId.HasValue ? a.Doc.DocStatus.Alias != "Canceled" : true)

                    join ap in this.unitOfWork.DbContext.Set<GvaViewAirport>().Include(ai => ai.AirportType) on a.LotId equals ap.LotId into ap1
                    from ap2 in ap1.DefaultIfEmpty()
                    select new
                    {
                        GApplication = a,
                        Set = a.Lot.Set,
                        GApplicationPart = a.GvaAppLotPart,
                        GViewApplication = a.GvaViewApplication,
                        GViewApplicationType = a.GvaViewApplication.ApplicationType,
                        GViewAirport = ap2,
                        GViewAirportType = ap2.AirportType,
                        GvaAppStage = a.Stages.OrderByDescending(ap => ap.GvaAppStageId).FirstOrDefault()
                    };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewAirport = new GvaViewAirport(),
                GViewAirportType = new NomValue(),
                GvaAppStage = new GvaApplicationStage()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndEquals(e => e.GvaAppStage.GvaStage.GvaStageId, stageId)
                .AndEquals(e => e.GvaAppStage.InspectorLotId, inspectorId)
                .AndEquals(e => e.GViewApplicationType.NomValueId, applicationTypeId);

            var query = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId);

            var applications = query
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            var result = applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    GvaAirportId = e.GViewAirport != null ? (int?)e.GViewAirport.LotId : null,
                    GvaAirportType = e.GViewAirportType != null ? e.GViewAirportType.Name : null,
                    GvaAirportName = e.GViewAirport != null ? e.GViewAirport.Name : null,
                    StageName = e.GvaAppStage != null ? e.GvaAppStage.GvaStage.Name : null,
                    StageTermDate = e.GvaAppStage != null ? e.GvaAppStage.StageTermDate : null
                })
                .ToList();

            return new ApplicationsDO()
            {
                Applications = result.OrderByDescending(r => r.AppPartDocumentDate),
                ApplicationsCount = query.Count()
            };
        }
        public IEnumerable<GvaApplication> GetLinkedToDocsApplications()
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>().Where(e => e.DocId.HasValue).AsQueryable();
        }

        public IEnumerable<ApplicationNomDO> GetNomApplications(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.GvaViewApplication)
                .Include(a => a.GvaViewApplication.ApplicationType)
                .Include(a => a.Doc)
                .Where(a => a.LotId == lotId && (a.DocId.HasValue ? a.Doc.DocStatus.Alias != "Canceled" : true))
                .AsEnumerable()
                .Select(a => new ApplicationNomDO(a))
                .OrderByDescending(a => a.ApplicationDate ?? DateTime.MaxValue);
        }

        public ApplicationNomDO GetNomApplication(int applicationId)
        {
            var app = this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.GvaViewApplication)
                .Include(a => a.GvaViewApplication.ApplicationType)
                .Include(a => a.Doc)
                .Where(a => a.GvaApplicationId == applicationId)
                .SingleOrDefault();

            return new ApplicationNomDO(app);
        }

        public void AddGvaApplication(GvaApplication gvaApplication)
        {
            this.unitOfWork.DbContext.Set<GvaApplication>().Add(gvaApplication);
        }

        public void DeleteGvaApplication(int gvaAppLotPartId)
        {
            var application = this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.GvaAppLotFiles)
                .SingleOrDefault(a => a.GvaAppLotPartId == gvaAppLotPartId);

            if (application != null)
            {
                foreach (var appFile in application.GvaAppLotFiles.ToList())
                {
                    this.unitOfWork.DbContext.Set<GvaAppLotFile>().Remove(appFile);
                }

                this.unitOfWork.DbContext.Set<GvaApplication>().Remove(application);
            }
        }

        public void AddGvaLotFile(GvaLotFile gvaLotFile)
        {
            this.unitOfWork.DbContext.Set<GvaLotFile>().Add(gvaLotFile);
        }

        public void AddGvaAppLotFile(GvaAppLotFile gvaAppLotFile)
        {
            this.unitOfWork.DbContext.Set<GvaAppLotFile>().Add(gvaAppLotFile);
        }

        public List<GvaCorrespondent> GetGvaCorrespondentsByLotId(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaCorrespondent>()
                .Include(e => e.Correspondent)
                .Where(p => p.LotId == lotId)
                .ToList();
        }

        public void AddGvaCorrespondent(GvaCorrespondent gvaCorrespondent)
        {
            this.unitOfWork.DbContext.Set<GvaCorrespondent>().Add(gvaCorrespondent);
        }

        public GvaApplication GetGvaApplicationByDocId(int docId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>()
                .FirstOrDefault(e => e.DocId == docId);
        }

        public IEnumerable<Set> GetLotSets(
            string names = null,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<Set>();

            predicate = predicate
                .AndStringMatches(p => p.Name, names, exact);

            var persons = this.unitOfWork.DbContext.Set<Set>()
                .Where(predicate);

            return persons
                .OrderBy(p => p.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public Set GetLotSet(int lotSetId)
        {
            return this.unitOfWork.DbContext.Set<Set>()
                .SingleOrDefault(p => p.SetId == lotSetId);
        }

        public List<DocumentApplicationViewDO> GetApplicationsForLot(int lotId, string path, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<DocumentApplicationDO>(path);

            List<DocumentApplicationViewDO> applicationViewDOs = new List<DocumentApplicationViewDO>();
            foreach (var partVersion in partVersions)
            {
                var gvaApplication = this.unitOfWork.DbContext.Set<GvaApplication>()
                    .Include(a => a.GvaAppLotPart)
                    .Include(a => a.Doc.DocStatus)
                    .Include(a => a.Stages.Select(s => s.GvaStage))
                    .Where(a => a.GvaAppLotPartId == partVersion.PartId && (a.DocId.HasValue ? a.Doc.DocStatus.Alias != "Canceled" : true))
                    .FirstOrDefault();

                if (gvaApplication == null) 
                {
                    continue;
                }

                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    DocumentApplicationViewDO applicationViewDO = new DocumentApplicationViewDO()
                    {
                        ApplicationId = gvaApplication.GvaApplicationId,
                        ApplicationPaymentType = partVersion.Content.ApplicationPaymentType,
                        Currency = partVersion.Content.Currency,
                        DocumentDate = partVersion.Content.DocumentDate,
                        DocumentNumber = partVersion.Content.DocumentNumber,
                        ApplicationType = partVersion.Content.ApplicationType,
                        TaxAmount = partVersion.Content.TaxAmount,
                        TaxNumber = partVersion.Content.TaxNumber,
                        Notes = partVersion.Content.Notes,
                        OldDocumentNumber = partVersion.Content.OldDocumentNumber,
                        Case = lotFile != null ? new CaseDO(lotFile) : null
                    };

                    if (gvaApplication.Stages.Count() > 0)
                    {
                        var applicationStage = gvaApplication.Stages.OrderByDescending(a => a.GvaAppStageId).First();
                        applicationViewDO.Stage = new NomValue()
                        {
                            Name = applicationStage.GvaStage.Name,
                            Alias = applicationStage.GvaStage.Alias
                        };
                    }

                    applicationViewDOs.Add(applicationViewDO);
                }
            }

            return applicationViewDOs;
        }

        public GvaViewApplication GetApplicationById(int applicationId)
        {
            var application = this.unitOfWork.DbContext.Set<GvaApplication>().Where(a => a.GvaApplicationId == applicationId).Single();
            return this.unitOfWork.DbContext.Set<GvaViewApplication>()
                .Include(a => a.ApplicationType)
                .Where(gva => gva.PartId == application.GvaAppLotPartId)
                .Single();
        }

        public GvaViewApplication GetApplicationByPartId(int applicationPartId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewApplication>()
                .Include(a => a.ApplicationType)
                .Where(gva => gva.PartId == applicationPartId)
                .Single();
        }

        public List<AppExamSystQualificationDO> GetApplicationQualifications(string path, int lotId)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<DocumentApplicationDO>(path);

            var licenceTypeIds = this.nomRepository.GetNomValue(partVersion.Content.ApplicationType.NomValueId)
                    .TextContent
                    .GetItems<int>("licenceTypeIds");

            List<AppExamSystQualificationDO> qualifications = new List<AppExamSystQualificationDO>();
            foreach (int licenceTypeId in licenceTypeIds)
            {
                var licenceType = this.nomRepository.GetNomValue(licenceTypeId);
                string licenceQlfCode = licenceType.TextContent.Get<string>("qlfCode");
                if (!string.IsNullOrEmpty(licenceQlfCode))
                {
                    var qualification = this.unitOfWork.DbContext.Set<GvaExSystQualification>()
                        .Where(q => q.Code == licenceQlfCode)
                        .FirstOrDefault();

                    var personQualification = this.unitOfWork.DbContext.Set<GvaViewPersonQualification>()
                        .Where(q => q.LotId == lotId && q.LicenceTypeCode == licenceType.Code)
                        .FirstOrDefault();

                    qualifications.Add(
                        new AppExamSystQualificationDO()
                        {
                            LicenceType = licenceType,
                            QualificationCode = qualification.Code,
                            QualificationName = qualification.Name,
                            State = personQualification != null ? personQualification.State : QualificationState.Missing.ToString(),
                            StateMethod = personQualification != null ? personQualification.StateMethod : null
                        });
                }
            }

            return qualifications;
        }

        private Doc CreateNewDoc(List<int> correspondents, NomValue applicationType, UserContext userContext)
        {
            DocDirection direction = this.unitOfWork.DbContext.Set<DocDirection>()
                        .SingleOrDefault(d => d.Alias == "Incomming");
            DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>()
                .SingleOrDefault(e => e.Alias == "Document");
            DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>()
                .SingleOrDefault(s => s.Alias == "Draft");
            DocCasePartType casePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                .SingleOrDefault(pt => pt.Alias == "Public");
            DocSourceType manualSource = this.unitOfWork.DbContext.Set<DocSourceType>().
                SingleOrDefault(st => st.Alias == "Manual");
            DocFormatType formatType = this.unitOfWork.DbContext.Set<DocFormatType>()
                .SingleOrDefault(ft => ft.Alias == "Paper");

            int documentTypeId = this.nomRepository.GetNomValue("applicationTypes", applicationType.NomValueId)
                .TextContent.Get<int>("documentTypeId");

            Doc newDoc = this.docRepository.CreateDoc(
            direction.DocDirectionId,
            documentEntryType.DocEntryTypeId,
            draftStatus.DocStatusId,
            applicationType.Name,
            casePartType.DocCasePartTypeId,
            manualSource.DocSourceTypeId,
            null,
            documentTypeId,
            formatType.DocFormatTypeId,
            null,
            userContext);

            DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                .SingleOrDefault(e => e.Alias.ToLower() == "public");

            List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                .ToList();

            ElectronicServiceStage electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                .SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId && e.IsFirstByDefault);

            List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                .ToList();

            DocUnitRole importedBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                .SingleOrDefault(e => e.Alias == "ImportedBy");

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>()
                .FirstOrDefault(e => e.UserId == userContext.UserId);

            newDoc.CreateDocProperties(
                    null,
                    internalDocCasePartType.DocCasePartTypeId,
                    docTypeClassifications,
                    null,
                    electronicServiceStage,
                    docTypeUnitRoles,
                    importedBy,
                    unitUser,
                    correspondents,
                    null,
                    userContext);

            this.docRepository.GenerateAccessCode(newDoc, userContext);

            this.unitOfWork.Save();

            this.docRepository.ExecSpSetDocTokens(docId: newDoc.DocId);
            this.docRepository.ExecSpSetDocUnitTokens(docId: newDoc.DocId);

            newDoc.SetReceiptOrder(this.docRepository.GetNextReceiptOrder(newDoc.DocId), userContext);

            this.docRepository.RegisterDoc(newDoc, unitUser, userContext);

            return newDoc;
        }

        public ApplicationMainDO CreateNewApplication(ApplicationNewDO applicationNewDO, UserContext userContext, int? docId = null)
        {
            var gvaCorrespondents = this.GetGvaCorrespondentsByLotId(applicationNewDO.LotId);

            foreach (var corrId in applicationNewDO.Correspondents)
            {
                if (!gvaCorrespondents.Any(e => e.CorrespondentId == corrId))
                {
                    GvaCorrespondent gvaCorrespondent = new GvaCorrespondent();
                    gvaCorrespondent.CorrespondentId = corrId;
                    gvaCorrespondent.LotId = applicationNewDO.LotId;
                    gvaCorrespondent.IsActive = true;

                    this.AddGvaCorrespondent(gvaCorrespondent);
                }
            }

            Doc doc = null;
            if(docId.HasValue)
            {
                int caseId = this.docRepository.GetCaseId(docId.Value);

                doc = this.docRepository.Find(caseId);
            }
            else
            {
                doc = this.CreateNewDoc(applicationNewDO.Correspondents, applicationNewDO.ApplicationType, userContext);
            }

            if (applicationNewDO.ApplicationType != null)
            {
                var lot = this.lotRepository.GetLotIndex(applicationNewDO.LotId);

                var application = new DocumentApplicationDO
                {
                    DocumentDate = doc.RegDate,
                    ApplicationType = applicationNewDO.ApplicationType,
                    DocumentNumber = doc.RegUri
                };

                PartVersion<DocumentApplicationDO> partVersion = lot.CreatePart(applicationNewDO.SetPartPath + "/*", application, userContext);

                lot.Commit(userContext, lotEventDispatcher);

                GvaApplication newGvaApplication = new GvaApplication()
                {
                    LotId = applicationNewDO.LotId,
                    Doc = doc,
                    GvaAppLotPart = partVersion != null ? partVersion.Part : null
                };

                this.AddGvaApplication(newGvaApplication);

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = null,
                    GvaCaseTypeId = applicationNewDO.CaseTypeId,
                    PageIndex = null,
                    PageIndexInt = null,
                    PageNumber = null,
                    Note = null
                };

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = newGvaApplication,
                    GvaLotFile = lotFile,
                    DocFile = null
                };

                this.AddGvaLotFile(lotFile);
                this.AddGvaAppLotFile(gvaAppLotFile);

                this.unitOfWork.Save();

                int stageId = this.unitOfWork.DbContext.Set<GvaStage>()
                    .Where(s => s.Alias.Equals("new"))
                    .Single().GvaStageId;

                dynamic stageTermDate = null;
                int? duration = this.nomRepository.GetNomValue("applicationTypes", applicationNewDO.ApplicationType.NomValueId).TextContent.Get<int?>("duration");
                if (duration.HasValue)
                {
                    stageTermDate = DateTime.Now.AddDays(duration.Value);
                }

                GvaApplicationStage applicationStage = new GvaApplicationStage()
                {
                    GvaStageId = stageId,
                    StartingDate = DateTime.Now,
                    Ordinal = 0,
                    GvaApplicationId = newGvaApplication.GvaApplicationId,
                    StageTermDate = stageTermDate
                };

                this.unitOfWork.DbContext.Set<GvaApplicationStage>().Add(applicationStage);
                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                return new ApplicationMainDO()
                {
                    LotId = applicationNewDO.LotId,
                    GvaApplicationId = newGvaApplication.GvaApplicationId,
                    PartIndex = partVersion.Part.Index,
                    Set = partVersion.Part.Lot.Set.Alias.ToLower()
                };
            }
            else
            {
                GvaApplication newGvaApplication = new GvaApplication()
                {
                    LotId = applicationNewDO.LotId,
                    Doc = doc
                };

                this.AddGvaApplication(newGvaApplication);

                this.unitOfWork.Save();

                return new ApplicationMainDO()
                {
                    LotId = applicationNewDO.LotId,
                    GvaApplicationId = newGvaApplication.GvaApplicationId
                };
            }
        }
    }
}
