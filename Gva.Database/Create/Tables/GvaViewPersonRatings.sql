PRINT 'GvaViewPersonRatings'
GO 

CREATE TABLE [dbo].[GvaViewPersonRatings] (
    [LotId]          INT           NOT NULL,
    [LotPartId]      INT           NOT NULL,
    [RatingType]     NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_GvaViewPersonRatings]          PRIMARY KEY ([LotId], [LotPartId]),
    CONSTRAINT [FK_GvaViewPersonRatings_Lots]     FOREIGN KEY ([LotId])              REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonRatings_LotParts] FOREIGN KEY ([LotPartId])      REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO

exec spDescTable  N'GvaViewPersonRatings', N'Класове на физически лица.'
exec spDescColumn N'GvaViewPersonRatings', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonRatings', N'LotPartId'                 , N'Идентификатор на част от партида.'
exec spDescColumn N'GvaViewPersonRatings', N'RatingType'                , N'Тип клас.'
GO