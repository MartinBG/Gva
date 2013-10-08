print 'ExperienceMeasures'
GO

CREATE TABLE [dbo].[ExperienceMeasures] (
    [ExperienceMeasureId]       INT             NOT NULL IDENTITY(1,1),
    [Code]                      NVARCHAR (50)   NULL,
    [Name]                      NVARCHAR (50)   NULL,
    [Version]                   ROWVERSION      NOT NULL,
    CONSTRAINT [PK_ExperienceMeasures] PRIMARY KEY ([ExperienceMeasureId])
);
GO

exec spDescTable  N'ExperienceMeasures'                          , N'Видове летателен опит.'
exec spDescColumn N'ExperienceMeasures', N'ExperienceMeasureId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ExperienceMeasures', N'Code'                 , N'Код.'
exec spDescColumn N'ExperienceMeasures', N'Name'                 , N'Наименование.'
GO
