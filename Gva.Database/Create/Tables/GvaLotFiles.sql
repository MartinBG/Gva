PRINT 'GvaLotFiles'
GO 

CREATE TABLE [dbo].[GvaLotFiles] (
    [GvaLotFileId]      INT            NOT NULL IDENTITY,
    [LotPartId]         INT            NOT NULL,
    [GvaFileId]         INT            NULL,
    [DocFileId]         INT            NULL,
    [GvaCaseTypeId]     INT            NOT NULL,
    [PageIndex]         NVARCHAR (50)  NULL,
    [PageIndexInt]      INT            NULL,
    [PageNumber]        INT            NULL,
    [Note]              NVARCHAR (100) NULL,
    CONSTRAINT [PK_GvaLotFiles]                 PRIMARY KEY ([GvaLotFileId]),
    CONSTRAINT [FK_GvaLotFiles_DocFiles]        FOREIGN KEY([DocFileId])        REFERENCES [dbo].[DocFiles]     ([DocFileId]),
    CONSTRAINT [FK_GvaLotFiles_GvaFiles]        FOREIGN KEY([GvaFileId])        REFERENCES [dbo].[GvaFiles]     ([GvaFileId]),
    CONSTRAINT [FK_GvaLotFiles_LotParts]        FOREIGN KEY([LotPartId])        REFERENCES [dbo].[LotParts]     ([LotPartId]),
    CONSTRAINT [FK_GvaLotFiles_GvaCaseTypes]    FOREIGN KEY([GvaCaseTypeId])    REFERENCES [dbo].[GvaCaseTypes] ([GvaCaseTypeId])
)
GO

exec spDescTable  N'GvaLotFiles', N'Файлове от описа.'
exec spDescColumn N'GvaLotFiles', N'GvaLotFileId'     , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaLotFiles', N'LotPartId'        , N'Част от партидата.'
exec spDescColumn N'GvaLotFiles', N'GvaFileId'        , N'Файл.'
exec spDescColumn N'GvaLotFiles', N'DocFileId'        , N'Файл от документооборота.'
exec spDescColumn N'GvaLotFiles', N'GvaCaseTypeId'    , N'Тип дело.'
exec spDescColumn N'GvaLotFiles', N'PageIndex'        , N'Номер на страницата в описа.'
exec spDescColumn N'GvaLotFiles', N'PageNumber'       , N'Брой страници.'
exec spDescColumn N'GvaLotFiles', N'Note'             , N'Бележка.'
GO

CREATE NONCLUSTERED INDEX [IDX_GvaLotFiles_LotPartId] ON [dbo].[GvaLotFiles] ([LotPartId])
GO
