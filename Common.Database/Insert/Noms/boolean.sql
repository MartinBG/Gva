GO
INSERT [dbo].[Noms] ([Name], [Alias], [Category]) VALUES (N'Да/Не номенклатура', N'boolean', N'system')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]         , [NameAlt], [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'Y'  , N'Да'          , NULL     , NULL           , N'yes' , 1         , NULL         ),
    (@nomId , N'N'  , N'Не'          , NULL     , NULL           , N'no'  , 1         , NULL         )
GO
