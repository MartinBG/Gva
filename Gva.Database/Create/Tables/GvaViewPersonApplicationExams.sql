﻿PRINT 'GvaViewPersonApplicationExams'
GO 

CREATE TABLE [dbo].[GvaViewPersonApplicationExams] (
    [LotId]                INT           NOT NULL,
    [GvaApplicationId]     INT           NOT NULL,
    [CertCampCode]         NVARCHAR(50)  NOT NULL,
    [CertCampName]         NVARCHAR(50)  NOT NULL,
    [ExamCode]             NVARCHAR(50)  NOT NULL,
    [ExamName]             NVARCHAR(50)  NOT NULL,
    [ExamDate]             DATETIME2     NOT NULL,
    CONSTRAINT [PK_GvaViewPersonApplicationExams]                 PRIMARY KEY ([LotId], [GvaApplicationId], [ExamCode]),
    CONSTRAINT [FK_GvaViewPersonApplicationExams_GvaApplications] FOREIGN KEY ([GvaApplicationId]) REFERENCES [dbo].[GvaApplications] ([GvaApplicationId]),
    CONSTRAINT [FK_GvaViewPersonApplicationExams_GvaViewPersons]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO

exec spDescTable  N'GvaViewPersonApplicationExams', N'Документи на Физически лица.'
exec spDescColumn N'GvaViewPersonApplicationExams', N'LotId'                , N'Идентификатор на партида на физическо лице.'
exec spDescColumn N'GvaViewPersonApplicationExams', N'GvaApplicationId'     , N'Идентификатор на заявление.'
exec spDescColumn N'GvaViewPersonApplicationExams', N'CertCampCode'         , N'Код на сертификационната кампания.'
exec spDescColumn N'GvaViewPersonApplicationExams', N'CertCampName'         , N'Наименование на сертификационната кампания.'
exec spDescColumn N'GvaViewPersonApplicationExams', N'ExamCode'             , N'Код на изпита.'
exec spDescColumn N'GvaViewPersonApplicationExams', N'ExamName'             , N'Наименование на изпита.'
exec spDescColumn N'GvaViewPersonApplicationExams', N'ExamDate'             , N'Дата на явяване на изпита.'
GO
