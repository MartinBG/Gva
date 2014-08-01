PRINT 'GvaViewOrganizationInspectionsRecommendations'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationInspectionsRecommendations] (
    [LotId]                       INT     NOT NULL,
    [InspectionPartIndex]         INT     NOT NULL,
    [RecommendationPartIndex]     INT     NOT NULL,
    CONSTRAINT [PK_GvaViewOrganizationInspectionsRecommendations]                                   PRIMARY KEY ([InspectionPartIndex], [RecommendationPartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationInspectionsRecommendations_GvaViewOrganizationRecommendation] FOREIGN KEY ([RecommendationPartIndex]) REFERENCES [dbo].[GvaViewOrganizationRecommendations] ([RecommendationPartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationInspectionsRecommendations_GvaViewOrganizationInspection]     FOREIGN KEY ([InspectionPartIndex]) REFERENCES [dbo].[GvaViewOrganizationInspections] ([InspectionPartIndex])
);
GO

