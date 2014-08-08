PRINT 'GvaViewInventoryItems'
GO 

CREATE TABLE [dbo].[GvaViewInventoryItems] (
    [LotId]            INT              NOT NULL,
    [LotPartId]        INT              NOT NULL,
    [SetPartAlias]     NVARCHAR(100)    NOT NULL,
    [Name]             NVARCHAR(MAX)    NOT NULL,
    [TypeId]           INT              NULL,
    [Number]           NVARCHAR(100)    NULL,
    [Date]             DATETIME2        NULL,
    [Publisher]        NVARCHAR(150)    NULL,
    [Valid]            BIT              NULL,
    [FromDate]         DATETIME2        NULL,
    [ToDate]           DATETIME2        NULL,
    [Notes]            NVARCHAR(MAX)    NULL,
    [CreatedBy]        NVARCHAR(50)     NOT NULL,
    [CreationDate]     DATETIME2        NOT NULL,
    [EditedBy]         NVARCHAR(50)     NULL,
    [EditedDate]       DATETIME2        NULL,
    CONSTRAINT [PK_GvaViewInventoryItems]              PRIMARY KEY ([LotId], [LotPartId]),
    CONSTRAINT [FK_GvaViewInventoryItems_Lots]         FOREIGN KEY ([LotId])         REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewInventoryItems_LotParts]     FOREIGN KEY ([LotPartId])     REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewInventoryItems_NomValues]    FOREIGN KEY ([TypeId])        REFERENCES [dbo].[NomValues] ([NomValueId])
)
GO

exec spDescTable  N'GvaViewInventoryItems', N'Опис.'
exec spDescColumn N'GvaViewInventoryItems', N'LotId'           , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewInventoryItems', N'LotPartId'       , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewInventoryItems', N'SetPartAlias'    , N'Вид документ.'
exec spDescColumn N'GvaViewInventoryItems', N'Name'            , N'Единен граждански номер.'
exec spDescColumn N'GvaViewInventoryItems', N'TypeId'          , N'Тип документ.'
exec spDescColumn N'GvaViewInventoryItems', N'Number'          , N'Номер на документ.'
exec spDescColumn N'GvaViewInventoryItems', N'Date'            , N'Дата.'
exec spDescColumn N'GvaViewInventoryItems', N'Publisher'       , N'Издател.'
exec spDescColumn N'GvaViewInventoryItems', N'Valid'           , N'Валиден.'
exec spDescColumn N'GvaViewInventoryItems', N'FromDate'        , N'Валиден от дата.'
exec spDescColumn N'GvaViewInventoryItems', N'ToDate'          , N'Валиден до дата.'
exec spDescColumn N'GvaViewInventoryItems', N'Notes'           , N'Бележки.'
exec spDescColumn N'GvaViewInventoryItems', N'CreatedBy'       , N'Създател.'
exec spDescColumn N'GvaViewInventoryItems', N'CreationDate'    , N'Дата на създаване.'
exec spDescColumn N'GvaViewInventoryItems', N'EditedBy'        , N'Последно променен от.'
exec spDescColumn N'GvaViewInventoryItems', N'EditedDate'      , N'Дата на последна промяна.'
GO
