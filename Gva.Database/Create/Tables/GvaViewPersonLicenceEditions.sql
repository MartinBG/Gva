PRINT 'GvaViewPersonLicenceEditions'
GO 

CREATE TABLE [dbo].[GvaViewPersonLicenceEditions] (
    [LotId]                 INT           NOT NULL,
    [LotPartId]             INT           NOT NULL,
    [EditionIndex]          INT           NOT NULL,
    [LicenceTypeId]         INT           NOT NULL,
    [StampNumber]           NVARCHAR(50)  NULL,
    [DateValidFrom]         DATETIME2     NOT NULL,
    [DateValidTo]           DATETIME2     NOT NULL,
    [LicenceActionId]       INT           NOT NULL,
    [LicenceNumber]         NVARCHAR(50)  NULL,
    [LicenceNumber]         INT           NOT NULL,
    [IsLastEdition]         BIT           NOT NULL,
    [GvaApplicationId]      INT           NULL,
    [ApplicationName]       NVARCHAR(MAX) NULL,
    [ApplicationPartIndex]  INT           NULL,
    CONSTRAINT [PK_GvaViewPersonLicenceEditions]                      PRIMARY KEY ([LotId], [LotPartId], [EditionIndex]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_GvaViewPersons]       FOREIGN KEY ([LotId])                                    REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_LotParts]             FOREIGN KEY ([LotPartId])                                REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_NomValues]            FOREIGN KEY ([LicenceTypeId])                            REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_NomValues2]           FOREIGN KEY ([LicenceActionId])                          REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_GvaApplications]      FOREIGN KEY ([GvaApplicationId])                         REFERENCES [dbo].[GvaApplications] ([GvaApplicationId])
)
GO

exec spDescTable  N'GvaViewPersonLicenceEditions', N'Данни за вписване по лиценз.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LotId'                 , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LotPartId'             , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'EditionIndex'          , N'Индекс на вписването.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicenceTypeId'         , N'Вид лиценз.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'StampNumber'           , N'Номер на печата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'DateValidFrom'         , N'Документа е валиден от.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'DateValidTo'           , N'Документа е валиден до.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicenceActionId'       , N'Основание.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicenceNumber'         , N'Номер на лиценза.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'IsLastEdition'         , N'Флаг, определящ дали вписаването е последно за лиценза.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'GvaApplicationId'      , N'Идентификатор на заявление.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'ApplicationName'       , N'Име на заявлението, свързано с това вписване.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'ApplicationPartIndex'  , N'Индекс на заявлението, свързано с това вписване.'
GO
