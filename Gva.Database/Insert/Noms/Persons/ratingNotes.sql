GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Бележки за квалификационен клас', N'ratingNotes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]                  , [Name]                 , [NameAlt]              ,[ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'bulgarian_english_bg' , N'Български, Английски', N'Български, Английски', NULL          , NULL   , 1         , NULL  ),
	(@nomId , N'bulgarian_english'    , N'Bulgarian, English'  , N'Bulgarian, English'  , NULL          , NULL   , 1         , NULL  )
GO
