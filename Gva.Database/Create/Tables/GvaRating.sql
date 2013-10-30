PRINT 'GvaRatings'
GO 

CREATE TABLE [dbo].[GvaRatings] (
    [GvaRatingId]        INT   NOT NULL IDENTITY,
    [GvaApplicationId]   INT   NULL,
    CONSTRAINT [PK_GvaRatings]                 PRIMARY KEY ([GvaRatingId]),
    CONSTRAINT [FK_GvaRatings_GvaApplications] FOREIGN KEY ([GvaApplicationId]) REFERENCES [dbo].[GvaApplications] ([GvaApplicationId])
)
GO

exec spDescTable  N'GvaRatings', N'Квалификации.'
exec spDescColumn N'GvaRatings', N'GvaRatingId'       , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaRatings', N'GvaApplicationId'  , N'Заявление.'
GO
