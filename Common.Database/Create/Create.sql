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

-- System
:r $(rootPath)"\Tables\System\GParams.sql"
:r $(rootPath)"\Tables\System\ActionLogs.sql"

-- Users
:r $(rootPath)"\Tables\Users\Users.sql"
:r $(rootPath)"\Tables\Users\Roles.sql"
:r $(rootPath)"\Tables\Users\UserRoles.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

--:r $(rootPath)"\Diagram\Persons.sql"