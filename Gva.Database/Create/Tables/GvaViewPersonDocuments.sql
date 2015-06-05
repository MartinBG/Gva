PRINT 'GvaViewPersonDocuments'
GO 

CREATE TABLE [dbo].[GvaViewPersonDocuments] (
    [LotId]                 INT           NOT NULL,
    [LotPartId]             INT           NOT NULL,
    [ParentLotPartId]       INT           NULL,
    [SetPartAlias]          NVARCHAR(100) NOT NULL,
    [DocumentNumber]        NVARCHAR(50)  NULL,
    [DocumentPersonNumber]  INT           NULL,
    [TypeId]                INT           NULL,
    [RoleId]                INT           NULL,
    [Publisher]             NVARCHAR(150) NULL,
    [Limitations]           NVARCHAR(150) NULL,
    [MedClassId]            INT           NULL,
    [Date]                  DATETIME2     NULL,
    [Valid]                 BIT           NULL,
    [FromDate]              DATETIME2     NULL,
    [ToDate]                DATETIME2     NULL,
    [Notes]                 NVARCHAR(MAX) NULL,
    [CreatedBy]             NVARCHAR(50)  NOT NULL,
    [CreationDate]          DATETIME2     NOT NULL,
    [EditedBy]              NVARCHAR(50)  NULL,
    [EditedDate]            DATETIME2     NULL,
    CONSTRAINT [PK_GvaViewPersonDocuments]                PRIMARY KEY ([LotId], [LotPartId]),
    CONSTRAINT [FK_GvaViewPersonDocuments_GvaViewPersons] FOREIGN KEY ([LotId])           REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonDocuments_NomValues]      FOREIGN KEY ([TypeId])          REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonDocuments_NomValues2]     FOREIGN KEY ([RoleId])          REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonDocuments_NomValues3]     FOREIGN KEY ([MedClassId])      REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonDocuments_LotParts]       FOREIGN KEY ([LotPartId])       REFERENCES [dbo].[LotParts] ([LotPartId]),
    CONSTRAINT [FK_GvaViewPersonDocuments_LotParts2]      FOREIGN KEY ([ParentLotPartId]) REFERENCES [dbo].[LotParts] ([LotPartId])
)
GO
exec spDescTable  N'GvaViewPersonDocuments', N'Документи на Физически лица.'
exec spDescColumn N'GvaViewPersonDocuments', N'LotId'                , N'Идентификатор на партида на физическо лице.'
exec spDescColumn N'GvaViewPersonDocuments', N'DocumentNumber'       , N'Номер на документ.'
exec spDescColumn N'GvaViewPersonDocuments', N'DocumentPersonNumber' , N'No в списъка (групов док.).'
exec spDescColumn N'GvaViewPersonDocuments', N'TypeId'               , N'Тип на документа.'
exec spDescColumn N'GvaViewPersonDocuments', N'RoleId'               , N'Роля на документа.'
exec spDescColumn N'GvaViewPersonDocuments', N'Publisher'            , N'Издател на документа.'
exec spDescColumn N'GvaViewPersonDocuments', N'Limitations'          , N'Ограничения (за медицинските).'
exec spDescColumn N'GvaViewPersonDocuments', N'MedClassId'           , N'Клас на медицинско'
exec spDescColumn N'GvaViewPersonDocuments', N'LotPartId'            , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewPersonDocuments', N'ParentLotPartId'      , N'Идентификатор на част от партидата.'
exec spDescColumn N'GvaViewPersonDocuments', N'SetPartAlias'         , N'Вид документ.'
exec spDescColumn N'GvaViewPersonDocuments', N'Date'                 , N'Дата.'
exec spDescColumn N'GvaViewPersonDocuments', N'Valid'                , N'Валиден.'
exec spDescColumn N'GvaViewPersonDocuments', N'FromDate'             , N'Валиден от дата.'
exec spDescColumn N'GvaViewPersonDocuments', N'ToDate'               , N'Валиден до дата.'
exec spDescColumn N'GvaViewPersonDocuments', N'Notes'                , N'Бележки.'
exec spDescColumn N'GvaViewPersonDocuments', N'CreatedBy'            , N'Създател.'
exec spDescColumn N'GvaViewPersonDocuments', N'CreationDate'         , N'Дата на създаване.'
exec spDescColumn N'GvaViewPersonDocuments', N'EditedBy'             , N'Последно променен от.'
exec spDescColumn N'GvaViewPersonDocuments', N'EditedDate'           , N'Дата на последна промяна.'
GO
