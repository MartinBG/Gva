PRINT 'GvaViewPersonQualifications'
GO 

CREATE TABLE [dbo].[GvaViewPersonQualifications] (
    [LotId]                INT           NOT NULL,
    [ApplicationPartIndex] INT           NOT NULL,
    [QualificationCodes]   NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_GvaViewPersonQualifications]                PRIMARY KEY ([LotId], [ApplicationPartIndex]),
    CONSTRAINT [FK_GvaViewPersonQualifications_GvaViewPersons] FOREIGN KEY ([LotId])  REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO

exec spDescTable  N'GvaViewPersonQualifications', N'Данни за инспектор.'
exec spDescColumn N'GvaViewPersonQualifications', N'LotId'                     , N'Идентификатор на партида.'
exec spDescColumn N'GvaViewPersonQualifications', N'ApplicationPartIndex'      , N'Идентификатор на  заявление.'
exec spDescColumn N'GvaViewPersonQualifications', N'QualificationCodes'        , N'Кодове на квалификации.'
GO
