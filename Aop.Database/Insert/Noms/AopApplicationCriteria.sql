GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Критерий', N'AopApplicationCriteria')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]         , [Name]                            , [NameAlt], [ParentValueId], [Alias]        , [IsActive], [TextContent])
VALUES
    (@nomId , N'Economical'  , N'Икономически най-изгодна оферта', NULL     , NULL           , N'Economical'  , 1         , NULL         ),
    (@nomId , N'LowestBiding', N'Най-ниска цена'                 , NULL     , NULL           , N'LowestBiding', 1         , NULL         )
GO
