PRINT 'AopPortalDocRelations'
GO 

CREATE TABLE [dbo].[AopPortalDocRelations] (
    [AopPortalDocRelationId]    INT  NOT NULL IDENTITY,
    [PortalDocId] UNIQUEIDENTIFIER NOT NULL,
    [DocId] int NOT NULL,
	[Version] ROWVERSION NOT NULL,
    CONSTRAINT [PK_AopPortalDocRelations]           PRIMARY KEY ([AopPortalDocRelationId])
)
GO
