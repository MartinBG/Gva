PRINT 'GvaViewOrganizationRecommendations'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationRecommendations] (
    [LotId]                  INT           NOT NULL,
    [RecommendationPartIndex]    INT           NOT NULL,
    [RecommendationPartName] NVARCHAR(150) NULL,
    [FormText]               NVARCHAR(150) NULL,
    [FormDate]               DATETIME2     NULL,
    CONSTRAINT [PK_GvaViewOrganizationRecommendations]                       PRIMARY KEY ([RecommendationPartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationRecommendations_GvaViewOrganizations]  FOREIGN KEY ([LotId])    REFERENCES [dbo].[GvaViewOrganizations] ([LotId])
)
GO