PRINT 'GvaViewPersonDocuments'
GO 

CREATE TABLE [dbo].[GvaViewPersonDocuments] (
    [LotId]                INT           NOT NULL,
	[PartIndex]            INT           NOT NULL,
    [DocumentNumber]       NVARCHAR(50)  NOT NULL,
    [DocumentPersonNumber] INT           NULL,
    CONSTRAINT [PK_GvaViewPersonDocuments]                PRIMARY KEY ([LotId], [PartIndex], [DocumentNumber]),
    CONSTRAINT [FK_GvaViewPersonDocuments_GvaViewPersons] FOREIGN KEY ([LotId]) REFERENCES [dbo].[GvaViewPersons] ([LotId])
)
GO
exec spDescTable  N'GvaViewPersonDocuments', N'Документи на Физически лица.'
exec spDescColumn N'GvaViewPersonDocuments', N'LotId'                , N'Идентификатор на партида на физическо лице.'
exec spDescColumn N'DocumentNumber'        , N'DocumentNumber'       , N'Номер на документ.'
exec spDescColumn N'DocumentPersonNumber'  , N'DocumentPersonNumber' , N'No в списъка (групов док.).'
GO
