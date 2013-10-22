﻿USE [$(dbName)]
GO

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

:r $(rootPath)\"Tables\GvaApplications.sql"
:r $(rootPath)\"Tables\GvaFiles.sql"
:r $(rootPath)\"Tables\GvaLotFileTypes.sql"
:r $(rootPath)\"Tables\GvaLotFiles.sql"
:r $(rootPath)\"Tables\GvaAppLotFiles.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Gva.sql"
