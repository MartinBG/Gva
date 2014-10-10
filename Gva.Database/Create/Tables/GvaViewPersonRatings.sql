PRINT 'GvaViewPersonRatings'
GO 

CREATE TABLE [dbo].[GvaViewPersonRatings] (
    [LotId]                    INT               NOT NULL,
    [LotPartId]                INT               NOT NULL,
    [EditionIndex]             INT               NOT NULL,
    [RatingPartIndex]          INT               NOT NULL,
    [EditionPartIndex]         INT               NOT NULL,
    [RatingTypeId]             INT               NULL,
    [RatingLevelId]            INT               NULL,
    [RatingClassId]            INT               NULL,
    [AircraftTypeGroupId]      INT               NULL,
    [AuthorizationId]          INT               NULL,
    [RatingSubClasses]         NVARCHAR(MAX)     NULL,
    [Limitations]              NVARCHAR(MAX)     NULL,
    [LastDocDateValidFrom]     DATETIME2         NOT NULL,
    [LastDocDateValidTo]       DATETIME2         NULL,
    [FirstDocDateValidFrom]    DATETIME2         NOT NULL,
    [Notes]                    NVARCHAR(MAX)     NULL,
    [NotesAlt]                 NVARCHAR(MAX)     NULL,
    [GvaCaseTypeId]            INT               NULL,
    CONSTRAINT [PK_GvaViewPersonRatings]                        PRIMARY KEY ([LotId], [LotPartId]),
    CONSTRAINT [FK_GvaViewPersonRatings_GvaViewPersons]         FOREIGN KEY ([LotId])                                  REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonRatings_LotParts]               FOREIGN KEY ([LotPartId])                              REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonRatings_RatingTypeId]           FOREIGN KEY ([RatingTypeId])                           REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_RatingLevelId]          FOREIGN KEY ([RatingLevelId])                          REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_RatingClassId]          FOREIGN KEY ([RatingClassId])                          REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_AircraftTypeGroupId]    FOREIGN KEY ([AircraftTypeGroupId])                    REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_AuthorizationId]        FOREIGN KEY ([AuthorizationId])                        REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewPersonRatings', N'Данни за квалификационен клас.'
exec spDescColumn N'GvaViewPersonRatings', N'LotId'                    , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewPersonRatings', N'LotPartId'                , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewPersonRatings', N'EditionIndex'             , N'Индекс на вписването.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingTypeId'             , N'Тип ВС на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingLevelId'            , N'Степен на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingClassId'            , N'Клас ВС на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'AircraftTypeGroupId'      , N'Група на ВС на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'AuthorizationId'          , N'Разрешение на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingSubClasses'         , N'Подкласове на класа.'
exec spDescColumn N'GvaViewPersonRatings', N'Limitations'              , N'Ограничения към класа.'
exec spDescColumn N'GvaViewPersonRatings', N'LastDocDateValidFrom'     , N'Дата на вписване на последното вписване.'
exec spDescColumn N'GvaViewPersonRatings', N'LastDocDateValidTo'       , N'Дата на валидност на последното вписване.'
exec spDescColumn N'GvaViewPersonRatings', N'FirstDocDateValidFrom'    , N'Дата на вписване на първото вписване.'
exec spDescColumn N'GvaViewPersonRatings', N'Notes'                    , N'Бележки към последното вписване.'
exec spDescColumn N'GvaViewPersonRatings', N'NotesAlt'                 , N'Бележки лат. към последното вписване.'
exec spDescColumn N'GvaViewPersonRatings', N'GvaCaseTypeId'            , N'Идентификатор на типа дело на квалификация.'
GO
