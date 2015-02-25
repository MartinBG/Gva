PRINT 'GvaViewApplications'
GO 

CREATE TABLE [dbo].[GvaViewApplications] (
    [LotId]               INT            NOT NULL,
    [LotPartId]           INT            NOT NULL,
    [DocumentDate]        DATETIME2      NULL,
    [DocumentNumber]      NVARCHAR(100)  NULL,
    [OldDocumentNumber]   NVARCHAR(100)  NULL,
    [ApplicationTypeId]   INT            NOT NULL,
    CONSTRAINT [PK_GvaViewApplications]             PRIMARY KEY ([LotId], [LotPartId]),
    CONSTRAINT [FK_GvaViewApplications_Lots]        FOREIGN KEY ([LotId])                REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewApplications_LotParts]    FOREIGN KEY ([LotPartId])            REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewApplications_NomValues]   FOREIGN KEY ([ApplicationTypeId])    REFERENCES [dbo].[NomValues] ([NomValueId]),
)
GO

