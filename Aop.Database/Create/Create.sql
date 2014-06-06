USE [$(dbName)]
GO

---------------------------------------------------------------
-- Tables
---------------------------------------------------------------

:r $(rootPath)"\Tables\AopApplicationCriteria.sql"
:r $(rootPath)"\Tables\AopApplicationObjects.sql"
:r $(rootPath)"\Tables\AopEmployerTypes.sql"
:r $(rootPath)"\Tables\AopApplicationTypes.sql"
:r $(rootPath)"\Tables\AopChecklistStatuses.sql"
:r $(rootPath)"\Tables\AopProcedureStatuses.sql"

:r $(rootPath)"\Tables\AopEmployers.sql"
:r $(rootPath)"\Tables\AopApplications.sql"
:r $(rootPath)"\Tables\AopPortalDocRelations.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

---------------------------------------------------------------
--Insert AOP EXCEL CONFIG
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\AopExcelConfig\DocTypeGroups.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\Units.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\UnitRelations.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\RegisterIndexes.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\DocTypes.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\DocFileTypes.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\IrregularityTypes.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\Classifications.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ClassificationRelations.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\UnitClassifications.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\DocTypeUnitRoles.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\DocTypeClassifications.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ElectronicServiceStages.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ElectronicServiceStageExecutors.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\Users.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ConfigFinalize.sql"

:r $(rootPath)\"..\Insert\AopCorrespondents.sql"

:r $(rootPath)\"..\Insert\AopApplicationCriteria.sql"
:r $(rootPath)\"..\Insert\AopApplicationObjects.sql"
:r $(rootPath)\"..\Insert\AopApplicationTypes.sql"
:r $(rootPath)\"..\Insert\AopChecklistStatuses.sql"
:r $(rootPath)\"..\Insert\AopEmployerTypes.sql"
:r $(rootPath)\"..\Insert\AopProcedureStatuses.sql"

---------------------------------------------------------------
--Insert ADDITIONAL
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Docs\DocFileOriginTypes.sql"
:r $(rootPath)\"..\Insert\AopEmployers.sql"

