PRINT 'GvaViewOrganizationInspections'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationInspections] (
    [LotId]                  INT           NOT NULL,
    [InspectionPartIndex]    INT           NOT NULL,
    CONSTRAINT [PK_GvaViewOrganizationInspections]                       PRIMARY KEY ([InspectionPartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationInspections_GvaViewOrganizations]  FOREIGN KEY ([LotId])    REFERENCES [dbo].[GvaViewOrganizations] ([LotId])
)
GO