PRINT 'ASExamQuestions'
GO 

CREATE TABLE [dbo].[ASExamQuestions] (
    [ASExamQuestionId]     INT           NOT NULL IDENTITY,
    [ASExamQuestionTypeId] INT           NOT NULL,
    [QuestionText]         NVARCHAR(MAX) NOT NULL,
    [Answer1]              NVARCHAR(MAX) NOT NULL,
    [IsChecked1]           BIT           NOT NULL,
    [Answer2]              NVARCHAR(MAX) NOT NULL,
    [IsChecked2]           BIT           NOT NULL,
    [Answer3]              NVARCHAR(MAX) NOT NULL,
    [IsChecked3]           BIT           NOT NULL,
    [Answer4]              NVARCHAR(MAX) NOT NULL,
    [IsChecked4]           BIT           NOT NULL,
    [Version]              ROWVERSION    NOT NULL,

    CONSTRAINT [PK_ASExamQuestions]      PRIMARY KEY ([ASExamQuestionId])
)
GO

exec spDescTable  N'ASExamQuestions', N'Въпроси за теоретичен изпит АС.'
exec spDescColumn N'ASExamQuestions', N'ASExamQuestionId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ASExamQuestions', N'ASExamQuestionTypeId', N'Тип на въпрос.'
exec spDescColumn N'ASExamQuestions', N'QuestionText', N'Текст на въпрос'
exec spDescColumn N'ASExamQuestions', N'Answer1', N'Текст отговор 1.'
exec spDescColumn N'ASExamQuestions', N'Answer2', N'Текст отговор 2.'
exec spDescColumn N'ASExamQuestions', N'Answer3', N'Текст отговор 3.'
exec spDescColumn N'ASExamQuestions', N'Answer4', N'Текст отговор 4.'
exec spDescColumn N'ASExamQuestions', N'IsChecked1', N'Верен отговор 1.'
exec spDescColumn N'ASExamQuestions', N'IsChecked2', N'Верен отговор 2.'
exec spDescColumn N'ASExamQuestions', N'IsChecked3', N'Верен отговор 3.'
exec spDescColumn N'ASExamQuestions', N'IsChecked4', N'Верен отговор 4.'
exec spDescColumn N'ASExamQuestions', N'Version', N'Версия.'
GO
