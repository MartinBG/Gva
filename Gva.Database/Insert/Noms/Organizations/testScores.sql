GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Номенклатура Оценка от писмен изпити', N'testScores')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]       , [NameAlt], [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'Y'  , N'Издържал'  , NULL     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'N'  , N'Неиздържал', NULL     , NULL           , NULL   , 1         , NULL         )
GO
