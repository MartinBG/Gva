PRINT 'GvaViewOrganizationInspectionsRecommendations'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationInspectionsRecommendations] (
    [LotId]                     INT NOT NULL,
    [InspectionPartIndex]       INT NOT NULL,
    [RecommendationPartIndex]   INT NOT NULL,
    CONSTRAINT [PK_GvaViewOrganizationInspectionsRecommendations]                                   PRIMARY KEY ([LotId], [InspectionPartIndex], [RecommendationPartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationInspectionsRecommendations_GvaViewOrganizationRecommendation] FOREIGN KEY ([LotId], [RecommendationPartIndex]) REFERENCES [dbo].[GvaViewOrganizationRecommendations] ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationInspectionsRecommendations_GvaViewOrganizationInspection]     FOREIGN KEY ([LotId], [InspectionPartIndex])     REFERENCES [dbo].[GvaViewOrganizationInspections] ([LotId], [PartIndex])
);
GO
