PRINT 'GvaViewPersonRatings'
GO 

CREATE TABLE [dbo].[GvaViewPersonRatings] (
    [LotId]                    INT               NOT NULL,
    [LotPartId]                INT               NOT NULL,
    [PartIndex]                INT               NOT NULL,
    [RatingTypes]              NVARCHAR(MAX)     NULL,
    [RatingLevelId]            INT               NULL,
    [RatingClassId]            INT               NULL,
    [AircraftTypeGroupId]      INT               NULL,
    [AuthorizationId]          INT               NULL,
    [LocationIndicatorId]      INT               NULL,
    [CaaId]                    INT               NULL,
    [Sector]                   NVARCHAR(100)     NULL,
    [AircraftTypeCategoryId]   INT               NULL,
    CONSTRAINT [PK_GvaViewPersonRatings]                        PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonRatings_GvaViewPersons]         FOREIGN KEY ([LotId])                 REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonRatings_LotParts]               FOREIGN KEY ([LotPartId])             REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonRatings_RatingLevelId]          FOREIGN KEY ([RatingLevelId])         REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_RatingClassId]          FOREIGN KEY ([RatingClassId])         REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_AircraftTypeGroupId]    FOREIGN KEY ([AircraftTypeGroupId])   REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_AuthorizationId]        FOREIGN KEY ([AuthorizationId])       REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_LocationIndicatorId]    FOREIGN KEY ([LocationIndicatorId])   REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_CaaId]                  FOREIGN KEY ([CaaId])   REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonRatings_AircraftTypeCategoryId]  FOREIGN KEY ([AircraftTypeCategoryId])   REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewPersonRatings', N'Данни за квалификационен клас.'
exec spDescColumn N'GvaViewPersonRatings', N'LotId'                    , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewPersonRatings', N'LotPartId'                , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingTypes'              , N'Типове ВС на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingLevelId'            , N'Степен на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingClassId'            , N'Клас ВС на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'AircraftTypeGroupId'      , N'Група на ВС на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'AircraftTypeCategoryId'   , N'Категория на ВС на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'AuthorizationId'          , N'Разрешение на квалификационния клас.'
exec spDescColumn N'GvaViewPersonRatings', N'LocationIndicatorId'      , N'Индикатор на местоположение.'
exec spDescColumn N'GvaViewPersonRatings', N'CaaId'                    , N'Администрация.'
exec spDescColumn N'GvaViewPersonRatings', N'Sector'                   , N'Сектор/работно място.'


GO
