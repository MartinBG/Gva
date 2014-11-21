USE [$(dbName)]
GO

---------------------------------------------------------------
-- Tables
---------------------------------------------------------------
--Roles
:r $(rootPath)"\Tables\Users\RoleClassifications.sql"

--Aop specific
:r $(rootPath)"\Tables\AopEmployers.sql"
:r $(rootPath)"\Tables\AopApplications.sql"
:r $(rootPath)"\Tables\AopPortalDocRelations.sql"

--Aop Tokens
:r $(rootPath)"\Tables\Tokens\AopApplicationTokens.sql"

---------------------------------------------------------------
-- Views
---------------------------------------------------------------

:r $(rootPath)"\Views\vwAopApplicationUsers.sql"

---------------------------------------------------------------
-- Procedures
---------------------------------------------------------------
:r $(rootPath)"\Procedures\Tokens\spSetAopApplicationTokens.sql"
:r $(rootPath)"\Procedures\Tokens\spSetAopApplicationUnitTokens.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

---------------------------------------------------------------
--Insert AOP EXCEL CONFIG
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\AopExcelConfig\DocTypeGroups.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ElectronicServiceProviders.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\Units.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\UnitRelations.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\RegisterIndexes.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\DocTypes.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\DocFileTypes.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\IrregularityTypes.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\Classifications.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ClassificationRelations.sql"
--:r $(rootPath)\"..\Insert\AopExcelConfig\UnitClassifications.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\DocTypeUnitRoles.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\DocTypeClassifications.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ElectronicServiceStages.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ElectronicServiceStageExecutors.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\Users.sql"
:r $(rootPath)\"..\Insert\AopExcelConfig\ConfigFinalize.sql"

---------------------------------------------------------------
--Insert ADDITIONAL
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\AopCorrespondents.sql"

:r $(rootPath)\"..\Insert\Noms\AopApplicationCriteria.sql"
:r $(rootPath)\"..\Insert\Noms\AopApplicationObjects.sql"
:r $(rootPath)\"..\Insert\Noms\AopApplicationTypes.sql"
:r $(rootPath)\"..\Insert\Noms\AopChecklistStatuses.sql"
:r $(rootPath)\"..\Insert\Noms\AopEmployerTypes.sql"
:r $(rootPath)\"..\Insert\Noms\AopProcedureStatuses.sql"

:r $(rootPath)\"..\Insert\Users\Roles.sql"
:r $(rootPath)\"..\Insert\Users\UserRoles.sql"
:r $(rootPath)\"..\Insert\Users\RoleClassifications.sql"
