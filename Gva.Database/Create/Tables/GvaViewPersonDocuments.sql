PRINT 'GvaViewPersonDocuments'
GO 

CREATE TABLE [dbo].[GvaViewPersonDocuments] (
    [LotId]                 INT           NOT NULL,
    [PartIndex]             INT           NOT NULL,
    [DocumentNumber]        NVARCHAR(50)  NULL,
    [DocumentPersonNumber]  INT           NULL,
    [TypeId]                INT           NULL,
    [RoleId]                INT           NULL,
    [Publisher]             NVARCHAR(150) NULL,
    [Limitations]           NVARCHAR(150) NULL,
    [DateValidFrom]         DATETIME2     NULL,
    CONSTRAINT [PK_GvaViewPersonDocuments]                PRIMARY KEY ([LotId], [PartIndex]),
    CONSTRAINT [FK_GvaViewPersonDocuments_GvaViewPersons] FOREIGN KEY ([LotId])  REFERENCES [dbo].[GvaViewPersons] ([LotId]),
    CONSTRAINT [FK_GvaViewPersonDocuments_NomValues]      FOREIGN KEY ([TypeId]) REFERENCES [dbo].[NomValues] ([NomValueId]),
    CONSTRAINT [FK_GvaViewPersonDocuments_NomValues2]     FOREIGN KEY ([RoleId]) REFERENCES [dbo].[NomValues] ([NomValueId])
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
exec spDescColumn N'GvaViewPersonDocuments', N'DateValidFrom'        , N'Дата на издаване на документа.'
GO
