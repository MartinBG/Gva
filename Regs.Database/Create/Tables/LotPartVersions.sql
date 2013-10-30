PRINT 'LotPartVersions'
GO 

CREATE TABLE [dbo].[LotPartVersions] (
    [LotPartVersionId]         INT            NOT NULL IDENTITY,
    [LotPartId]                INT            NOT NULL,
    [TextBlobId]               INT            NOT NULL,
    [LotPartVersionCommitId]   INT            NOT NULL,
    [CreatorId]                INT            NOT NULL,
    [CreateDate]               DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_LotPartVersions]           PRIMARY KEY ([LotPartVersionId]),
    CONSTRAINT [FK_LotPartVersions_Commits]   FOREIGN KEY ([LotPartVersionCommitId]) REFERENCES [dbo].[LotCommits] ([LotCommitId]),
    CONSTRAINT [FK_LotPartVersions_LotParts]  FOREIGN KEY ([LotPartId])              REFERENCES [dbo].[LotParts]   ([LotPartId]),
    CONSTRAINT [FK_LotPartVersions_TextBlobs] FOREIGN KEY ([TextBlobId])             REFERENCES [dbo].[TextBlobs]  ([TextBlobId]),
)
GO

exec spDescTable  N'LotPartVersions', N'Версии на част на партида.'
exec spDescColumn N'LotPartVersions', N'LotPartVersionId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotPartVersions', N'LotPartId'                , N'Част на партида.'
exec spDescColumn N'LotPartVersions', N'TextBlobId'               , N'Съдържание.'
exec spDescColumn N'LotPartVersions', N'LotPartVersionCommitId'   , N'Първо вписване с което е направена версията.'
exec spDescColumn N'LotPartVersions', N'CreatorId'                , N'Създател.'
exec spDescColumn N'LotPartVersions', N'CreateDate'               , N'Дата на създаване.'
GO
