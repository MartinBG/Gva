print 'GParams'
GO

CREATE TABLE [dbo].[GParams] (
    [Key]   NVARCHAR (50)   NOT NULL,
    [Value] NVARCHAR (500)  NULL
    CONSTRAINT [PK_GParams] PRIMARY KEY CLUSTERED ([Key] ASC)
);
GO

exec spDescTable  N'GParams', N'Системна таблица с настройки.'
exec spDescColumn N'GParams', N'Key', N'Ключ.'
exec spDescColumn N'GParams', N'Value', N'Стойност.'
GO
