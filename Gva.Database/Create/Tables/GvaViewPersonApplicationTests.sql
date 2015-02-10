PRINT 'GvaViewPersonApplicationTests'
GO 

CREATE TABLE [dbo].[GvaViewPersonApplicationTests] (
    [LotId]                INT           NOT NULL,
    [GvaApplicationId]     INT           NOT NULL,
    [CertCampCode]         NVARCHAR(50)  NOT NULL,
    [CertCampName]         NVARCHAR(50)  NOT NULL,
    [TestCode]             NVARCHAR(50)  NOT NULL,
    [TestName]             NVARCHAR(50)  NOT NULL,
    [TestDate]             DATETIME2     NOT NULL,
    CONSTRAINT [PK_GvaViewPersonApplicationTests]                 PRIMARY KEY ([LotId], [GvaApplicationId], [TestCode]),
    CONSTRAINT [FK_GvaViewPersonApplicationTests_GvaApplications] FOREIGN KEY ([GvaApplicationId]) REFERENCES [dbo].[GvaApplications] ([GvaApplicationId]),
    CONSTRAINT [FK_GvaViewPersonApplicationTests_GvaViewPersons]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO

exec spDescTable  N'GvaViewPersonApplicationTests', N'Документи на Физически лица.'
exec spDescColumn N'GvaViewPersonApplicationTests', N'LotId'                , N'Идентификатор на партида на физическо лице.'
exec spDescColumn N'GvaViewPersonApplicationTests', N'GvaApplicationId'     , N'Идентификатор на заявление.'
exec spDescColumn N'GvaViewPersonApplicationTests', N'CertCampCode'         , N'Код на сертификационната кампания.'
exec spDescColumn N'GvaViewPersonApplicationTests', N'CertCampName'         , N'Наименование на сертификационната кампания.'
exec spDescColumn N'GvaViewPersonApplicationTests', N'TestCode'             , N'Код на теста.'
exec spDescColumn N'GvaViewPersonApplicationTests', N'TestName'             , N'Наименование на теста.'
exec spDescColumn N'GvaViewPersonApplicationTests', N'TestDate'             , N'Дата на явяване на изпита.'
GO
