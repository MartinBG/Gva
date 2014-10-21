PRINT 'GvaViewPersonRatingEditions'
GO 

CREATE TABLE [dbo].[GvaViewPersonRatingEditions] (
    [LotId]                    INT               NOT NULL,
    [LotPartId]                INT               NOT NULL,
    [PartIndex]                INT               NOT NULL,
    [Index]                    INT               NOT NULL,
    [RatingPartIndex]          INT               NOT NULL,
    [RatingSubClasses]         NVARCHAR(MAX)     NULL,
    [Limitations]              NVARCHAR(MAX)     NULL,
    [DocDateValidFrom]         DATETIME2         NOT NULL,
    [DocDateValidTo]           DATETIME2         NULL,
    [Notes]                    NVARCHAR(MAX)     NULL,
    [NotesAlt]                 NVARCHAR(MAX)     NULL,
    CONSTRAINT [PK_GvaViewPersonRatingEditions]                        PRIMARY KEY ([LotId], [RatingPartIndex], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonRatingEditions_LotParts]               FOREIGN KEY ([LotPartId])                REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonRatingEditions_GvaViewPersonRatings]   FOREIGN KEY ([LotId], [RatingPartIndex]) REFERENCES [dbo].[GvaViewPersonRatings] ([LotId], [PartIndex])
)
GO

exec spDescTable  N'GvaViewPersonRatingEditions', N'Данни за вписване към квалификацията.'
exec spDescColumn N'GvaViewPersonRatingEditions', N'LotId'                , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewPersonRatingEditions', N'RatingPartIndex'      , N'Идентификатор на квалификацията към която е свързано това вписване'
exec spDescColumn N'GvaViewPersonRatingEditions', N'RatingSubClasses'     , N'Подкласове на класа.'
exec spDescColumn N'GvaViewPersonRatingEditions', N'Limitations'          , N'Ограничения към класа.'
exec spDescColumn N'GvaViewPersonRatingEditions', N'DocDateValidFrom'     , N'Дата на вписване.'
exec spDescColumn N'GvaViewPersonRatingEditions', N'DocDateValidTo'       , N'Дата на валидност.'
exec spDescColumn N'GvaViewPersonRatingEditions', N'Notes'                , N'Бележки към последното вписване.'
exec spDescColumn N'GvaViewPersonRatingEditions', N'NotesAlt'             , N'Бележки лат. към последното вписване.'
GO
