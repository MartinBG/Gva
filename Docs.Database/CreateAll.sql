SET QUOTED_IDENTIFIER ON
GO

PRINT '------ Creating Common'
:setvar rootPath "..\Common.Database\Create"
:r $(rootPath)"\CreateDB.sql"
:r $(rootPath)"\Create.sql"

PRINT '------ Creating Docs'
:setvar rootPath "..\Docs.Database\Create"
:r $(rootPath)"\Create.sql"

PRINT '------ Creating Test Data'
:setvar rootPath "..\Docs.Database\TestData"
:r $(rootPath)"\Create.sql"