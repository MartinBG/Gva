PRINT 'LotPartVersions'
GO 

CREATE TABLE [dbo].[LotPartVersions] (
    [LotPartVersionId]         INT            NOT NULL,
    [LotPartId]                INT            NOT NULL,
    [TextContent]              NVARCHAR(MAX)  NULL,
    [OriginalCommitId]         INT            NOT NULL,
    [LotPartOperationId]       INT            NOT NULL,
    [CreatorId]                INT            NOT NULL,
    [CreateDate]               DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_LotPartVersions]                   PRIMARY KEY ([LotPartVersionId]),
    CONSTRAINT [FK_LotPartVersions_LotCommits]        FOREIGN KEY ([OriginalCommitId])    REFERENCES [dbo].[LotCommits]         ([LotCommitId]),
    CONSTRAINT [FK_LotPartVersions_LotParts]          FOREIGN KEY ([LotPartId])           REFERENCES [dbo].[LotParts]           ([LotPartId]),
    CONSTRAINT [FK_LotPartVersions_LotPartOperations] FOREIGN KEY ([LotPartOperationId])  REFERENCES [dbo].[LotPartOperations]  ([LotPartOperationId]),
    CONSTRAINT [FK_LotPartVersions_Users]             FOREIGN KEY ([CreatorId])           REFERENCES [dbo].[Users]              ([UserId])
)
GO

exec spDescTable  N'LotPartVersions', N'Версии на част на партида.'
exec spDescColumn N'LotPartVersions', N'LotPartVersionId'         , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotPartVersions', N'LotPartId'                , N'Част на партида.'
exec spDescColumn N'LotPartVersions', N'TextContent'              , N'Съдържание.'
exec spDescColumn N'LotPartVersions', N'OriginalCommitId'         , N'Първо вписване с което е направена версията.'
exec spDescColumn N'LotPartVersions', N'LotPartOperationId'       , N'Вид на операцията.'
exec spDescColumn N'LotPartVersions', N'CreatorId'                , N'Създател.'
exec spDescColumn N'LotPartVersions', N'CreateDate'               , N'Дата на създаване.'
GO
