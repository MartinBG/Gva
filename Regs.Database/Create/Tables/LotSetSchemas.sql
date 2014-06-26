PRINT 'LotSetSchemas'
GO 

CREATE TABLE [dbo].[LotSetSchemas] (
    [LotSetId]      INT NOT NULL,
    [LotSchemaId]   INT NOT NULL,
    CONSTRAINT [PK_LotSetSchemas] PRIMARY KEY ([LotSetId], [LotSchemaId]),
    CONSTRAINT [FK_LotSetSchemas_LotSets] FOREIGN KEY ([LotSetId]) REFERENCES [dbo].[LotSets] ([LotSetId]),
    CONSTRAINT [FK_LotSetSchemas_LotSchemas] FOREIGN KEY ([LotSchemaId]) REFERENCES [dbo].[LotSchemas] ([LotSchemaId])
)
GO

exec spDescTable  N'LotSetSchemas', N'Схеми към партида.'
exec spDescColumn N'LotSetSchemas', N'LotSetId'     , N'Идентификатор на партида.'
exec spDescColumn N'LotSetSchemas', N'LotSchemaId'  , N'Идентификатор на схема.'
GO
