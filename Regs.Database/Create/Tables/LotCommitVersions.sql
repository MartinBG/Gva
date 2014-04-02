PRINT 'LotCommitVersions'
GO 

CREATE TABLE [dbo].[LotCommitVersions] (
    [LotCommitId]         INT   NOT NULL,
    [LotPartVersionId]    INT   NOT NULL,
    [OldLotPartVersionId] INT   NULL,
    CONSTRAINT [PK_LotCommitVersions]                     PRIMARY KEY ([LotCommitId], [LotPartVersionId]),
    CONSTRAINT [FK_LotCommitVersions_LotCommits]          FOREIGN KEY ([LotCommitId])         REFERENCES [dbo].[LotCommits]      ([LotCommitId]),
    CONSTRAINT [FK_LotCommitVersions_LotPartVersions]     FOREIGN KEY ([LotPartVersionId])    REFERENCES [dbo].[LotPartVersions] ([LotPartVersionId]),
    CONSTRAINT [FK_LotCommitVersions_OldLotPartVersions]  FOREIGN KEY ([OldLotPartVersionId]) REFERENCES [dbo].[LotPartVersions] ([LotPartVersionId])
)
GO

exec spDescTable  N'LotCommitVersions', N'Версии на части принадлежащи към вписване.'
exec spDescColumn N'LotCommitVersions', N'LotCommitId'        , N'Вписване.'
exec spDescColumn N'LotCommitVersions', N'LotPartVersionId'   , N'Версия на част на партида.'
exec spDescColumn N'LotCommitVersions', N'OldLotPartVersionId', N'Предходна версия на част на партида.'
GO
