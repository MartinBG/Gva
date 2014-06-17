GO
INSERT INTO Noms (Name, Alias) VALUES ('AC cert type','airworthinessReviewTypes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]                                 , [NameAlt]               , [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'AV' , N'Удостоверение за преглед за ЛГ (15a)', N'Paramotor-Trike'      , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'AW' , N'Удостоверение за преглед за ЛГ (15b)', N'Very Light Rotorcraft', NULL           , NULL   , 1         , NULL         )
GO
