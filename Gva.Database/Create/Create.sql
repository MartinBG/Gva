USE [$(dbName)]
GO

---------------------------------------------------------------
-- Tables
---------------------------------------------------------------

:r $(rootPath)\"Tables\GvaCaseTypes.sql"
:r $(rootPath)\"Tables\GvaLotCases.sql"
:r $(rootPath)\"Tables\GvaApplications.sql"
:r $(rootPath)\"Tables\GvaFiles.sql"
:r $(rootPath)\"Tables\GvaLotFiles.sql"
:r $(rootPath)\"Tables\GvaAppLotFiles.sql"
:r $(rootPath)\"Tables\GvaLotObjects.sql"
:r $(rootPath)\"Tables\GvaPersons.sql"
:r $(rootPath)\"Tables\GvaInventoryItems.sql"
:r $(rootPath)\"Tables\GvaApplicationSearches.sql"

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
