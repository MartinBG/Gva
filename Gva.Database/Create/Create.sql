SET QUOTED_IDENTIFIER ON
GO

USE [master]
GO

:r "Create\CreateDB.sql"

USE [$(DatabaseName)]
GO

---------------------------------------------------------------
--Tools
---------------------------------------------------------------

:r "Create\Tools\Tool_ScriptDiagram2008.sql"
:r "Create\Tools\spDesc.sql"
:r "Create\Tools\sp_generate_inserts.sql"

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

-- System
:r "Create\Tables\System\GParams.sql"

-- Users
:r "Create\Tables\Users\Users.sql"
:r "Create\Tables\Users\Roles.sql"
:r "Create\Tables\Users\UserRoles.sql"

-- Noms
:r "Create\Tables\Noms\Settlements.sql"
:r "Create\Tables\Noms\Countries.sql"
:r "Create\Tables\Noms\Genders.sql"
:r "Create\Tables\Noms\AddressTypes.sql"
:r "Create\Tables\Noms\EmploymentCategories.sql"
:r "Create\Tables\Noms\EducationDegrees.sql"
:r "Create\Tables\Noms\Schools.sql"
:r "Create\Tables\Noms\StaffTypes.sql"
:r "Create\Tables\Noms\RatingClasses.sql"
:r "Create\Tables\Noms\RatingTypes.sql"
:r "Create\Tables\Noms\Authorizations.sql"
:r "Create\Tables\Noms\LicenseTypes.sql"
:r "Create\Tables\Noms\LocationIndicators.sql"
:r "Create\Tables\Noms\ExperienceRoles.sql"
:r "Create\Tables\Noms\ExperienceMeasures.sql"
:r "Create\Tables\Noms\LicenseInstanceTypes.sql"
:r "Create\Tables\Noms\Examiners.sql"
:r "Create\Tables\Noms\Caas.sql"
:r "Create\Tables\Noms\RatingGroups66.sql"
:r "Create\Tables\Noms\RatingCategories66.sql"
:r "Create\Tables\Noms\RatingModels.sql"

-- Organizations
:r "Create\Tables\Organizations\Organizations.sql"

-- Aircrafts
:r "Create\Tables\Aircrafts\Aircrafts.sql"

-- Persons
:r "Create\Tables\Persons\Persons.sql"
:r "Create\Tables\Persons\Addresses.sql"
:r "Create\Tables\Persons\Educations.sql"
:r "Create\Tables\Persons\Employments.sql"
:r "Create\Tables\Persons\FlyingExperiences.sql"
:r "Create\Tables\Persons\Licenses.sql"
:r "Create\Tables\Persons\LicenseInstances.sql"
:r "Create\Tables\Persons\Ratings.sql"
:r "Create\Tables\Persons\RatingInstances.sql"
:r "Create\Tables\Persons\LicenseInstanceRatings.sql"

---------------------------------------------------------------
-- Views
---------------------------------------------------------------

--:r "Create\Views\vwLotNumbersIndexed.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r "Create\Diagram\Persons.sql"
:r "Create\Diagram\Licenses.sql"
:r "Create\Diagram\Ratings.sql"

---------------------------------------------------------------
--Insert Address Data
---------------------------------------------------------------

--:r "Insert\AddressData\Countries.sql"
--:r "Insert\AddressData\Districts.sql"
--:r "Insert\AddressData\Municipalities.sql"
--:r "Insert\AddressData\Settlements.sql"


---------------------------------------------------------------
--Insert System Data
---------------------------------------------------------------

--:r "Insert\SystemData\GParams.sql"
