GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Нива на несъответстие', N'disparityLevels')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name], [NameAlt], [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'0'  , N'0'  , NULL     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'1'  , N'1'  , NULL     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'2'  , N'2'  , NULL     , NULL           , NULL   , 1         , NULL         )
GO
