GO
INSERT [dbo].[Noms] ([Name], [Alias], [Category]) VALUES (N'Тип s mode code', N'sModeCodeTypes', N'sModeCode')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]           , [NameAlt]      , [ParentValueId], [Alias]        , [IsActive])
VALUES
    (@nomId , N'S'  , N'РВД (squitter)', N'Squitter'    , NULL           , N'squitter'    , 1         ),
    (@nomId , N'M'  , N'Военен'        , N'Military'    , NULL           , N'military'    , 1         ),
    (@nomId , N'A'  , N'ВС'            , N'Aircraft'    , NULL           , N'aircraft'    , 1         )
GO
