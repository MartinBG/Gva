PRINT 'GvaInventoryItems'
GO 

CREATE TABLE [dbo].[GvaInventoryItems] (
    [GvaInventoryItemId] INT          NOT NULL IDENTITY,
    [LotId]              INT          NOT NULL,
    [LotPartId]          INT          NOT NULL,
    [DocumentType]       NVARCHAR(50) NOT NULL,
    [Name]               NVARCHAR(50) NOT NULL,
    [BookPageNumber]     NVARCHAR(50) NOT NULL,
    [PageCount]          INT          NULL,
    [Type]               NVARCHAR(50) NULL,
    [Number]             NVARCHAR(50) NULL,
    [Date]               DATETIME2(7) NULL,
    [Publisher]          NVARCHAR(50) NOT NULL,
    [Valid]              BIT          NULL,
    [FromDate]           DATETIME2(7) NULL,
    [ToDate]             DATETIME2(7) NULL,
    [CreatedBy]          NVARCHAR(50) NOT NULL,
    [CreationDate]       DATETIME2(7) NOT NULL,
    [EditedBy]           NVARCHAR(50) NULL,
    [EditedDate]         DATETIME2(7) NULL,
    CONSTRAINT [PK_GvaInventoryItems]          PRIMARY KEY ([GvaInventoryItemId]),
    CONSTRAINT [FK_GvaInventoryItems_Lots]     FOREIGN KEY ([LotId])          REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaInventoryItems_LotParts] FOREIGN KEY ([LotPartId])      REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO

exec spDescTable  N'GvaInventoryItems', N'Опис.'
exec spDescColumn N'GvaInventoryItems', N'GvaInventoryItemId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaInventoryItems', N'LotId'             , N'Идентификатор на партидата.'
exec spDescColumn N'GvaInventoryItems', N'LotPartId'         , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaInventoryItems', N'DocumentType'      , N'Вид документ.'
exec spDescColumn N'GvaInventoryItems', N'Name'              , N'Единен граждански номер.'
exec spDescColumn N'GvaInventoryItems', N'BookPageNumber'    , N'Номер на страница в делов. книга.'
exec spDescColumn N'GvaInventoryItems', N'PageCount'         , N'Брой страници в делов. книга.'
exec spDescColumn N'GvaInventoryItems', N'Type'              , N'Тип документ.'
exec spDescColumn N'GvaInventoryItems', N'Number'            , N'Номер на документ.'
exec spDescColumn N'GvaInventoryItems', N'Date'              , N'Дата.'
exec spDescColumn N'GvaInventoryItems', N'Publisher'         , N'Издател.'
exec spDescColumn N'GvaInventoryItems', N'Valid'             , N'Валиден.'
exec spDescColumn N'GvaInventoryItems', N'FromDate'          , N'Валиден от дата.'
exec spDescColumn N'GvaInventoryItems', N'ToDate'            , N'Валиден до дата.'
exec spDescColumn N'GvaInventoryItems', N'CreatedBy'         , N'Създател.'
exec spDescColumn N'GvaInventoryItems', N'CreationDate'      , N'Дата на създаване.'
exec spDescColumn N'GvaInventoryItems', N'EditedBy'          , N'Последно променен от.'
exec spDescColumn N'GvaInventoryItems', N'EditedDate'        , N'Дата на последна промяна.'
GO
