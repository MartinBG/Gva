print 'MedicalCertificates'
GO

CREATE TABLE [dbo].[MedicalCertificates] (
    [MedicalCertificateId] INT             NOT NULL IDENTITY(1,1),
    [PersonId]             INT             NOT NULL,
    [Prefix]               NVARCHAR (50)   NULL,
    [NumberPrefix]         NVARCHAR (50)   NULL,
    [NumberSuffix]         NVARCHAR (50)   NULL,
    [IssueDate]            DATE            NULL,
    [ValidDate]            DATE            NULL,
    [MedicalClassId]       INT             NULL,
    [PublisherName]        NVARCHAR (200)  NULL,
    [Notes]                NVARCHAR (500)  NULL,
    [Version]              ROWVERSION      NOT NULL,
    CONSTRAINT [PK_MedicalCertificates]                PRIMARY KEY ([MedicalCertificateId]),
    CONSTRAINT [FK_MedicalCertificates_Persons]        FOREIGN KEY ([PersonId])              REFERENCES [dbo].[Persons]        ([PersonId]),
    CONSTRAINT [FK_MedicalCertificates_MedicalClasses] FOREIGN KEY ([MedicalClassId])        REFERENCES [dbo].[MedicalClasses] ([MedicalClassId])
);
GO

exec spDescTable  N'MedicalCertificates'                          , N'Класове/Вписани квалификации.'
exec spDescColumn N'MedicalCertificates', N'MedicalCertificateId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MedicalCertificates', N'PersonId'             , N'Физическо лице.'
exec spDescColumn N'MedicalCertificates', N'Prefix'               , N'Префикс.'
exec spDescColumn N'MedicalCertificates', N'NumberPrefix'         , N'Префикс на номер.'
exec spDescColumn N'MedicalCertificates', N'NumberSuffix'         , N'Суфикс на номер.'
exec spDescColumn N'MedicalCertificates', N'IssueDate'            , N'Дата.'
exec spDescColumn N'MedicalCertificates', N'ValidDate'            , N'Валидно до.'
exec spDescColumn N'MedicalCertificates', N'MedicalClassId'       , N'Клас.'
exec spDescColumn N'MedicalCertificates', N'PublisherName'        , N'Издател.'
exec spDescColumn N'MedicalCertificates', N'Notes'                , N'Бележки.'
GO
