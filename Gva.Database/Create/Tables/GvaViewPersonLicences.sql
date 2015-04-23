PRINT 'GvaViewPersonLicences'
GO 

CREATE TABLE [dbo].[GvaViewPersonLicences] (
    [LotId]                 INT           NOT NULL,
    [PartId]                INT           NOT NULL,
    [PartIndex]             INT           NOT NULL,
    [LicenceTypeId]         INT           NOT NULL,
    [LicenceNumber]         INT           NULL,
    [Valid]                 BIT           NULL,
    [LicenceTypeCaCode]     NVARCHAR(50)  NOT NULL,
    [PublisherCode]         NVARCHAR(50)  NOT NULL,
    [ForeignLicenceNumber]  NVARCHAR(50)  NULL,
    [ForeignPublisher]      NVARCHAR(100) NULL,
    [StatusChange]          NVARCHAR(100) NULL,
    CONSTRAINT [PK_GvaViewPersonLicences]                      PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonLicences_GvaViewPersons]       FOREIGN KEY ([LotId])                REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonLicences_LotParts]             FOREIGN KEY ([PartId])               REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonLicences_NomValues]            FOREIGN KEY ([LicenceTypeId])        REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewPersonLicences', N'Данни за вписване по лиценз.'
exec spDescColumn N'GvaViewPersonLicences', N'LotId'                 , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewPersonLicences', N'PartId'                , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewPersonLicences', N'PartIndex'             , N'Идентификатор на индекс на лиценза.'
exec spDescColumn N'GvaViewPersonLicences', N'LicenceTypeId'         , N'Вид лиценз.'
exec spDescColumn N'GvaViewPersonLicences', N'LicenceNumber'         , N'Номер на лиценза.'
exec spDescColumn N'GvaViewPersonLicences', N'Valid'                 , N'Валидност на лиценза.'
exec spDescColumn N'GvaViewPersonLicences', N'ForeignLicenceNumber'  , N'Чужд лиценз No .'
exec spDescColumn N'GvaViewPersonLicences', N'ForeignPublisher'      , N'Чужда администрация.'
exec spDescColumn N'GvaViewPersonLicences', N'StatusChange'          , N'Промяна на статуса.'
GO
