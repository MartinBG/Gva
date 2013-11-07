PRINT 'Common.ActionLogs'
GO

CREATE TABLE [dbo].[ActionLogs](
    [Id]          [int] IDENTITY(1,1) NOT NULL,
    [LogDate]  [datetime]          NULL,
    [IP]          [nvarchar](50)      NULL,
    [RawUrl]      [nvarchar](500)     NULL,
    [Form]        [nvarchar](500)     NULL,
    [BrowserInfo] [nvarchar](200)     NULL,
    [SessionId]   [nvarchar](50)      NULL,
    [RequestId]   [uniqueidentifier]  NULL,
    [Message]     [nvarchar](MAX)     NULL,
    CONSTRAINT [PK_ActionLogs] PRIMARY KEY ([Id])
);