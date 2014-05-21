USE [$(dbName)]
GO

---------------------------------------------------------------
-- Tables
---------------------------------------------------------------

:r $(rootPath)\"Tables\GvaCaseTypes.sql"
:r $(rootPath)\"Tables\GvaLotCases.sql"
:r $(rootPath)\"Tables\GvaApplications.sql"
:r $(rootPath)\"Tables\GvaCorrespondents.sql"
:r $(rootPath)\"Tables\GvaFiles.sql"
:r $(rootPath)\"Tables\GvaLotFiles.sql"
:r $(rootPath)\"Tables\GvaAppLotFiles.sql"
:r $(rootPath)\"Tables\GvaLotObjects.sql"
:r $(rootPath)\"Tables\GvaViewPersons.sql"
:r $(rootPath)\"Tables\GvaViewPersonLicences.sql"
:r $(rootPath)\"Tables\GvaViewPersonRatings.sql"
:r $(rootPath)\"Tables\GvaViewInventoryItems.sql"
:r $(rootPath)\"Tables\GvaViewApplications.sql"
:r $(rootPath)\"Tables\GvaViewOrganizations.sql"
:r $(rootPath)\"Tables\GvaViewAircrafts.sql"
:r $(rootPath)\"Tables\GvaViewAirports.sql"
:r $(rootPath)\"Tables\GvaViewEquipments.sql"
:r $(rootPath)\"Tables\GvaViewAircraftRegistrations.sql"
:r $(rootPath)\"Tables\GvaViewAircraftRegMarks.sql"
:r $(rootPath)\"Tables\GvaViewAircraftAws.sql"
:r $(rootPath)\"Tables\GvaWordTemplates.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Gva.sql"


---------------------------------------------------------------
-- Insert Migration Necessities
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\migrationUser.sql"

---------------------------------------------------------------
-- Insert Static Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\gvaCaseTypes.sql"
:r $(rootPath)\"..\Insert\gvaWordTemplates.sql"
:r $(rootPath)\"..\Insert\noms.sql"
:r $(rootPath)\"..\Insert\boolean.sql"
:r $(rootPath)\"..\Insert\documentParts.sql"
:r $(rootPath)\"..\Insert\linTypes.sql"
:r $(rootPath)\"..\Insert\testScores.sql"

---------------------------------------------------------------
-- Insert Migration Noms Values
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\migrationNomValues.sql"
--DBCC CHECKIDENT ('NomValues', RESEED, 999)

---------------------------------------------------------------
-- Aircrafts test noms from Apex
---------------------------------------------------------------
--:r $(rootPath)\"..\Insert\Noms\aircraftCategories.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftProducers.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftRelations.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftParts.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftPartProducers.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftPartStatuses.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftDebtTypes.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftCreditors.sql"
--:r $(rootPath)\"..\Insert\Noms\inspectors.sql"
--:r $(rootPath)\"..\Insert\Noms\lim145limitations.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftOccurrenceClasses.sql"
--:r $(rootPath)\"..\Insert\Noms\auditReasons.sql"
--:r $(rootPath)\"..\Insert\Noms\auditTypes.sql"
--:r $(rootPath)\"..\Insert\Noms\auditStates.sql"
--:r $(rootPath)\"..\Insert\Noms\otherDocumentTypes.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftCertificateTypes.sql"
:r $(rootPath)\"..\Insert\Noms\registers.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftNewOld.sql"
--:r $(rootPath)\"..\Insert\Noms\operationTypes.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftTypeCertificateTypes.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftLimitations.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftRegStatuses.sql"
--:r $(rootPath)\"..\Insert\Noms\examiners.sql"
--:r $(rootPath)\"..\Insert\Noms\aircraftRadioTypes.sql"

---------------------------------------------------------------
-- Aircrafts test noms from FM
---------------------------------------------------------------
--:r $(rootPath)\"..\Insert\Noms\cofATypes.sql"
--:r $(rootPath)\"..\Insert\Noms\easaTypes.sql"
--:r $(rootPath)\"..\Insert\Noms\easaCategories.sql"
--:r $(rootPath)\"..\Insert\Noms\euRegTypes.sql"
--:r $(rootPath)\"..\Insert\Noms\removalReasons.sql"

---------------------------------------------------------------
-- Aircrafts test noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\easaTypes.sql"
:r $(rootPath)\"..\Insert\Noms\airworthinessCertificateTypes.sql"
:r $(rootPath)\"..\Insert\Noms\airworthinessReviewTypes.sql"

---------------------------------------------------------------
-- Organizations test noms
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\Noms\disparityLevels.sql"
:r $(rootPath)\"..\Insert\Noms\recommendationPartNumbers.sql"
:r $(rootPath)\"..\Insert\Noms\aircarrierServices.sql"

---------------------------------------------------------------
-- Equipments test noms
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\Noms\equipmentProducers.sql"
:r $(rootPath)\"..\Insert\Noms\equipmentTypes.sql"

---------------------------------------------------------------
-- AS Exams test noms
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\Noms\commonQuestions.sql"
:r $(rootPath)\"..\Insert\Noms\specializedQuestions.sql"


---------------------------------------------------------------
--Insert GVA EXCEL CONFIG
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\GvaExcelConfig\DocTypeGroups.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\Units.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\UnitRelations.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\RegisterIndexes.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\DocTypes.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\DocFileTypes.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\IrregularityTypes.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\Classifications.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\ClassificationRelations.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\UnitClassifications.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\DocTypeUnitRoles.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\DocTypeClassifications.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\ElectronicServiceStages.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\ElectronicServiceStageExecutors.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\Users.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\ConfigFinalize.sql"

:r $(rootPath)\"..\Insert\GvaCorrespondents.sql"
