PRINT 'GvaRatingDates'
GO 

CREATE TABLE [dbo].[GvaRatingDates] (
    [GvaRatingDateId]   INT   NOT NULL IDENTITY,
    [GvaRatingId]       INT   NOT NULL,
    [LotPartId]         INT   NOT NULL,
    [IsActive]          INT   NOT NULL,
    CONSTRAINT [PK_GvaRatingDates]            PRIMARY KEY ([GvaRatingDateId]),
    CONSTRAINT [FK_GvaRatingDates_GvaRatings] FOREIGN KEY ([GvaRatingId])   REFERENCES [dbo].[GvaRatings] ([GvaRatingId]),
    CONSTRAINT [FK_GvaRatingDates_LotParts]   FOREIGN KEY ([LotPartId])     REFERENCES [dbo].[LotParts]   ([LotPartId])
)
GO

exec spDescTable  N'GvaRatingDates', N'Вписвания по квалификация.'
exec spDescColumn N'GvaRatingDates', N'GvaRatingDateId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaRatingDates', N'GvaRatingId'          , N'Квалификация.'
exec spDescColumn N'GvaRatingDates', N'LotPartId'            , N'Част от партидата.'
exec spDescColumn N'GvaRatingDates', N'IsActive'             , N'Маркер за валидност.'
GO
