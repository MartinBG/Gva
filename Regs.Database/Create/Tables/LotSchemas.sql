PRINT 'LotSchemas'
GO 

CREATE TABLE [dbo].[LotSchemas] (
    [LotSchemaId]  INT              NOT NULL IDENTITY,
    [SchemaText]    NVARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_LotSchemas] PRIMARY KEY ([LotSchemaId])
)
GO

exec spDescTable  N'LotSchemas', N'Схеми към партида.'
exec spDescColumn N'LotSchemas', N'LotSchemaId'  , N'Уникален системно генериран идентификатор.'
exec spDescColumn N'LotSchemas', N'SchemaText'   , N'Текст на схемата.'
GO
