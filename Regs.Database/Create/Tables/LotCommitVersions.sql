PRINT 'LotCommitVersions'
GO 

CREATE TABLE [dbo].[LotCommitVersions] (
    [LotPartVersionId]   INT   NOT NULL IDENTITY,
    [LotCommitId]        INT   NOT NULL,
    CONSTRAINT [PK_LotCommitVersions]                  PRIMARY KEY ([LotPartVersionId], [LotCommitId]),
    CONSTRAINT [FK_LotCommitVersions_LotCommits]       FOREIGN KEY ([LotCommitId])      REFERENCES [dbo].[LotCommits]      ([LotCommitId]),
    CONSTRAINT [FK_LotCommitVersions_LotPartVersions]  FOREIGN KEY ([LotPartVersionId]) REFERENCES [dbo].[LotPartVersions] ([LotPartVersionId])
)
GO

exec spDescTable  N'LotCommitVersions', N'Версии на части принадлежащи към вписване.'
exec spDescColumn N'LotCommitVersions', N'LotPartVersionId'   , N'Версия на част на партида.'
exec spDescColumn N'LotCommitVersions', N'LotCommitId'        , N'Вписване.'
GO
