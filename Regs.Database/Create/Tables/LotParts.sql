PRINT 'LotParts'
GO 

CREATE TABLE [dbo].[LotParts] (
    [LotPartId]     INT            NOT NULL IDENTITY,
    [LotId]         INT            NOT NULL,
    [Path]          NVARCHAR (50)  NOT NULL,
    [TextContent]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_LotParts]      PRIMARY KEY ([LotPartId]),
    CONSTRAINT [FK_LotParts_Lots] FOREIGN KEY ([LotId]) REFERENCES [dbo].[Lots] ([LotId])
)
GO

exec spDescTable  N'LotParts', N'Части на партиди.'
exec spDescColumn N'LotParts', N'LotPartId'      , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotParts', N'LotId'          , N'Партида.'
exec spDescColumn N'LotParts', N'Path'           , N'Път.'
exec spDescColumn N'LotParts', N'TextContent'    , N'Съдържание.'
GO
