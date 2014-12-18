GO
INSERT [dbo].[Noms] ([Name], [Alias], [Category]) VALUES (N'Производител', N'equipmentProducers', N'equipment')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]           , [NameAlt]    , [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'P1' , N'Производител 1', N'Producer 1', NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'P2' , N'Производител 2', N'Producer 2', NULL           , NULL   , 1         , NULL         )
GO
