﻿USE [$(dbName)]
GO

---------------------------------------------------------------
-- Tables
---------------------------------------------------------------

--Roles
:r $(rootPath)"\Tables\Users\RoleClassifications.sql"
---------------------------------------------------------------

:r $(rootPath)\"Tables\GvaCaseTypes.sql"
:r $(rootPath)\"Tables\GvaLotCases.sql"
:r $(rootPath)\"Tables\GvaApplications.sql"
:r $(rootPath)\"Tables\GvaCorrespondents.sql"
:r $(rootPath)\"Tables\GvaFiles.sql"
:r $(rootPath)\"Tables\GvaLotFiles.sql"
:r $(rootPath)\"Tables\GvaAppLotFiles.sql"
:r $(rootPath)\"Tables\GvaViewOrganizations.sql"
:r $(rootPath)\"Tables\GvaViewPersons.sql"
:r $(rootPath)\"Tables\GvaViewPersonInspectors.sql"
:r $(rootPath)\"Tables\GvaViewPersonExaminers.sql"
:r $(rootPath)\"Tables\GvaViewPersonLicences.sql"
:r $(rootPath)\"Tables\GvaViewPersonLicenceEditions.sql"
:r $(rootPath)\"Tables\GvaViewPrintedRatingEditions.sql"
:r $(rootPath)\"Tables\GvaViewPersonRatings.sql"
:r $(rootPath)\"Tables\GvaViewPersonRatingEditions.sql"
:r $(rootPath)\"Tables\GvaViewPersonDocuments.sql"
:r $(rootPath)\"Tables\GvaViewPersonChecks.sql"
:r $(rootPath)\"Tables\GvaViewPersonReports.sql"
:r $(rootPath)\"Tables\GvaViewPersonReportsChecks.sql"
:r $(rootPath)\"Tables\GvaViewInventoryItems.sql"
:r $(rootPath)\"Tables\GvaViewApplications.sql"
:r $(rootPath)\"Tables\GvaViewPersonApplicationExams.sql"
:r $(rootPath)\"Tables\GvaViewPersonQualifications.sql"
:r $(rootPath)\"Tables\GvaViewOrganizationRecommendations.sql"
:r $(rootPath)\"Tables\GvaViewOrganizationInspections.sql"
:r $(rootPath)\"Tables\GvaViewOrganizationInspectionsRecommendations.sql"
:r $(rootPath)\"Tables\GvaViewOrganizationApprovals.sql"
:r $(rootPath)\"Tables\GvaViewOrganizationAmendments.sql"
:r $(rootPath)\"Tables\GvaViewAirports.sql"
:r $(rootPath)\"Tables\GvaViewEquipments.sql"
:r $(rootPath)\"Tables\GvaViewAircrafts.sql"
:r $(rootPath)\"Tables\GvaViewAircraftRegistrations.sql"
:r $(rootPath)\"Tables\GvaViewAircraftRegMarks.sql"
:r $(rootPath)\"Tables\GvaViewAircraftCerts.sql"
:r $(rootPath)\"Tables\GvaWordTemplates.sql"
:r $(rootPath)\"Tables\GvaPapers.sql"
:r $(rootPath)\"Tables\ASExamQuestions.sql"
:r $(rootPath)\"Tables\ASExamVariants.sql"
:r $(rootPath)\"Tables\ASExamVariantQuestions.sql"
:r $(rootPath)\"Tables\GvaStages.sql"
:r $(rootPath)\"Tables\GvaAppStages.sql"
:r $(rootPath)\"Tables\GvaExSystQualifications.sql"
:r $(rootPath)\"Tables\GvaExSystExams.sql"
:r $(rootPath)\"Tables\GvaExSystCertPaths.sql"
:r $(rootPath)\"Tables\GvaExSystCertCampaigns.sql"
:r $(rootPath)\"Tables\GvaExSystExaminees.sql"
:r $(rootPath)\"Tables\GvaInvalidActNumbers.sql"
:r $(rootPath)\"Tables\GvaViewSModeCodes.sql"

---------------------------------------------------------------
-- Views
---------------------------------------------------------------

:r $(rootPath)\"Views\vwGvaLicenceEditions.sql"

---------------------------------------------------------------
-- Procedures
---------------------------------------------------------------

:r $(rootPath)\"Procedures\spSetLotPartTokens.sql"
:r $(rootPath)\"Procedures\spRebuildLotPartTokens.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Gva.sql"
:r $(rootPath)\"Diagram\GvaViews.sql"

---------------------------------------------------------------
--Insert GVA EXCEL CONFIG
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\GvaExcelConfig\DocTypeGroups.sql"
:r $(rootPath)\"..\Insert\GvaExcelConfig\ElectronicServiceProviders.sql"
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

---------------------------------------------------------------
-- Lots
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Lots\person.sql"
:r $(rootPath)\"..\Insert\Lots\organization.sql"
:r $(rootPath)\"..\Insert\Lots\aircraft.sql"
:r $(rootPath)\"..\Insert\Lots\airport.sql"
:r $(rootPath)\"..\Insert\Lots\equipment.sql"
:r $(rootPath)\"..\Insert\Lots\sModeCode.sql"

---------------------------------------------------------------
-- SystemData
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\migrationUser.sql"
:r $(rootPath)\"..\Insert\gvaCaseTypes.sql"
:r $(rootPath)\"..\Insert\gvaWordTemplates.sql"
:r $(rootPath)\"..\Insert\gvaPapers.sql"
:r $(rootPath)\"..\Insert\gvaStages.sql"

---------------------------------------------------------------
-- Aircrafts Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\Aircrafts\_aircraftsMigrationNoms.sql"
:r $(rootPath)\"..\Insert\Noms\Aircrafts\aircraftRemovalReasonsFm.sql"
:r $(rootPath)\"..\Insert\Noms\Aircrafts\airworthinessCertificateTypes.sql"
:r $(rootPath)\"..\Insert\Noms\Aircrafts\inspectorTypes.sql"
:r $(rootPath)\"..\Insert\Noms\Aircrafts\registers.sql"

---------------------------------------------------------------
-- Airports Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\Airports\_airportsMigrationNoms.sql"

---------------------------------------------------------------
-- Common Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\Common\_commonMigrationNoms.sql"
:r $(rootPath)\"..\Insert\Noms\Common\documentParts.sql"

---------------------------------------------------------------
-- Equipments Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\Equipments\equipmentProducers.sql"
:r $(rootPath)\"..\Insert\Noms\Equipments\equipmentTypes.sql"

---------------------------------------------------------------
-- Organizations Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\Organizations\_organizationsMigrationNoms.sql"
:r $(rootPath)\"..\Insert\Noms\Organizations\aircarrierServices.sql"
:r $(rootPath)\"..\Insert\Noms\Organizations\disparityLevels.sql"
:r $(rootPath)\"..\Insert\Noms\Organizations\recommendationPartNumbers.sql"
:r $(rootPath)\"..\Insert\Noms\Organizations\testScores.sql"

---------------------------------------------------------------
-- Persons Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\Persons\_personsMigrationNoms.sql"
:r $(rootPath)\"..\Insert\Noms\Persons\linTypes.sql"
:r $(rootPath)\"..\Insert\Noms\Persons\ratingNotes.sql"
:r $(rootPath)\"..\Insert\Noms\Persons\asExamQuestionTypes.sql"
:r $(rootPath)\"..\Insert\Noms\Persons\instructorExaminerCertificateAttachmentAuthorizations.sql"
:r $(rootPath)\"..\Insert\Noms\Persons\instructorExaminerCertificateAttachmentPrivileges.sql"
:r $(rootPath)\"..\Insert\asExams.sql"

---------------------------------------------------------------
-- SModeCodes Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\SModeCodes\sModeCodeTypes.sql"

---------------------------------------------------------------
--Insert ADDITIONAL
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Users\Roles.sql"
:r $(rootPath)\"..\Insert\Users\UserRoles.sql"
:r $(rootPath)\"..\Insert\Users\RoleClassifications.sql"
