GO
INSERT [dbo].[Noms] ([Name], [Alias], [Category]) VALUES (N'Номер на част от доклад', N'recommendationPartNumbers', N'orgCommon')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name], [NameAlt], [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'1'  , N'1'  , N'1'     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'2'  , N'2'  , N'2'     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'3'  , N'3'  , N'3'     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'4'  , N'4'  , N'4'     , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'5'  , N'5'  , N'5'     , NULL           , NULL   , 1         , NULL         )
GO
