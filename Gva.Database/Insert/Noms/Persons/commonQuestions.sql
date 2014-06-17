GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Статут на част', N'commonQuestions')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name], [NameAlt], [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'S1' , N'1.1', N'1.1'   , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'S2' , N'1.2', N'1.2'   , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'S2' , N'1.3', N'1.3'   , NULL           , NULL   , 1         , NULL         )
GO
