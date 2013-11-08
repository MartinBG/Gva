PRINT 'LotCommits'
GO 

CREATE TABLE [dbo].[LotCommits] (
    [LotCommitId]         INT           NOT NULL IDENTITY,
    [LotId]               INT           NOT NULL,
    [ParentLotCommitId]   INT           NULL,
    [CommiterId]          INT           NOT NULL,
    [CommitDate]          DATETIME2 (7) NOT NULL,
    [IsIndex]             BIT           NOT NULL,
    CONSTRAINT [PK_Commits]               PRIMARY KEY ([LotCommitId]),
    CONSTRAINT [FK_LotCommits_LotCommits] FOREIGN KEY ([ParentLotCommitId]) REFERENCES [dbo].[LotCommits] ([LotCommitId]),
    CONSTRAINT [FK_LotCommits_Lots]       FOREIGN KEY ([LotId])             REFERENCES [dbo].[Lots]       ([LotId]),
    CONSTRAINT [FK_LotCommits_Users]      FOREIGN KEY ([CommiterId])        REFERENCES [dbo].[Users]      ([UserId])
)
GO

exec spDescTable  N'LotCommits', N'Вписвание по партида.'
exec spDescColumn N'LotCommits', N'LotCommitId'        , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotCommits', N'LotId'              , N'Партида.'
exec spDescColumn N'LotCommits', N'ParentLotCommitId'  , N'Предходно вписване.'
exec spDescColumn N'LotCommits', N'CommiterId'         , N'Потребител направил вписването.'
exec spDescColumn N'LotCommits', N'CommitDate'         , N'Дата на вписването.'
exec spDescColumn N'LotCommits', N'IsIndex'            , N'Маркер за вписване в чернова.'
GO
