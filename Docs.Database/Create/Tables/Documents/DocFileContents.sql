print 'DocFileContents'
GO 

--//TODO2
CREATE TABLE DocFileContents
(
    DocFileContentId	    INT                         NOT NULL IDENTITY(1,1),
    [Key]                   UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL UNIQUE,
    [Hash]                  NVARCHAR(40)                NOT NULL,
    [Size]                  INT                         NOT NULL,
    [Content]               VARBINARY(MAX)              NOT NULL,

    CONSTRAINT PK_DocFileContents PRIMARY KEY CLUSTERED (DocFileContentId),
    CONSTRAINT [UQ_DocFileContents_Hash_Size] UNIQUE NONCLUSTERED ([Hash], [Size])
)
GO 

exec spDescTable  N'DocFileContents', N'Файлово съдържание.'
exec spDescColumn N'DocFileContents', N'DocFileContentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'DocFileContents', N'Key', N'Уникален идентификатор за извличане на файловото съдържание.'
exec spDescColumn N'DocFileContents', N'Hash', N'Уникален идентификатор на съдържанието на файла.'
exec spDescColumn N'DocFileContents', N'Size', N'Размер на съдържанието.'
exec spDescColumn N'DocFileContents', N'Content', N'Съдържание.'
GO
