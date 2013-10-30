USE [$(dbName)]
GO

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

:r $(rootPath)\"Tables\GvaApplications.sql"
:r $(rootPath)\"Tables\GvaFiles.sql"
:r $(rootPath)\"Tables\GvaLotFileTypes.sql"
:r $(rootPath)\"Tables\GvaLotFiles.sql"
:r $(rootPath)\"Tables\GvaAppLotFiles.sql"
:r $(rootPath)\"Tables\GvaFlyingExps.sql"
:r $(rootPath)\"Tables\GvaRating.sql"
:r $(rootPath)\"Tables\GvaRatingDates.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

:r $(rootPath)\"Diagram\Gva.sql"
