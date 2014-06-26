PRINT 'LotSetParts'
GO 

CREATE TABLE [dbo].[LotSetParts] (
    [LotSetPartId]  int             NOT NULL IDENTITY,
    [LotSetId]      int             NOT NULL,
    [Alias]         NVARCHAR (100)  NOT NULL,
    [Name]          NVARCHAR (100)  NOT NULL,
    [PathRegex]     NVARCHAR (100)  NOT NULL,
    [LotSchemaId]   INT             NULL,
    CONSTRAINT [PK_LotSetParts] PRIMARY KEY ([LotSetPartId]),
    CONSTRAINT [UQ_LotSetParts_Alias_LotSetId] UNIQUE NONCLUSTERED ([Alias], [LotSetId]),
    CONSTRAINT [FK_LotSetParts_LotSets] FOREIGN KEY ([LotSetId]) REFERENCES [dbo].[LotSets] ([LotSetId]),
    CONSTRAINT [FK_LotSetParts_LotSchemas] FOREIGN KEY ([LotSchemaId]) REFERENCES [dbo].[LotSchemas] ([LotSchemaId])
)
GO

exec spDescTable  N'LotSetParts', N'Части на тип партида.'
exec spDescColumn N'LotSetParts', N'LotSetPartId' , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotSetParts', N'LotSetId'     , N'Тип партида.'
exec spDescColumn N'LotSetParts', N'Alias'        , N'Псевдоним.'
exec spDescColumn N'LotSetParts', N'Name'         , N'Име.'
exec spDescColumn N'LotSetParts', N'PathRegex'    , N'Път.'
exec spDescColumn N'LotSetParts', N'LotSchemaId'  , N'Идентификатор на схема.'
GO
