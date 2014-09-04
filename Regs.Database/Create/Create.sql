USE [$(dbName)]
GO

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

-- Lots
:r $(rootPath)\"Tables\LotSchemas.sql"
:r $(rootPath)\"Tables\LotSets.sql"
:r $(rootPath)\"Tables\LotSetSchemas.sql"
:r $(rootPath)\"Tables\LotSetParts.sql"
:r $(rootPath)\"Tables\Lots.sql"
:r $(rootPath)\"Tables\LotParts.sql"
:r $(rootPath)\"Tables\LotPartOperations.sql"
:r $(rootPath)\"Tables\LotCommits.sql"
:r $(rootPath)\"Tables\LotPartVersions.sql"
:r $(rootPath)\"Tables\LotCommitVersions.sql"
:r $(rootPath)\"Tables\LotPartExts.sql"
:r $(rootPath)\"Tables\LotPartTokens.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Regs.sql"

---------------------------------------------------------------
-- Sequences
---------------------------------------------------------------

:r $(rootPath)\"Sequences\LotSequence.sql"
:r $(rootPath)\"Sequences\PartSequence.sql"
:r $(rootPath)\"Sequences\PartVersionSequence.sql"
:r $(rootPath)\"Sequences\CommitSequence.sql"

---------------------------------------------------------------
-- Insert
---------------------------------------------------------------
:r $(rootPath)\"..\Insert\LotPartOperations.sql"
