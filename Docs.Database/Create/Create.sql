USE [$(dbName)]
GO

---------------------------------------------------------------
--Tables
---------------------------------------------------------------
--Registers
:r $(rootPath)"\Tables\Registers\RegisterIndexes.sql"

-- Classifications
:r $(rootPath)"\Tables\Classifications\Classifications.sql"
:r $(rootPath)"\Tables\Classifications\ClassificationRelations.sql"
:r $(rootPath)"\Tables\Classifications\ClassificationRoles.sql"

-- Units
:r $(rootPath)"\Tables\Units\UnitTypes.sql"
:r $(rootPath)"\Tables\Units\Units.sql"
:r $(rootPath)"\Tables\Units\UnitRelations.sql"
:r $(rootPath)"\Tables\Units\UnitClassifications.sql"
:r $(rootPath)"\Tables\Units\UnitUsers.sql"

--Assignments
:r $(rootPath)"\Tables\Assignments\AssignmentTypes.sql"

--Correspondent
:r $(rootPath)"\Tables\Correspondents\CorrespondentGroups.sql"
:r $(rootPath)"\Tables\Correspondents\CorrespondentTypes.sql"
-- the table Correspondents uses this function
:r $(rootPath)"\Functions\fnGetCorrespondentDisplayName.sql"
:r $(rootPath)"\Tables\Correspondents\Correspondents.sql"
:r $(rootPath)"\Tables\Correspondents\CorrespondentContacts.sql"

-- CommonDocuments
:r $(rootPath)"\Tables\Documents\DocFileTypes.sql"
:r $(rootPath)"\Tables\Documents\DocFileKinds.sql"
:r $(rootPath)"\Tables\Documents\DocFileOriginTypes.sql"

-- IncomingDocuments
:r $(rootPath)"\Tables\IncomingDocuments\IncomingDocStatuses.sql"
:r $(rootPath)"\Tables\IncomingDocuments\IncomingDocs.sql"
:r $(rootPath)"\Tables\IncomingDocuments\IncomingDocFiles.sql"

-- Documents
:r $(rootPath)"\Tables\Documents\DocCasePartTypes.sql"
:r $(rootPath)"\Tables\Documents\DocDirections.sql"
:r $(rootPath)"\Tables\Documents\DocEntryTypes.sql"
:r $(rootPath)"\Tables\Documents\DocSourceTypes.sql"
:r $(rootPath)"\Tables\Documents\DocDestinationTypes.sql"
:r $(rootPath)"\Tables\Documents\DocStatuses.sql"
:r $(rootPath)"\Tables\Documents\DocUnitRoles.sql"

:r $(rootPath)"\Tables\Documents\DocTypeGroups.sql"
:r $(rootPath)"\Tables\Documents\DocRegisters.sql"
:r $(rootPath)"\Tables\Documents\DocTypes.sql"
:r $(rootPath)"\Tables\Documents\DocFormatTypes.sql"

:r $(rootPath)"\Tables\Documents\Docs.sql"
:r $(rootPath)"\Tables\Documents\DocCorrespondents.sql"
:r $(rootPath)"\Tables\Documents\DocCorrespondentContacts.sql"
:r $(rootPath)"\Tables\Documents\DocRelations.sql"
:r $(rootPath)"\Tables\Documents\DocUnits.sql"
:r $(rootPath)"\Tables\Documents\DocClassifications.sql"
:r $(rootPath)"\Tables\Documents\DocFiles.sql"
:r $(rootPath)"\Tables\Documents\DocIncomingDocs.sql"

:r $(rootPath)"\Tables\Documents\DocTypeUnitRoles.sql"
:r $(rootPath)"\Tables\Documents\DocTypeClassifications.sql"
:r $(rootPath)"\Tables\Documents\DocUnitPermissions.sql"
:r $(rootPath)"\Tables\Documents\DocUsers.sql"
:r $(rootPath)"\Tables\Documents\DocHasReads.sql"

:r $(rootPath)"\Tables\Documents\DocCasePartMovements.sql"

--Others
:r $(rootPath)"\Tables\Others\IrregularityTypes.sql"

--DocWorkflows
:r $(rootPath)"\Tables\Documents\DocWorkflowActions.sql"
:r $(rootPath)"\Tables\Documents\DocWorkflows.sql"

--DocFileContent
:r $(rootPath)"\Tables\Documents\DocFileContents.sql"
:r $(rootPath)"\Tables\Documents\Tickets.sql"

--ElectronicServices
:r $(rootPath)"\Tables\ElectronicServices\ElectronicServiceStages.sql"
:r $(rootPath)"\Tables\ElectronicServices\ElectronicServiceStageExecutors.sql"
:r $(rootPath)"\Tables\Documents\DocElectronicServiceStages.sql"

--AdministrativeEmails
:r $(rootPath)"\Tables\AdministrativeEmails\AdministrativeEmailStatuses.sql"
:r $(rootPath)"\Tables\AdministrativeEmails\AdministrativeEmailTypes.sql"
:r $(rootPath)"\Tables\AdministrativeEmails\AdministrativeEmails.sql"

---------------------------------------------------------------
-- Functions
---------------------------------------------------------------
:r $(rootPath)"\Functions\fnCheckForRegisteredChildDocs.sql"
--Classifications Fn
:r $(rootPath)"\Functions\fnGetParentClassifications.sql"
:r $(rootPath)"\Functions\fnGetSubordinateClassifications.sql"
--Units Fn
:r $(rootPath)"\Functions\fnGetParentUnits.sql"
:r $(rootPath)"\Functions\fnGetSubordinateUnits.sql"

---------------------------------------------------------------
-- Procedures
---------------------------------------------------------------
:r $(rootPath)"\Procedures\spGetDocRegisterId.sql"
:r $(rootPath)"\Procedures\spGetDocRegisterNextNumber.sql"
:r $(rootPath)"\Procedures\spDeleteNotRegisteredDoc.sql"

--Classifications SP
:r $(rootPath)"\Procedures\Classifications\spSetDocUsers.sql"
:r $(rootPath)"\Procedures\Classifications\spSetUserDocs.sql"
:r $(rootPath)"\Procedures\Classifications\spSetAllDocsUsers.sql"
:r $(rootPath)"\Procedures\Classifications\spGetUnitClassifications.sql"
:r $(rootPath)"\Procedures\Classifications\spSetDeactiveUnit.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------
:r $(rootPath)"\Diagram\Main.sql"
:r $(rootPath)"\Diagram\Docs.sql"
:r $(rootPath)"\Diagram\DocRelations.sql"
:r $(rootPath)"\Diagram\Correspondents.sql"
:r $(rootPath)"\Diagram\Users.sql"

---------------------------------------------------------------
-- Views
---------------------------------------------------------------
:r $(rootPath)"\Views\vwDocs.sql"


---------------------------------------------------------------
-- Insert
---------------------------------------------------------------
--classifications
:r $(rootPath)\"..\Insert\Classifications\Classifications.sql"
:r $(rootPath)\"..\Insert\Classifications\ClassificationRelations.sql"
:r $(rootPath)\"..\Insert\Classifications\ClassificationRoles.sql"


--assignments
:r $(rootPath)\"..\Insert\Assignments\AssignmentTypes.sql"

--units
:r $(rootPath)\"..\Insert\Units\UnitTypes.sql"
:r $(rootPath)\"..\Insert\Units\Units.sql"
:r $(rootPath)\"..\Insert\Units\UnitRelations.sql"
:r $(rootPath)\"..\Insert\Units\UnitUsers.sql"
:r $(rootPath)\"..\Insert\Units\UnitClassifications.sql"

--docs
:r $(rootPath)\"..\Insert\Docs\RegisterIndexes.sql"
:r $(rootPath)\"..\Insert\Docs\DocCasePartTypes.sql"
:r $(rootPath)\"..\Insert\Docs\DocDestinationTypes.sql"
:r $(rootPath)\"..\Insert\Docs\DocDirections.sql"
:r $(rootPath)\"..\Insert\Docs\DocEntryTypes.sql"
:r $(rootPath)\"..\Insert\Docs\DocFormatTypes.sql"
:r $(rootPath)\"..\Insert\Docs\DocStatuses.sql"
:r $(rootPath)\"..\Insert\Docs\DocSourceTypes.sql"
:r $(rootPath)\"..\Insert\Docs\DocTypeGroups.sql"
:r $(rootPath)\"..\Insert\Docs\DocTypes.sql"
:r $(rootPath)\"..\Insert\Docs\DocTypeClassifications.sql"
:r $(rootPath)\"..\Insert\Docs\DocUnitPermissions.sql"
:r $(rootPath)\"..\Insert\Docs\DocUnitRoles.sql"
:r $(rootPath)\"..\Insert\Docs\DocWorkflowActions.sql"
:r $(rootPath)\"..\Insert\Docs\DocFileKinds.sql"
:r $(rootPath)\"..\Insert\Docs\DocFileTypes.sql"
:r $(rootPath)\"..\Insert\Docs\DocFileOriginTypes.sql"

--correspondents
:r $(rootPath)\"..\Insert\Correspondents\CorrespondentGroups.sql"
:r $(rootPath)\"..\Insert\Correspondents\CorrespondentTypes.sql"
:r $(rootPath)\"..\Insert\Correspondents\Correspondents.sql"

--IncomingDocs
:r $(rootPath)\"..\Insert\IncomingDocs\IncomingDocStatuses.sql"

--electronic service stages
:r $(rootPath)\"..\Insert\ElectronicServiceStages\ElectronicServiceStages.sql"
:r $(rootPath)\"..\Insert\ElectronicServiceStages\ElectronicServiceStageExecutors.sql"


