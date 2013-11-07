PRINT 'Logs'
GO

CREATE TABLE [dbo].[Logs](
    [LogId]     INT              NOT NULL IDENTITY,
    [Level]     NVARCHAR (50)    NOT NULL,
    [LogDate]   DATETIME         NULL,
    [IP]        NVARCHAR (50)    NULL,
    [RawUrl]    NVARCHAR (500)   NULL,
    [Form]      NVARCHAR (500)   NULL,
    [UserAgent] NVARCHAR (200)   NULL,
    [SessionId] NVARCHAR (50)    NULL,
    [RequestId] UNIQUEIDENTIFIER NULL,
    [Message]   NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY ([LogId])
);
GO