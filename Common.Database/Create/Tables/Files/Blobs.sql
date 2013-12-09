PRINT 'Blobs'
GO

CREATE TABLE [dbo].[Blobs] (
    [BlobId]        INT                         NOT NULL IDENTITY,
    [Key]           UNIQUEIDENTIFIER ROWGUIDCOL NOT NULL UNIQUE,
    [Hash]          NVARCHAR(40)                NULL,
    [Size]          INT                         NULL,
    [Content]       VARBINARY(MAX)              NULL,
    [IsDeleted]     BIT                         NOT NULL,
    CONSTRAINT [PK_Blobs] PRIMARY KEY CLUSTERED ([BlobId] ASC)
);
GO

CREATE UNIQUE INDEX [UQ_Blobs_Hash_Size]
    ON [dbo].[Blobs]([Hash], [Size]) WHERE [Hash] IS NOT NULL AND [Size] IS NOT NULL
GO

exec spDescTable  N'Blobs', N'Файлово съдържание.'
exec spDescColumn N'Blobs', N'BlobId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Blobs', N'Key', N'Уникален идентификатор за извличане на файловото съдържание.'
exec spDescColumn N'Blobs', N'Hash', N'Уникален идентификатор на съдържанието на файла.'
exec spDescColumn N'Blobs', N'Size', N'Размер на съдържанието.'
exec spDescColumn N'Blobs', N'Content', N'Съдържание.'
exec spDescColumn N'Blobs', N'IsDeleted', N'Маркер за изтриване.'
GO
