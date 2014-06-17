GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Да/Не номенклатура', N'boolean')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]         , [NameAlt], [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'Y'  , N'Персонал'    , NULL     , NULL           , N'yes' , 1         , NULL         ),
    (@nomId , N'N'  , N'Организация' , NULL     , NULL           , N'no'  , 1         , NULL         )
GO
