PRINT 'GvaViewPersons'
GO 

CREATE TABLE [dbo].[GvaViewPersons] (
    [LotId]            INT           NOT NULL,
    [Lin]              INT           NOT NULL UNIQUE,
    [LinTypeId]        INT           NOT NULL,
    [Uin]              NVARCHAR(50)  NULL,
    [Names]            NVARCHAR(MAX) NOT NULL,
    [RatingCodes]      NVARCHAR(MAX) NULL,
    [LicenceCodes]     NVARCHAR(MAX) NULL,
    [BirtDate]         DATETIME2     NOT NULL,
    [OrganizationId]   INT           NULL,
    [EmploymentId]     INT           NULL,
    CONSTRAINT [PK_GvaViewPersons]                      PRIMARY KEY ([LotId]),
    CONSTRAINT [FK_GvaViewPersons_Lots]                 FOREIGN KEY ([LotId])          REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewPersons_NomValues]            FOREIGN KEY ([LinTypeId])      REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersons_GvaViewOrganizations] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[GvaViewOrganizations] ([LotId]),
    CONSTRAINT [FK_GvaViewPersons_NomValues2]           FOREIGN KEY ([EmploymentId])   REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [UQ_IDX_GvaViewPersons_Uin]
ON [dbo].[GvaViewPersons](Uin)
WHERE Uin IS NOT NULL;
GO

exec spDescTable  N'GvaViewPersons', N'Физически лица.'
exec spDescColumn N'GvaViewPersons', N'LotId'         , N'Идентификатор на партида на физическо лице.'
exec spDescColumn N'GvaViewPersons', N'Lin'           , N'Личен идентификационен номер.'
exec spDescColumn N'GvaViewPersons', N'LinTypeId'     , N'Тип лин.'
exec spDescColumn N'GvaViewPersons', N'Uin'           , N'Единен граждански номер.'
exec spDescColumn N'GvaViewPersons', N'Names'         , N'Имена.'
exec spDescColumn N'GvaViewPersons', N'RatingCodes'   , N'Квалификации.'
exec spDescColumn N'GvaViewPersons', N'LicenceCodes'  , N'Лицензи.'
exec spDescColumn N'GvaViewPersons', N'BirtDate'      , N'Дата на раждане.'
exec spDescColumn N'GvaViewPersons', N'OrganizationId', N'Фирма.'
exec spDescColumn N'GvaViewPersons', N'EmploymentId'  , N'Длъжност.'
GO
