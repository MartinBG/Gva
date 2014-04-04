﻿PRINT 'GvaViewInventoryItems'
GO 

CREATE TABLE [dbo].[GvaViewInventoryItems] (
    [GvaViewInvItemId] INT              NOT NULL IDENTITY,
    [LotId]            INT              NOT NULL,
    [LotPartId]        INT              NOT NULL,
    [DocumentType]     NVARCHAR(50)     NOT NULL,
    [Name]             NVARCHAR(MAX)    NOT NULL,
    [Type]             NVARCHAR(MAX)    NULL,
    [Number]           NVARCHAR(50)     NULL,
    [Date]             DATETIME2(7)     NULL,
    [Publisher]        NVARCHAR(150)    NULL,
    [Valid]            BIT              NULL,
    [FromDate]         DATETIME2(7)     NULL,
    [ToDate]           DATETIME2(7)     NULL,
    [CreatedBy]        NVARCHAR(50)     NOT NULL,
    [CreationDate]     DATETIME2(7)     NOT NULL,
    [EditedBy]         NVARCHAR(50)     NULL,
    [EditedDate]       DATETIME2(7)     NULL,
    CONSTRAINT [PK_GvaViewInventoryItems]              PRIMARY KEY ([GvaViewInvItemId]),
    CONSTRAINT [FK_GvaViewInventoryItems_Lots]         FOREIGN KEY ([LotId])         REFERENCES [dbo].[Lots] ([LotId]),
    CONSTRAINT [FK_GvaViewInventoryItems_LotParts]     FOREIGN KEY ([LotPartId])     REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO

exec spDescTable  N'GvaViewInventoryItems', N'Опис.'
exec spDescColumn N'GvaViewInventoryItems', N'GvaViewInvItemId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'GvaViewInventoryItems', N'LotId'           , N'Идентификатор на партидата.'
exec spDescColumn N'GvaViewInventoryItems', N'LotPartId'       , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewInventoryItems', N'DocumentType'    , N'Вид документ.'
exec spDescColumn N'GvaViewInventoryItems', N'Name'            , N'Единен граждански номер.'
exec spDescColumn N'GvaViewInventoryItems', N'Type'            , N'Тип документ.'
exec spDescColumn N'GvaViewInventoryItems', N'Number'          , N'Номер на документ.'
exec spDescColumn N'GvaViewInventoryItems', N'Date'            , N'Дата.'
exec spDescColumn N'GvaViewInventoryItems', N'Publisher'       , N'Издател.'
exec spDescColumn N'GvaViewInventoryItems', N'Valid'           , N'Валиден.'
exec spDescColumn N'GvaViewInventoryItems', N'FromDate'        , N'Валиден от дата.'
exec spDescColumn N'GvaViewInventoryItems', N'ToDate'          , N'Валиден до дата.'
exec spDescColumn N'GvaViewInventoryItems', N'CreatedBy'       , N'Създател.'
exec spDescColumn N'GvaViewInventoryItems', N'CreationDate'    , N'Дата на създаване.'
exec spDescColumn N'GvaViewInventoryItems', N'EditedBy'        , N'Последно променен от.'
exec spDescColumn N'GvaViewInventoryItems', N'EditedDate'      , N'Дата на последна промяна.'
GO