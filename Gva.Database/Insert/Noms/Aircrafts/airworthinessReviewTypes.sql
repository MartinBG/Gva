GO
INSERT INTO Noms (Name, Alias) VALUES ('AC cert type','airworthinessReviewTypes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]                                 , [NameAlt]                                , [ParentValueId], [Alias]  , [IsActive], [TextContent])
VALUES
    (@nomId , NULL  , N'Неизвестен'                          , N'Unknown'                               , NULL           , 'unknown', 1         , NULL         ),
    (@nomId , NULL  , N'Удостоверение за преглед за ЛГ (15a)', N'Airworthiness review certificate (15a)', NULL           , '15a'    , 1         , NULL         ),
    (@nomId , NULL  , N'Удостоверение за преглед за ЛГ (15b)', N'Airworthiness review certificate (15b)', NULL           , '15b'    , 1         , NULL         )
GO
