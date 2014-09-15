GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Статус на процедура', N'AopProcedureStatus')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]  , [Name]  , [NameAlt], [ParentValueId], [Alias] , [IsActive], [TextContent])
VALUES
    (@nomId , N'Type1', N'Вид 1', NULL     , NULL           , N'Type1', 1         , NULL         ),
    (@nomId , N'Type2', N'Вид 2', NULL     , NULL           , N'Type2', 1         , NULL         )
GO
