USE [$(dbName)]
GO

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

-- Lots
:r $(rootPath)\"Tables\LotSets.sql"
:r $(rootPath)\"Tables\LotSetParts.sql"
:r $(rootPath)\"Tables\Lots.sql"
:r $(rootPath)\"Tables\LotParts.sql"
:r $(rootPath)\"Tables\TextBlobs.sql"
:r $(rootPath)\"Tables\LotPartOperations.sql"
:r $(rootPath)\"Tables\LotCommits.sql"
:r $(rootPath)\"Tables\LotPartVersions.sql"
:r $(rootPath)\"Tables\LotCommitVersions.sql"
:r $(rootPath)\"Tables\LotPartExts.sql"

-- Noms
:r $(rootPath)\"Tables\Noms.sql"
:r $(rootPath)\"Tables\NomValues.sql"


---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Regs.sql"


---------------------------------------------------------------
-- Insert
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\LotPartOperations.sql"