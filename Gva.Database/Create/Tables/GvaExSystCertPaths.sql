PRINT 'GvaExSystCertPaths'
GO 

CREATE TABLE [dbo].[GvaExSystCertPaths] (
    [Name]              NVARCHAR(200)  NOT NULL,
    [Code]              INT            NOT NULL,
    [ValidFrom]         DATETIME2      NULL,
    [ValidTo]           DATETIME2      NULL,
    [QualificationCode] NVARCHAR(200)  NOT NULL,
    [ExamCode]          NVARCHAR(200)  NOT NULL,
    CONSTRAINT [PK_GvaExSystCertPaths] PRIMARY KEY ([Code], [QualificationCode], [ExamCode]),
    CONSTRAINT [FK_GvaExSystCertPaths_GvaExSystQualifications] FOREIGN KEY ([QualificationCode]) REFERENCES [dbo].[GvaExSystQualifications] ([Code]),
    CONSTRAINT [FK_GvaExSystCertPaths_GvaExSystExams] FOREIGN KEY ( [ExamCode], [QualificationCode]) REFERENCES [dbo].[GvaExSystExams] ([Code], [QualificationCode])
)
GO

exec spDescTable  N'GvaExSystCertPaths', N'Сертификационни пътища от изпитната система.'
exec spDescColumn N'GvaExSystCertPaths', N'Name'                    , N'Наименование.'
exec spDescColumn N'GvaExSystCertPaths', N'Code'                    , N'Код.'
exec spDescColumn N'GvaExSystCertPaths', N'QualificationCode'       , N'Код на квалификация.'
exec spDescColumn N'GvaExSystCertPaths', N'ValidFrom'               , N'Дата на начало на валидност на кампанията.'
exec spDescColumn N'GvaExSystCertPaths', N'ValidTo'                 , N'Дата на край на валидност на кампанията.'
exec spDescColumn N'GvaExSystCertPaths', N'ExamCode'                , N'Код на изпита.'
GO
