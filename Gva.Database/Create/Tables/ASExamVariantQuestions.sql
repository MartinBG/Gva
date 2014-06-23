PRINT 'ASExamVariantQuestions'
GO 

CREATE TABLE [dbo].[ASExamVariantQuestions] (
    [ASExamVariantQuestionId] INT NOT NULL IDENTITY,
    [ASExamVariantId]         INT NOT NULL,
    [ASExamQuestionId]        INT NOT NULL,
    [QuestionNumber]          INT NOT NULL,

    CONSTRAINT [PK_ASExamVariantQuestions]                 PRIMARY KEY ([ASExamVariantQuestionId]),
    CONSTRAINT [FK_ASExamVariantQuestions_ASExamVariants]  FOREIGN KEY ([ASExamVariantId])  REFERENCES [dbo].[ASExamVariants]  ([ASExamVariantId]),
    CONSTRAINT [FK_ASExamVariantQuestions_ASExamQuestions] FOREIGN KEY ([ASExamQuestionId]) REFERENCES [dbo].[ASExamQuestions] ([ASExamQuestionId])
)
GO

exec spDescTable  N'ASExamVariantQuestions', N'Свързаност на варианти и въпроси.'
exec spDescColumn N'ASExamVariantQuestions', N'ASExamVariantQuestionId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ASExamVariantQuestions', N'ASExamVariantId', N'Идентификатор на вариант.'
exec spDescColumn N'ASExamVariantQuestions', N'ASExamQuestionId', N'Идентификатор на въпрос.'
exec spDescColumn N'ASExamVariantQuestions', N'QuestionNumber', N'Пореден номер на въпрос.'
GO
