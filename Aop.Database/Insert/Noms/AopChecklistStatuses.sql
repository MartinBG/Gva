GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Статус на чеклист', N'AopChecklistStatus')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]    , [Name]     , [NameAlt], [ParentValueId], [Alias]   , [IsActive], [TextContent])
VALUES
    (@nomId , N'New'    , N'Нов'     , NULL     , NULL           , N'New'    , 1         , NULL         ),
    (@nomId , N'Copy'   , N'Копие'   , NULL     , NULL           , N'Copy'   , 1         , NULL         ),
    (@nomId , N'Correct', N'Поправка', NULL     , NULL           , N'Correct', 1         , NULL         )
GO
