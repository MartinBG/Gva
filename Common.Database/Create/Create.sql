USE [$(dbName)]
GO

---------------------------------------------------------------
--Tools
---------------------------------------------------------------

:r $(rootPath)\"Tools\Tool_ScriptDiagram2008.sql"
:r $(rootPath)\"Tools\spDesc.sql"
:r $(rootPath)\"Tools\sp_generate_inserts.sql"

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

--Addresses
:r $(rootPath)"\Tables\Addresses\Districts.sql"
:r $(rootPath)"\Tables\Addresses\Municipalities.sql"
:r $(rootPath)"\Tables\Addresses\Settlements.sql"
:r $(rootPath)"\Tables\Addresses\Countries.sql"

--Files
:r $(rootPath)"\Tables\Files\Blobs.sql"

-- System
:r $(rootPath)"\Tables\System\GParams.sql"
:r $(rootPath)"\Tables\System\Logs.sql"

-- Users
:r $(rootPath)"\Tables\Users\Users.sql"
:r $(rootPath)"\Tables\Users\Roles.sql"
:r $(rootPath)"\Tables\Users\UserRoles.sql"

-- Noms
:r $(rootPath)\"Tables\Noms\Noms.sql"
:r $(rootPath)\"Tables\Noms\NomValues.sql"


-- Classifications
:r $(rootPath)"\Tables\Classifications\Classifications.sql"
:r $(rootPath)"\Tables\Classifications\ClassificationRelations.sql"
:r $(rootPath)"\Tables\Classifications\ClassificationPermissions.sql"

-- Units
:r $(rootPath)"\Tables\Units\UnitTypes.sql"
:r $(rootPath)"\Tables\Units\Units.sql"
:r $(rootPath)"\Tables\Units\UnitRelations.sql"
:r $(rootPath)"\Tables\Units\UnitClassifications.sql"
:r $(rootPath)"\Tables\Units\UnitUsers.sql"

---------------------------------------------------------------
--Functions
---------------------------------------------------------------

:r $(rootPath)\"Functions\ufnParseJSON.sql"
:r $(rootPath)\"Functions\ufnGetNomValuesByTextContentProperty.sql"

---------------------------------------------------------------
-- Insert
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\boolean.sql"
:r $(rootPath)\"..\Insert\TestBlobs.sql"
:r $(rootPath)\"..\Insert\TestUsers.sql"

:r $(rootPath)\"..\Insert\Classifications\ClassificationPermissions.sql"
