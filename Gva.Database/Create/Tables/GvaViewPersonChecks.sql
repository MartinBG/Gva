PRINT 'GvaViewPersonChecks'
GO 

CREATE TABLE [dbo].[GvaViewPersonChecks] (
    [LotId]                    INT           NOT NULL,
    [PartId]                   INT           NOT NULL,
    [PartIndex]                INT           NOT NULL,
    [Publisher]                NVARCHAR(100) NULL,
    [DocumentNumber]           NVARCHAR(50)  NULL,
    [DocumentTypeId]           INT           NULL,
    [DocumentRoleId]           INT           NULL,
    [RatingTypes]              NVARCHAR(MAX) NULL,
    [RatingClassId]            INT           NULL,
    [AuthorizationId]          INT           NULL,
    [Sector]                   NVARCHAR(50)  NULL,
    [LicenceTypeId]            INT           NULL,
    [ValidId]                  INT           NULL,
    [PersonCheckRatingValueId] INT           NULL,
    [DocumentDateValidFrom]    DATETIME2     NULL,
    [DocumentDateValidTo]      DATETIME2     NULL,
    CONSTRAINT [PK_GvaViewPersonChecks]                PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonChecks_GvaViewPersons] FOREIGN KEY ([LotId])                 REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonChecks_NomValues]   FOREIGN KEY ([DocumentTypeId])           REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonChecks_NomValues2]  FOREIGN KEY ([DocumentRoleId])           REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonChecks_NomValues3]  FOREIGN KEY ([RatingClassId])            REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonChecks_NomValues4]  FOREIGN KEY ([AuthorizationId])          REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonChecks_NomValues5]  FOREIGN KEY ([LicenceTypeId])            REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonChecks_NomValues6]  FOREIGN KEY ([ValidId])                  REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonChecks_NomValues7]  FOREIGN KEY ([PersonCheckRatingValueId]) REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO
