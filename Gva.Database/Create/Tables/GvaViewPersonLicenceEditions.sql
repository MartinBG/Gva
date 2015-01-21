PRINT 'GvaViewPersonLicenceEditions'
GO 

CREATE TABLE [dbo].[GvaViewPersonLicenceEditions] (
    [LotId]                 INT              NOT NULL,
    [PartId]                INT              NOT NULL,
    [PartIndex]             INT              NOT NULL,
    [LicencePartIndex]      INT              NOT NULL,
    [Index]                 INT              NOT NULL,
    [IsLastEdition]         BIT              NOT NULL,
    [StampNumber]           NVARCHAR(50)     NULL,
    [FirstDocDateValidFrom] DATETIME2        NOT NULL,
    [DateValidFrom]         DATETIME2        NOT NULL,
    [DateValidTo]           DATETIME2        NULL,
    [LicenceActionId]       INT              NOT NULL,
    [Notes]                 NVARCHAR(MAX)    NULL,
    [Inspector]             NVARCHAR(100)    NULL,
    [Limitations]           NVARCHAR(MAX)    NULL,
    [PrintedFileId]         INT              NULL,
    CONSTRAINT [PK_GvaViewPersonLicenceEditions]                         PRIMARY KEY ([LotId], [LicencePartIndex], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_LotParts]                FOREIGN KEY ([PartId])                                 REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_NomValues]               FOREIGN KEY ([LicenceActionId])                        REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_GvaViewPersonLicences]   FOREIGN KEY ([LotId], [LicencePartIndex])              REFERENCES [dbo].[GvaViewPersonLicences] ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonLicenceEditions_GvaFiles]                FOREIGN KEY ([PrintedFileId])                          REFERENCES [dbo].[GvaFiles] ([GvaFileId])
)
GO

exec spDescTable  N'GvaViewPersonLicenceEditions', N'Данни за вписване по лиценз.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LotId'                 , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'PartIndex'             , N'Идентификатор на част на впсиването.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicencePartIndex'      , N'Идентификатор на индекс на лиценза.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'Index'                 , N'Индекс на вписването.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'StampNumber'           , N'Номер на печата.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'FirstDocDateValidFrom' , N'Стойността на първото вписване на Документа е валиден от.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'DateValidFrom'         , N'Документа е валиден от.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'DateValidTo'           , N'Документа е валиден до.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'LicenceActionId'       , N'Основание.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'Notes'                 , N'Бележки.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'Inspector'             , N'Инспектор.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'Limitations'           , N'Ограничения.'
exec spDescColumn N'GvaViewPersonLicenceEditions', N'PrintedFileId'         , N'Идентификатор на файла на принтиран лиценз.'
GO
