GO
INSERT [dbo].[Noms] ([Name], [Alias], [Category]) VALUES (N'Тип съоръжение', N'equipmentTypes', N'equipment')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]  , [NameAlt], [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'T1' , N'Тип 1', N'Type 1', NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'T2' , N'Тип 2', N'Type 2', NULL           , NULL   , 1         , NULL         )
GO
