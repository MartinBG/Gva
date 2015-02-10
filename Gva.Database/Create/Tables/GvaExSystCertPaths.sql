PRINT 'GvaExSystCertPaths'
GO 

CREATE TABLE [dbo].[GvaExSystCertPaths] (
    [Name]              NVARCHAR(200)  NOT NULL,
    [Code]              INT            NOT NULL,
    [ValidFrom]         DATETIME2      NULL,
    [ValidTo]           DATETIME2      NULL,
    [QualificationCode] NVARCHAR(200)  NOT NULL,
    [TestCode]          NVARCHAR(200)  NOT NULL,
    CONSTRAINT [PK_GvaExSystCertPaths] PRIMARY KEY ([Code], [QualificationCode], [TestCode]),
    CONSTRAINT [FK_GvaExSystCertPaths_GvaExSystQualifications] FOREIGN KEY ([QualificationCode]) REFERENCES [dbo].[GvaExSystQualifications] ([Code]),
    CONSTRAINT [FK_GvaExSystCertPaths_GvaExSystTests] FOREIGN KEY ( [TestCode], [QualificationCode]) REFERENCES [dbo].[GvaExSystTests] ([Code], [QualificationCode])
)
GO

exec spDescTable  N'GvaExSystCertPaths', N'Сертификационни пътища от изпитната система.'
exec spDescColumn N'GvaExSystCertPaths', N'Name'                    , N'Наименование.'
exec spDescColumn N'GvaExSystCertPaths', N'Code'                    , N'Код.'
exec spDescColumn N'GvaExSystCertPaths', N'QualificationCode'       , N'Код на квалификация.'
exec spDescColumn N'GvaExSystCertPaths', N'ValidFrom'               , N'Дата на начало на валидност на кампанията.'
exec spDescColumn N'GvaExSystCertPaths', N'ValidTo'                 , N'Дата на край на валидност на кампанията.'
exec spDescColumn N'GvaExSystCertPaths', N'TestCode'                , N'Код на теста.'
GO
