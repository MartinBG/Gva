PRINT 'GvaViewOrganizationInspections'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationInspections] (
    [LotId]     INT           NOT NULL,
    [PartIndex] INT           NOT NULL,
    CONSTRAINT [PK_GvaViewOrganizationInspections]                       PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationInspections_GvaViewOrganizations]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewOrganizations] ([LotId])
)
GO
