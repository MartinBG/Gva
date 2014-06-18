GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Регистър ВС', N'registers')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]       , [NameAlt]    , [ParentValueId], [Alias]     , [IsActive], [TextContent]         )
VALUES
    (@nomId , N'1'  , N'Регистър 1', N'Register 1', NULL           , N'register1', 1         , N'{"regPrefix":""}'   ),
    (@nomId , N'2'  , N'Регистър 2', N'Register 2', NULL           , N'register2', 1         , N'{"regPrefix":"II-"}')
GO
