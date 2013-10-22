PRINT 'GvaFiles'
GO 

CREATE TABLE [dbo].[GvaFiles] (
    [GvaFileId]     INT           NOT NULL IDENTITY,
    [Filename]      NVARCHAR (50) NOT NULL,
    [FileContentId] INT           NOT NULL,
    CONSTRAINT [PK_GvaFiles] PRIMARY KEY ([GvaFileId])
)
GO

exec spDescTable  N'GvaFiles', N'Файлове.'
exec spDescColumn N'GvaFiles', N'GvaFileId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaFiles', N'Filename'      , N'Име на файла.'
exec spDescColumn N'GvaFiles', N'FileContentId' , N'Съдържание на файла.'
GO
