PRINT 'GvaViewPersonLicenceEditions'
GO 

CREATE TABLE [dbo].[GvaViewPersonLicenceEditions] (
    [LotId]                 INT           NOT NULL,
    [LicencePartId]         INT           NOT NULL,
    [EditionPartId]         INT           NOT NULL,
    [EditionIndex]          INT           NOT NULL,
    [LicenceTypeId]         INT           NOT NULL,
    [StampNumber]           NVARCHAR(50)  NULL,
    [DateValidFrom]         DATETIME2     NOT NULL,
    [DateValidTo]           DATETIME2     NULL,
    [LicenceActionId]       INT           NOT NULL,
    [LicenceNumber]         INT           NULL,
    [IsLastEdition]         BIT           NOT NULL,
    [LicencePartIndex]      INT           NOT NULL,
    [EditionPartIndex]      INT           NOT NULL,
    [FirstDocDateValidFrom] DATETIME2     NOT NULL,
    [Valid]                 BIT           NULL,
    [LicenceTypeCode]       NVARCHAR(50)  NOT NULL,
    [LicenceTypeCaCode]     NVARCHAR(50)  NOT NULL,
    [PublisherCode]         NVARCHAR(50)  NOT NULL,
    CONSTRAINT [PK_GvaViewPersonLicenceEditions]                      PRIMARY KEY ([LotId], [LicencePartId], [EditionPartId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_GvaViewPersons]       FOREIGN KEY ([LotId])                                    REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_LotParts]             FOREIGN KEY ([LicencePartId])                            REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_LotParts2]            FOREIGN KEY ([EditionPartId])                            REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_NomValues]            FOREIGN KEY ([LicenceTypeId])                            REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_NomValues2]           FOREIGN KEY ([LicenceActionId])                          REFERENCES [dbo].[NomValues] ([NomValueId]),
)
GO

exec spDescTable  N'GvaViewPersonLicenceEditions', N'Данни за вписване по лиценз.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LotId'                 , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicencePartId'         , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'EditionPartId'         , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicencePartIndex'      , N'Идентификатор на индекс на лиценза.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'EditionPartIndex'      , N'Идентификатор на част на впсиването.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'EditionIndex'          , N'Индекс на вписването.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicenceTypeId'         , N'Вид лиценз.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'StampNumber'           , N'Номер на печата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'DateValidFrom'         , N'Документа е валиден от.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'DateValidTo'           , N'Документа е валиден до.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicenceActionId'       , N'Основание.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicenceNumber'         , N'Номер на лиценза.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'IsLastEdition'         , N'Флаг, определящ дали вписаването е последно за лиценза.'
GO
