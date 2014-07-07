DECLARE @commonQuestionId INT;
SELECT @commonQuestionId = NomValueId FROM NomValues WHERE Alias = 'commonQuestions'
DECLARE @specializedQuestionId INT;
SELECT @specializedQuestionId = NomValueId FROM NomValues WHERE Alias = 'specializedQuestions'

SET IDENTITY_INSERT ASExamQuestions ON
INSERT INTO ASExamQuestions
    ([ASExamQuestionId], [ASExamQuestionTypeId], [QuestionText], [Answer1], [IsChecked1], [Answer2], [IsChecked2], [Answer3], [IsChecked3], [Answer4], [IsChecked4])
VALUES
    (1                 , @commonQuestionId     , N'Въпрос 1'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 1           , N'4'     , 1           ),
    (2                 , @commonQuestionId     , N'Въпрос 2'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 1           , N'4'     , 1           ),
    (3                 , @commonQuestionId     , N'Въпрос 3'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 1           , N'4'     , 1           ),
    (4                 , @commonQuestionId     , N'Въпрос 4'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 1           , N'4'     , 1           ),
    (5                 , @commonQuestionId     , N'Въпрос 5'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 1           , N'4'     , 1           ),
    (6                 , @commonQuestionId     , N'Въпрос 6'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (7                 , @commonQuestionId     , N'Въпрос 7'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (8                 , @commonQuestionId     , N'Въпрос 8'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (9                 , @commonQuestionId     , N'Въпрос 9'   , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (10                , @commonQuestionId     , N'Въпрос 10'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (11                , @specializedQuestionId, N'Въпрос 11'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (12                , @specializedQuestionId, N'Въпрос 12'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (13                , @specializedQuestionId, N'Въпрос 13'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (14                , @specializedQuestionId, N'Въпрос 14'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (15                , @specializedQuestionId, N'Въпрос 15'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (16                , @specializedQuestionId, N'Въпрос 16'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (17                , @specializedQuestionId, N'Въпрос 17'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (18                , @specializedQuestionId, N'Въпрос 18'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (19                , @specializedQuestionId, N'Въпрос 19'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (20                , @specializedQuestionId, N'Въпрос 20'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (21                , @specializedQuestionId, N'Въпрос 21'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (22                , @specializedQuestionId, N'Въпрос 22'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (23                , @specializedQuestionId, N'Въпрос 23'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (24                , @specializedQuestionId, N'Въпрос 24'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (25                , @specializedQuestionId, N'Въпрос 25'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (26                , @specializedQuestionId, N'Въпрос 26'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (27                , @specializedQuestionId, N'Въпрос 27'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (28                , @specializedQuestionId, N'Въпрос 28'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (29                , @specializedQuestionId, N'Въпрос 29'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           ),
    (30                , @specializedQuestionId, N'Въпрос 30'  , N'1'     , 1           , N'2'    , 1            , N'3'     , 0           , N'4'     , 0           )
GO
SET IDENTITY_INSERT ASExamQuestions OFF
GO

------------------------------

DECLARE @commonQuestionId INT;
SELECT @commonQuestionId = NomValueId FROM NomValues WHERE Alias = 'commonQuestions'
DECLARE @specializedQuestionId INT;
SELECT @specializedQuestionId = NomValueId FROM NomValues WHERE Alias = 'specializedQuestions'

SET IDENTITY_INSERT [ASExamVariants] ON
INSERT INTO [ASExamVariants]
    ([ASExamVariantId], [ASExamQuestionTypeId], [Name]) 
VALUES
    (1                , @commonQuestionId     , N'1.1'),
    (2                , @commonQuestionId     , N'1.2'),
    (3                , @commonQuestionId     , N'1.3'),
    (4                , @specializedQuestionId, N'2.1'),
    (5                , @specializedQuestionId, N'2.2'),
    (6                , @specializedQuestionId, N'2.3')
GO
SET IDENTITY_INSERT [ASExamVariants] OFF
GO

------------------------------

DECLARE @commonQuestionId INT;
SELECT @commonQuestionId = NomValueId FROM NomValues WHERE Alias = 'commonQuestions'
DECLARE @specializedQuestionId INT;
SELECT @specializedQuestionId = NomValueId FROM NomValues WHERE Alias = 'specializedQuestions'

SET IDENTITY_INSERT [ASExamVariantQuestions] ON
INSERT INTO [dbo].[ASExamVariantQuestions]
    ([ASExamVariantQuestionId], [ASExamVariantId], [ASExamQuestionId], [QuestionNumber])
VALUES
    (1                        , 1                , 1                 , 1               ),
    (2                        , 1                , 2                 , 2               ),
    (3                        , 1                , 3                 , 3               ),
    (4                        , 1                , 4                 , 4               ),
    (5                        , 1                , 5                 , 5               ),
    (6                        , 1                , 6                 , 6               ),
    (7                        , 1                , 7                 , 7               ),
    (8                        , 1                , 8                 , 8               ),
    (9                        , 1                , 9                 , 9               ),
    (10                       , 1                , 10                , 10              ),
    (11                       , 4                , 11                , 1               ),
    (12                       , 4                , 12                , 2               ),
    (13                       , 4                , 13                , 3               ),
    (14                       , 4                , 14                , 4               ),
    (15                       , 4                , 15                , 5               ),
    (16                       , 4                , 16                , 6               ),
    (17                       , 4                , 17                , 7               ),
    (18                       , 4                , 18                , 8               ),
    (19                       , 4                , 19                , 9               ),
    (20                       , 4                , 20                , 10              ),
    (21                       , 4                , 21                , 11              ),
    (22                       , 4                , 22                , 12              ),
    (23                       , 4                , 23                , 13              ),
    (24                       , 4                , 24                , 14              ),
    (25                       , 4                , 25                , 15              ),
    (26                       , 4                , 26                , 16              ),
    (27                       , 4                , 27                , 17              ),
    (28                       , 4                , 28                , 18              ),
    (29                       , 4                , 29                , 19              ),
    (30                       , 4                , 30                , 20              )
GO
SET IDENTITY_INSERT [ASExamVariantQuestions] OFF
GO
