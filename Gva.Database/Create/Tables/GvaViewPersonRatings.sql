PRINT 'GvaViewPersonRatings'
GO 

CREATE TABLE [dbo].[GvaViewPersonRatings] (
    [LotId]          INT           NOT NULL,
    [PartIndex]      INT           NOT NULL,
    [RatingTypeId]   INT           NOT NULL,
    CONSTRAINT [PK_GvaViewPersonRatings]                 PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonRatings_GvaViewPersons]  FOREIGN KEY ([LotId])            REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonRatings_NomValues]       FOREIGN KEY ([RatingTypeId])     REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewPersonRatings', N'Класове на физически лица.'
exec spDescColumn N'GvaViewPersonRatings', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonRatings', N'PartIndex'                 , N'Индекс на част от партида.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingTypeId'              , N'Тип клас.'
GO