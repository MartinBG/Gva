PRINT 'TextBlobs'
GO 

CREATE TABLE [dbo].[TextBlobs] (
    [TextBlobId]   INT            NOT NULL IDENTITY,
    [Hash]         NVARCHAR (50)  NOT NULL,
    [Size]         INT            NOT NULL,
    [TextContent]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_TextBlobs] PRIMARY KEY ([TextBlobId])
)
GO

exec spDescTable  N'TextBlobs', N'Текстови съдържания.'
exec spDescColumn N'TextBlobs', N'TextBlobId'   , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'TextBlobs', N'Hash'         , N'Хеш.'
exec spDescColumn N'TextBlobs', N'Size'         , N'Размер.'
exec spDescColumn N'TextBlobs', N'TextContent'  , N'Съдържание.'
GO
