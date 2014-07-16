PRINT 'GvaStages'
GO 

CREATE TABLE [dbo].[GvaStages](
    [GvaStageId]       INT           NOT NULL,
    [Name]             NVARCHAR(200) NULL,
    [Alias]            NVARCHAR(200) NULL,
    CONSTRAINT [PK_GvaStages] PRIMARY KEY ([GvaStageId])
)
GO

exec spDescTable  N'GvaStages', N'Файлове'
exec spDescColumn N'GvaStages', N'GvaStageId'       , N'Уникален системно генериран идентификатор'
exec spDescColumn N'GvaStages', N'Name'             , N'Наименование'
exec spDescColumn N'GvaStages', N'Alias'            , N'Псевдоним'
GO
