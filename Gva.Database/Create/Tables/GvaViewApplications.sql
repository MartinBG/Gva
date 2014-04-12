PRINT 'GvaViewApplications'
GO 

CREATE TABLE [dbo].[GvaViewApplications] (
    [LotPartId]           INT           NOT NULL,
    [LotId]               INT           NOT NULL,
    [RequestDate]         DATETIME      NULL,
    [DocumentNumber]      NVARCHAR(100)  NULL,
    [ApplicationTypeName] NVARCHAR(500) NULL,
    [StatusName]          NVARCHAR(50)  NULL,
    CONSTRAINT [PK_GvaViewApplications]           PRIMARY KEY ([LotId], [LotPartId]),
    CONSTRAINT [FK_GvaViewApplications_Lots]      FOREIGN KEY ([LotId])          REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewApplications_LotParts]  FOREIGN KEY ([LotPartId])      REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO

