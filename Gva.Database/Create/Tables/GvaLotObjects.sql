PRINT 'GvaLotObjects'
GO 

CREATE TABLE [dbo].[GvaLotObjects] (
    [GvaLotObjectId]     INT   NOT NULL IDENTITY,
    [GvaApplicationId]   INT   NULL,
    [LotPartId]          INT   NOT NULL,
    [IsActive]           BIT   NOT NULL,
    CONSTRAINT [PK_GvaLotObjects]                  PRIMARY KEY ([GvaLotObjectId]),
    CONSTRAINT [FK_GvaLotObjects_GvaApplications]  FOREIGN KEY ([GvaApplicationId]) REFERENCES [dbo].[GvaApplications] ([GvaApplicationId]),
    CONSTRAINT [FK_GvaLotObjects_LotParts]         FOREIGN KEY ([LotPartId])        REFERENCES [dbo].[LotParts]        ([LotPartId])
)
GO

exec spDescTable  N'GvaLotObjects', N'Части към заявление.'
exec spDescColumn N'GvaLotObjects', N'GvaLotObjectId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaLotObjects', N'GvaApplicationId'   , N'Заявление.'
exec spDescColumn N'GvaLotObjects', N'LotPartId'          , N'Част от партидата.'
exec spDescColumn N'GvaLotObjects', N'IsActive'           , N'Маркер за валидност.'
GO
