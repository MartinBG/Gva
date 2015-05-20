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

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Units.sql"

---------------------------------------------------------------
--Functions
---------------------------------------------------------------

:r $(rootPath)\"Functions\ufnParseJSON.sql"
:r $(rootPath)\"Functions\ufnGetNomValuesByTextContentProperty.sql"

---------------------------------------------------------------
--Procedures
---------------------------------------------------------------


---------------------------------------------------------------
-- Insert
---------------------------------------------------------------

:r $(rootPath)\"..\Insert\Noms\boolean.sql"
:r $(rootPath)\"..\Insert\Addresses\districts.sql"
:r $(rootPath)\"..\Insert\Addresses\municipalities.sql"
:r $(rootPath)\"..\Insert\Addresses\settlements.sql"
:r $(rootPath)\"..\Insert\Addresses\countries.sql"
:r $(rootPath)\"..\Insert\TestBlobs.sql"
:r $(rootPath)\"..\Insert\TestUsers.sql"
