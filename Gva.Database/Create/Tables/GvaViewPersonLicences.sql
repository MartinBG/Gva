PRINT 'GvaViewPersonLicences'
GO 

CREATE TABLE [dbo].[GvaViewPersonLicences] (
    [LotId]          INT           NOT NULL,
    [PartIndex]      INT           NOT NULL,
    [LicenceTypeId]  INT           NOT NULL,
    CONSTRAINT [PK_GvaViewPersonLicences]                 PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonLicences_GvaViewPersons]  FOREIGN KEY ([LotId])            REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonLicences_NomValues]       FOREIGN KEY ([LicenceTypeId])    REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewPersonLicences', N'Лицензи на физически лица.'
exec spDescColumn N'GvaViewPersonLicences', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonLicences', N'PartIndex'                 , N'Индекс на част от партида.'
exec spDescColumn N'GvaViewPersonLicences', N'LicenceTypeId'             , N'Тип лиценз.'
GO