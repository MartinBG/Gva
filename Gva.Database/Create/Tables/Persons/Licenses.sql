print 'Licenses'
GO

CREATE TABLE [dbo].[Licenses] (
    [LicenseId]                   INT             NOT NULL IDENTITY(1,1),
    [PersonId]                    INT             NOT NULL,
    [LicenseTypeId]               INT             NULL,
    [LicenseNumber]               NVARCHAR (50)   NULL,
    [IssueData]                   DATE            NULL,
    [ForeignLicenseNumber]        NVARCHAR (50)   NULL,
    [ForeignCaaId]                INT             NULL,
    [ForeignLicenseEmploymentId]  INT             NULL,
    [Notes]                       NVARCHAR (500)  NULL,
    [IsValid]                     BIT             NOT NULL,
    [Version]                     ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Licenses]              PRIMARY KEY ([LicenseId]),
    CONSTRAINT [FK_Licenses_Persons]      FOREIGN KEY ([PersonId])                    REFERENCES [dbo].[Persons]      ([PersonId]),
    CONSTRAINT [FK_Licenses_LicenseTypes] FOREIGN KEY ([LicenseTypeId])               REFERENCES [dbo].[LicenseTypes] ([LicenseTypeId]),
    CONSTRAINT [FK_Licenses_Caas]         FOREIGN KEY ([ForeignCaaId])                REFERENCES [dbo].[Caas]         ([CaaId]),
    CONSTRAINT [FK_Licenses_Employments]  FOREIGN KEY ([ForeignLicenseEmploymentId])  REFERENCES [dbo].[Employments]  ([EmploymentId])
);
GO

exec spDescTable  N'Licenses'                                , N'Лицензи за правоспособност.'
exec spDescColumn N'Licenses', N'LicenseId'                  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Licenses', N'PersonId'                   , N'Физическо лице.'
exec spDescColumn N'Licenses', N'LicenseTypeId'              , N'Вид(типове) правоспособност.'
exec spDescColumn N'Licenses', N'LicenseNumber'              , N'Номер на лиценз за правоспособност.'
exec spDescColumn N'Licenses', N'ForeignLicenseNumber'       , N'Чужестранен лицензен номер, който се признава с този лиценз.'
exec spDescColumn N'Licenses', N'ForeignCaaId'               , N'Чужда организация издател на чужестранен лицензен номер, който се признава с този лиценз.'
exec spDescColumn N'Licenses', N'ForeignLicenseEmploymentId' , N'Месторабота при признаване на чужд лиценз.'
exec spDescColumn N'Licenses', N'Notes'                      , N'Бележки.'
exec spDescColumn N'Licenses', N'IsValid'                    , N'Валидност.'
GO
