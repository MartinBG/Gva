USE [$(dbName)]
GO

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

-- Lots
:r $(rootPath)\"Tables\LotTypes.sql"
:r $(rootPath)\"Tables\Lots.sql"
:r $(rootPath)\"Tables\LotParts.sql"

-- Noms
:r $(rootPath)\"Tables\Noms.sql"
:r $(rootPath)\"Tables\NomValues.sql"


---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Regs.sql"
