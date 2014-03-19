PRINT 'GvaApplicationSearches'
GO 

CREATE TABLE [dbo].[GvaApplicationSearches] (
    [LotPartId]    INT  NOT NULL,
    [RequestDate] DATETIME NULL,
    [DocumentNumber] NVARCHAR(20) NULL,
    [ApplicationTypeName] NVARCHAR(500) NULL,
    [StatusName] NVARCHAR(50) NULL

    CONSTRAINT [PK_GvaApplicationSearches]           PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_GvaApplicationSearches_LotParts]      FOREIGN KEY ([LotPartId]) REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO

