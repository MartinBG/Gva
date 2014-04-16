﻿using System.Data.Entity;
using Common.Data;

namespace Docs.Api.Models
{
    public class DocsDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClassificationRelationMap());
            modelBuilder.Configurations.Add(new ClassificationRoleMap());
            modelBuilder.Configurations.Add(new ClassificationMap());
            modelBuilder.Configurations.Add(new CorrespondentContactMap());
            modelBuilder.Configurations.Add(new CorrespondentGroupMap());
            modelBuilder.Configurations.Add(new CorrespondentMap());
            modelBuilder.Configurations.Add(new CorrespondentTypeMap());
            modelBuilder.Configurations.Add(new DocCasePartMovementMap());
            modelBuilder.Configurations.Add(new DocCasePartTypeMap());
            modelBuilder.Configurations.Add(new DocClassificationMap());
            modelBuilder.Configurations.Add(new DocCorrespondentContactMap());
            modelBuilder.Configurations.Add(new DocCorrespondentMap());
            modelBuilder.Configurations.Add(new DocDestinationTypeMap());
            modelBuilder.Configurations.Add(new DocDirectionMap());
            modelBuilder.Configurations.Add(new DocElectronicServiceStageMap());
            modelBuilder.Configurations.Add(new DocEntryTypeMap());
            modelBuilder.Configurations.Add(new DocFileContentMap());
            modelBuilder.Configurations.Add(new DocFileKindMap());
            modelBuilder.Configurations.Add(new DocFileMap());
            modelBuilder.Configurations.Add(new DocFileTypeMap());
            modelBuilder.Configurations.Add(new DocFileOriginTypeMap());
            modelBuilder.Configurations.Add(new DocFormatTypeMap());
            modelBuilder.Configurations.Add(new DocHasReadMap());
            modelBuilder.Configurations.Add(new DocIncomingDocMap());
            modelBuilder.Configurations.Add(new DocRegisterMap());
            modelBuilder.Configurations.Add(new DocRelationMap());
            modelBuilder.Configurations.Add(new DocMap());
            modelBuilder.Configurations.Add(new DocSourceTypeMap());
            modelBuilder.Configurations.Add(new DocStatusMap());
            modelBuilder.Configurations.Add(new DocTypeClassificationMap());
            modelBuilder.Configurations.Add(new DocTypeGroupMap());
            modelBuilder.Configurations.Add(new DocTypeMap());
            modelBuilder.Configurations.Add(new DocTypeUnitRoleMap());
            modelBuilder.Configurations.Add(new DocUnitPermissionMap());
            modelBuilder.Configurations.Add(new DocUnitRoleMap());
            modelBuilder.Configurations.Add(new DocUnitMap());
            modelBuilder.Configurations.Add(new DocUserMap());
            modelBuilder.Configurations.Add(new DocWorkflowActionMap());
            modelBuilder.Configurations.Add(new DocWorkflowMap());
            modelBuilder.Configurations.Add(new ElectronicServiceStageExecutorMap());
            modelBuilder.Configurations.Add(new ElectronicServiceStageMap());
            modelBuilder.Configurations.Add(new IncomingDocFileMap());
            modelBuilder.Configurations.Add(new IncomingDocMap());
            modelBuilder.Configurations.Add(new IncomingDocStatusMap());
            modelBuilder.Configurations.Add(new IrregularityTypeMap());
            modelBuilder.Configurations.Add(new RegisterIndexMap());
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new UnitClassificationMap());
            modelBuilder.Configurations.Add(new UnitRelationMap());
            modelBuilder.Configurations.Add(new UnitMap());
            modelBuilder.Configurations.Add(new UnitTypeMap());
            modelBuilder.Configurations.Add(new UnitUserMap());
        }
    }
}
