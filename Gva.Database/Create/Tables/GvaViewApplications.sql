PRINT 'GvaViewApplications'
GO 

CREATE TABLE [dbo].[GvaViewApplications] (
    [LotPartId]           INT           NOT NULL,
    [RequestDate]         DATETIME      NULL,
    [DocumentNumber]      NVARCHAR(20)  NULL,
    [ApplicationTypeName] NVARCHAR(500) NULL,
    [StatusName]          NVARCHAR(50)  NULL,
    CONSTRAINT [PK_GvaViewApplications]           PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_GvaViewApplications_LotParts]  FOREIGN KEY ([LotPartId]) REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO

