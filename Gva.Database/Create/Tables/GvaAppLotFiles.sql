PRINT 'GvaAppLotFiles'
GO 

CREATE TABLE [dbo].[GvaAppLotFiles] (
    [GvaApplicationId]  INT   NOT NULL,
    [GvaLotFileId]      INT   NOT NULL,
    [DocFileId]         INT   NULL,
    CONSTRAINT [PK_GvaAppLotFiles]                 PRIMARY KEY ([GvaApplicationId], [GvaLotFileId]),
    CONSTRAINT [FK_GvaAppLotFiles_DocFiles]        FOREIGN KEY ([DocFileId])        REFERENCES [dbo].[DocFiles]        ([DocFileId]),
    CONSTRAINT [FK_GvaAppLotFiles_GvaApplications] FOREIGN KEY ([GvaApplicationId]) REFERENCES [dbo].[GvaApplications] ([GvaApplicationId]),
    CONSTRAINT [FK_GvaAppLotFiles_GvaLotFiles]     FOREIGN KEY ([GvaLotFileId])     REFERENCES [dbo].[GvaLotFiles]     ([GvaLotFileId])
)
GO

exec spDescTable  N'GvaAppLotFiles', N'Файлове към заявление.'
exec spDescColumn N'GvaAppLotFiles', N'GvaApplicationId'  , N'Заявление.'
exec spDescColumn N'GvaAppLotFiles', N'DocFileId'         , N'Файл от документооборота.'
exec spDescColumn N'GvaAppLotFiles', N'GvaLotFileId'      , N'Файл от описа.'
GO
