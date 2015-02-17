PRINT 'GvaExSystExaminees'
GO 

CREATE TABLE [dbo].[GvaExSystExaminees] (
    [GvaExSystExamineeId]    INT             NOT NULL IDENTITY,
    [Uin]                    NVARCHAR(50)    NULL,
    [Lin]                    INT             NULL,
    [ExamCode]               NVARCHAR(200)   NOT NULL,
    [EndTime]                DATETIME2       NOT NULL,
    [TotalScore]             NVARCHAR(10)    NULL,
    [ResultStatus]           NVARCHAR(50)    NOT NULL,
    [CertCampCode]           NVARCHAR(200)   NULL,
    [LotId]                  INT             NOT NULL,
    CONSTRAINT [PK_GvaExSystExaminees] PRIMARY KEY ([GvaExSystExamineeId]),
    CONSTRAINT [FK_GvaExSystExaminees_GvaViewPersons] FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO

exec spDescTable  N'GvaExSystExaminees', N'Изпитвани лица от изпитната система.'
exec spDescColumn N'GvaExSystExaminees', N'Uin'                    , N'EГН.'
exec spDescColumn N'GvaExSystExaminees', N'Lin'                    , N'ЛИН.'
exec spDescColumn N'GvaExSystExaminees', N'ExamCode'               , N'Код на изпита.'
exec spDescColumn N'GvaExSystExaminees', N'EndTime'                , N'Край на изпита'
exec spDescColumn N'GvaExSystExaminees', N'TotalScore'             , N'Резултат от изпита.'
exec spDescColumn N'GvaExSystExaminees', N'ResultStatus'           , N'Статус на резултат.'
exec spDescColumn N'GvaExSystExaminees', N'CertCampCode'           , N'Наименование на сертификационната кампания.'
GO
