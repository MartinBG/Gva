PRINT 'GvaViewOrganizationRecommendations'
GO 

CREATE TABLE [dbo].[GvaViewOrganizationRecommendations] (
    [LotId]                  INT           NOT NULL,
    [PartIndex]              INT           NOT NULL,
    [RecommendationPartName] NVARCHAR(150) NULL,
    [FormText]               NVARCHAR(150) NULL,
    [FormDate]               DATETIME2     NULL,
    CONSTRAINT [PK_GvaViewOrganizationRecommendations]                       PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewOrganizationRecommendations_GvaViewOrganizations]  FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewOrganizations] ([LotId])
)
GO
