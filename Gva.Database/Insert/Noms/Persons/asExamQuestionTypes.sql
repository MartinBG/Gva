GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Типове въпроси', N'asExamQuestionTypes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]                 , [Name]                   , [NameAlt]               , [ParentValueId], [Alias]                , [IsActive], [TextContent])
VALUES
    (@nomId , N'commonQuestions'     , N'Основни въпроси'       , N'Common questions'     , NULL           , N'commonQuestions'     , 1         , NULL         ),
    (@nomId , N'specializedQuestions', N'Специализирани въпроси', N'Specialized questions', NULL           , N'specializedQuestions', 1         , NULL         )
GO
