PRINT 'GvaExSystTests'
GO 

CREATE TABLE [dbo].[GvaExSystTests] (
    [Name]              NVARCHAR(200)  NOT NULL,
    [Code]              NVARCHAR(200)  NOT NULL UNIQUE,
    [QualificationCode] NVARCHAR(200)  NOT NULL,
    CONSTRAINT [PK_GvaExSystTests] PRIMARY KEY ([Code], [QualificationCode]),
    CONSTRAINT [FK_GvaExSystTests_GvaExSystQualifications] FOREIGN KEY ([QualificationCode]) REFERENCES [dbo].[GvaExSystQualifications] ([Code])
)
GO

exec spDescTable  N'GvaExSystTests', N'Тестове от изпитната система.'
exec spDescColumn N'GvaExSystTests', N'Name'                    , N'Наименование.'
exec spDescColumn N'GvaExSystTests', N'Code'                    , N'Код.'
exec spDescColumn N'GvaExSystTests', N'QualificationCode'       , N'Код на квалификация.'
GO
