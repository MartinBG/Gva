PRINT 'ASExamVariants'
GO 

CREATE TABLE [dbo].[ASExamVariants] (
    [ASExamVariantId]      INT           NOT NULL IDENTITY,
    [ASExamQuestionTypeId] INT           NOT NULL,
    [Name]                 NVARCHAR(500) NOT NULL,
    [Version]              ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ASExamVariants]       PRIMARY KEY ([ASExamVariantId])
)
GO

exec spDescTable  N'ASExamVariants', N'Варианти за теоретичен изпит АС.'
exec spDescColumn N'ASExamVariants', N'ASExamVariantId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ASExamVariants', N'Name', N'Наименование на вариант.'
exec spDescColumn N'ASExamVariants', N'Version', N'Версия.'
GO
