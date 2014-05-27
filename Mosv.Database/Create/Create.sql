USE [$(dbName)]
GO

---------------------------------------------------------------
-- Tables
---------------------------------------------------------------

:r $(rootPath)\"Tables\MosvViewAdmissions.sql"
:r $(rootPath)\"Tables\MosvViewSignals.sql"
:r $(rootPath)\"Tables\MosvViewSuggestions.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

---------------------------------------------------------------
-- Insert Migration Necessities
---------------------------------------------------------------

---------------------------------------------------------------
-- Insert Static Noms
---------------------------------------------------------------

---------------------------------------------------------------
-- Insert Migration Noms Values
---------------------------------------------------------------

--DBCC CHECKIDENT ('NomValues', RESEED, 999)

---------------------------------------------------------------
-- Insert MOSV Static Noms
---------------------------------------------------------------


---------------------------------------------------------------
-- Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\registers.sql"
:r $(rootPath)\"..\Insert\Noms\mosv.sql"

---------------------------------------------------------------
--Insert Mosv EXCEL CONFIG
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\MosvExcelConfig\DocTypeGroups.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\Units.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\UnitRelations.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\RegisterIndexes.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\DocTypes.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\DocFileTypes.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\IrregularityTypes.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\Classifications.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\ClassificationRelations.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\UnitClassifications.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\DocTypeUnitRoles.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\DocTypeClassifications.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\ElectronicServiceStages.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\ElectronicServiceStageExecutors.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\Users.sql"
:r $(rootPath)\"..\Insert\MosvExcelConfig\ConfigFinalize.sql"

:r $(rootPath)\"..\Insert\MosvCorrespondents.sql"
