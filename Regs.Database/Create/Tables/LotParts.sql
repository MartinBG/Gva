PRINT 'LotParts'
GO 

CREATE TABLE [dbo].[LotParts] (
    [LotPartId]     INT            NOT NULL,
    [LotSetPartId]  INT            NOT NULL,
    [LotId]         INT            NOT NULL,
    [Path]          NVARCHAR (100) NOT NULL,
    [Index]         INT            NOT NULL,
    [CreatorId]     INT            NOT NULL,
    [CreateDate]    DATETIME2      NOT NULL,
    CONSTRAINT [PK_LotParts]             PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_LotParts_LotSetParts] FOREIGN KEY ([LotSetPartId]) REFERENCES [dbo].[LotSetParts] ([LotSetPartId]),
    CONSTRAINT [FK_LotParts_Lots]        FOREIGN KEY ([LotId])        REFERENCES [dbo].[Lots]        ([LotId]),
    CONSTRAINT [FK_LotParts_Users]       FOREIGN KEY ([CreatorId])    REFERENCES [dbo].[Users]       ([UserId])
)
GO

exec spDescTable  N'LotParts', N'Части на партиди.'
exec spDescColumn N'LotParts', N'LotPartId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotParts', N'LotSetPartId'   , N'Част на тип партида.'
exec spDescColumn N'LotParts', N'LotId'          , N'Партида.'
exec spDescColumn N'LotParts', N'Path'           , N'Път.'
exec spDescColumn N'LotParts', N'Index'          , N'Индекс.'
exec spDescColumn N'LotParts', N'CreatorId'      , N'Създател.'
exec spDescColumn N'LotParts', N'CreateDate'     , N'Дата на създаване.'
GO
