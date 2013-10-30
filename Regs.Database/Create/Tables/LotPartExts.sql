PRINT 'LotPartExts'
GO 

CREATE TABLE [dbo].[LotPartExts] (
    [LotPartId]                 INT   NOT NULL IDENTITY,
    [IndexLotPartVersionId]     INT   NULL,
    [CommitedLotPartVersionId]  INT   NOT NULL,
    [FirstLotPartVersionId]     INT   NOT NULL,
    CONSTRAINT [PK_LotPartExts]                  PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_LotPartExts_LotParts]         FOREIGN KEY ([LotPartId])                REFERENCES [dbo].[LotParts]        ([LotPartId]),
    CONSTRAINT [FK_LotPartExts_LotPartVersions]  FOREIGN KEY ([CommitedLotPartVersionId]) REFERENCES [dbo].[LotPartVersions] ([LotPartVersionId]),
    CONSTRAINT [FK_LotPartExts_LotPartVersions1] FOREIGN KEY ([FirstLotPartVersionId])    REFERENCES [dbo].[LotPartVersions] ([LotPartVersionId]),
    CONSTRAINT [FK_LotPartExts_LotPartVersions2] FOREIGN KEY ([IndexLotPartVersionId])    REFERENCES [dbo].[LotPartVersions] ([LotPartVersionId])
)
GO

exec spDescTable  N'LotPartExts', N'Допълнения към част на партида.'
exec spDescColumn N'LotPartExts', N'LotPartId'                 , N'Част на партида.'
exec spDescColumn N'LotPartExts', N'IndexLotPartVersionId'     , N'Версия в чернова на частта.'
exec spDescColumn N'LotPartExts', N'CommitedLotPartVersionId'  , N'Последно вписана версия на частта.'
exec spDescColumn N'LotPartExts', N'FirstLotPartVersionId'     , N'Първа версия на частта.'
GO
