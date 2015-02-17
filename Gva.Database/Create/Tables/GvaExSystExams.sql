PRINT 'GvaExSystExams'
GO 

CREATE TABLE [dbo].[GvaExSystExams] (
    [Name]              NVARCHAR(200)  NOT NULL,
    [Code]              NVARCHAR(200)  NOT NULL UNIQUE,
    [QualificationCode] NVARCHAR(200)  NOT NULL,
    CONSTRAINT [PK_GvaExSystExams] PRIMARY KEY ([Code])
)
GO

exec spDescTable  N'GvaExSystExams', N'Тестове от изпитната система.'
exec spDescColumn N'GvaExSystExams', N'Name'                    , N'Наименование.'
exec spDescColumn N'GvaExSystExams', N'Code'                    , N'Код.'
exec spDescColumn N'GvaExSystExams', N'QualificationCode'       , N'Код на квалификация.'
GO
