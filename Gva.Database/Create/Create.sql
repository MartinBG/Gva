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
:r $(rootPath)\"Tables\GvaViewAircraftRegistrations.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Gva.sql"

---------------------------------------------------------------
-- Insert Noms
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\TestData.sql"
:r $(rootPath)\"..\Insert\Noms\gender.sql"
:r $(rootPath)\"..\Insert\Noms\countries.sql"
:r $(rootPath)\"..\Insert\Noms\cities.sql"
:r $(rootPath)\"..\Insert\Noms\addressTypes.sql"
:r $(rootPath)\"..\Insert\Noms\staffTypes.sql"
:r $(rootPath)\"..\Insert\Noms\employmentCategories.sql"
:r $(rootPath)\"..\Insert\Noms\graduations.sql"
:r $(rootPath)\"..\Insert\Noms\schools.sql"
:r $(rootPath)\"..\Insert\Noms\directions.sql"
:r $(rootPath)\"..\Insert\Noms\documentTypes.sql"
:r $(rootPath)\"..\Insert\Noms\documentParts.sql"
:r $(rootPath)\"..\Insert\Noms\documentRoles.sql"
:r $(rootPath)\"..\Insert\Noms\personIddocumentTypes.sql"
:r $(rootPath)\"..\Insert\Noms\personCheckDocumentRoles.sql"
:r $(rootPath)\"..\Insert\Noms\personCheckDocumentTypes.sql"
:r $(rootPath)\"..\Insert\Noms\personStatusTypes.sql"
:r $(rootPath)\"..\Insert\Noms\otherDocPublishers.sql"
:r $(rootPath)\"..\Insert\Noms\medDocPublishers.sql"
:r $(rootPath)\"..\Insert\Noms\ratingTypes.sql"
:r $(rootPath)\"..\Insert\Noms\ratingClassGroups.sql"
:r $(rootPath)\"..\Insert\Noms\ratingClasses.sql"
:r $(rootPath)\"..\Insert\Noms\ratingSubClasses.sql"
:r $(rootPath)\"..\Insert\Noms\authorizationGroups.sql"
:r $(rootPath)\"..\Insert\Noms\authorizations.sql"
:r $(rootPath)\"..\Insert\Noms\licenceTypeDictionary.sql"
:r $(rootPath)\"..\Insert\Noms\licenceTypes.sql"
:r $(rootPath)\"..\Insert\Noms\locationIndicators.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftTCHolders.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftTypes.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftTypeGroups.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftGroup66.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftClases66.sql"
:r $(rootPath)\"..\Insert\Noms\limitations66.sql"
:r $(rootPath)\"..\Insert\Noms\medClasses.sql"
:r $(rootPath)\"..\Insert\Noms\medLimitation.sql"
:r $(rootPath)\"..\Insert\Noms\experienceRoles.sql"
:r $(rootPath)\"..\Insert\Noms\experienceMeasures.sql"
:r $(rootPath)\"..\Insert\Noms\caa.sql"
:r $(rootPath)\"..\Insert\Noms\personRatingLevels.sql"
:r $(rootPath)\"..\Insert\Noms\licenceActions.sql"
:r $(rootPath)\"..\Insert\Noms\organizationTypes.sql"
:r $(rootPath)\"..\Insert\Noms\organizationKinds.sql"
:r $(rootPath)\"..\Insert\Noms\applicationTypes.sql"
:r $(rootPath)\"..\Insert\Noms\applicationpaymentTypes.sql"
:r $(rootPath)\"..\Insert\Noms\currencies.sql"
:r $(rootPath)\"..\Insert\Noms\yesNoOptions.sql"

---------------------------------------------------------------
-- Aircrafts test noms from Apex
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\Noms\aircraftCategories.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftProducers.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftRelations.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftParts.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftPartProducers.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftPartStatuses.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftDebtTypes.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftCreditors.sql"
:r $(rootPath)\"..\Insert\Noms\inspectors.sql"
:r $(rootPath)\"..\Insert\Noms\lim145limitations.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftOccurrenceClasses.sql"
:r $(rootPath)\"..\Insert\Noms\auditReasons.sql"
:r $(rootPath)\"..\Insert\Noms\auditTypes.sql"
:r $(rootPath)\"..\Insert\Noms\auditStates.sql"
:r $(rootPath)\"..\Insert\Noms\otherDocumentTypes.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftCertificateTypes.sql"
:r $(rootPath)\"..\Insert\Noms\registers.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftNewOld.sql"
:r $(rootPath)\"..\Insert\Noms\operationTypes.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftTypeCertificateTypes.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftLimitations.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftRegStatuses.sql"
:r $(rootPath)\"..\Insert\Noms\examiners.sql"
:r $(rootPath)\"..\Insert\Noms\aircraftRadioTypes.sql"

---------------------------------------------------------------
-- Aircrafts test noms from FM
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\Noms\cofATypes.sql"
:r $(rootPath)\"..\Insert\Noms\easaTypes.sql"
:r $(rootPath)\"..\Insert\Noms\easaCategories.sql"
:r $(rootPath)\"..\Insert\Noms\euRegTypes.sql"
:r $(rootPath)\"..\Insert\Noms\removalReasons.sql"
