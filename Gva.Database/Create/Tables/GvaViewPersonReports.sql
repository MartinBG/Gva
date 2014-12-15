PRINT 'GvaViewPersonReports'
GO 

CREATE TABLE [dbo].[GvaViewPersonReports] (
    [LotId]                    INT           NOT NULL,
    [PartIndex]                INT           NOT NULL,
    [Publisher]                NVARCHAR(100) NULL,
    [DocumentNumber]           NVARCHAR(50)  NULL,
    [Date]                     DATETIME2     NULL,
    CONSTRAINT [PK_GvaViewPersonReports]                PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonReports_GvaViewPersons] FOREIGN KEY ([LotId])  REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO
