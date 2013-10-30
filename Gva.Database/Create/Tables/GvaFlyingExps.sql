PRINT 'GvaFlyingExps'
GO 

CREATE TABLE [dbo].[GvaFlyingExps] (
    [GvaFlyingExpId]     INT   NOT NULL IDENTITY,
    [GvaApplicationId]   INT   NULL,
    [LotPartId]          INT   NOT NULL,
    CONSTRAINT [PK_GvaFlyingExps]                  PRIMARY KEY ([GvaFlyingExpId]),
    CONSTRAINT [FK_GvaFlyingExps_GvaApplications]  FOREIGN KEY ([GvaApplicationId]) REFERENCES [dbo].[GvaApplications] ([GvaApplicationId]),
    CONSTRAINT [FK_GvaFlyingExps_LotParts]         FOREIGN KEY ([LotPartId])        REFERENCES [dbo].[LotParts]        ([LotPartId])
)
GO

exec spDescTable  N'GvaFlyingExps', N'Летателен/практически опит.'
exec spDescColumn N'GvaFlyingExps', N'GvaFlyingExpId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaFlyingExps', N'GvaApplicationId'   , N'Заявление.'
exec spDescColumn N'GvaFlyingExps', N'LotPartId'          , N'Част от партидата.'
GO
