GO
INSERT [dbo].[Noms] ([Name], [Alias]) VALUES (N'Обект', N'AopApplicationObject')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code]         , [Name]         , [NameAlt], [ParentValueId], [Alias]        , [IsActive], [TextContent])
VALUES
    (@nomId , N'Construction', N'Строителство', NULL     , NULL           , N'Construction', 1         , NULL         ),
    (@nomId , N'Deliveries'  , N'Доставки'    , NULL     , NULL           , N'Deliveries'  , 1         , NULL         ),
    (@nomId , N'Services'    , N'Услуги'      , NULL     , NULL           , N'Services'    , 1         , NULL         )
GO
