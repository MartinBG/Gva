PRINT 'GvaViewPrintedRatingEditions'
GO 

CREATE TABLE [dbo].[GvaViewPrintedRatingEditions] (
    [LotId]                   INT              NOT NULL,
    [RatingPartIndex]         INT              NOT NULL,
    [RatingEditionPartIndex]  INT              NOT NULL,
    [LicencePartIndex]        INT              NOT NULL,
    [LicenceEditionPartIndex] INT              NOT NULL,
    [PrintedFileId]           INT              NOT NULL,
    [LicenceStatusId]         INT              NULL,
    [NoNumber]                BIT              NULL,
    CONSTRAINT [PK_GvaViewPrintedRatingEditions]                              PRIMARY KEY ([LotId], [RatingPartIndex], [RatingEditionPartIndex], [LicencePartIndex], [LicenceEditionPartIndex]),
    CONSTRAINT [FK_GvaViewPrintedRatingEditions_GvaViewPersonLicenceEditions] FOREIGN KEY ([LotId], [LicencePartIndex], [LicenceEditionPartIndex]) REFERENCES [dbo].[GvaViewPersonLicenceEditions] ([LotId], [LicencePartIndex], [PartIndex]),
    CONSTRAINT [FK_GvaViewPrintedRatingEditions_GvaFiles]                     FOREIGN KEY ([PrintedFileId])                                        REFERENCES [dbo].[GvaFiles] ([GvaFileId])
)
GO

exec spDescTable  N'GvaViewPrintedRatingEditions', N'Данни за принтирани квалификационни класове към вписвания на лиценз.'
exec spDescColumn N'GvaViewPrintedRatingEditions', N'LotId'                   , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewPrintedRatingEditions', N'RatingPartIndex'         , N'Идентификатор на квалификационен клас.'
exec spDescColumn N'GvaViewPrintedRatingEditions', N'RatingEditionPartIndex'  , N'Идентификатор на вписване на квалификационен клас.'
exec spDescColumn N'GvaViewPrintedRatingEditions', N'LicencePartIndex'        , N'Идентификатор на индекс на лиценз.'
exec spDescColumn N'GvaViewPrintedRatingEditions', N'LicenceEditionPartIndex' , N'Идентификатор на вписване на лиценз.'
exec spDescColumn N'GvaViewPrintedRatingEditions', N'PrintedFileId'           , N'Идентификатор на файла на принтиран лиценз.'
exec spDescColumn N'GvaViewPrintedRatingEditions', N'LicenceStatusId'         , N'Статус за готов/получен лиценз.'
exec spDescColumn N'GvaViewPrintedRatingEditions', N'NoNumber'                , N'Флаг за лиценз номер.'

GO
