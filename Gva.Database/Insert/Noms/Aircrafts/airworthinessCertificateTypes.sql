GO
INSERT INTO Noms (Name, Alias) VALUES ('Тип летателна годност','airworthinessCertificateTypes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]                                                , [NameAlt]                                  , [ParentValueId], [Alias]     , [IsActive], [TextContent])
VALUES
    (@nomId , NULL  , N'Неизвестен'                                         , N'Unknown'                                 , NULL           , 'unknown'   , 1         , NULL         ),
    (@nomId , NULL  , N'Удостоверение за летателна годност (F25)'           , N'Airworthiness certificate (F25)'         , NULL           , 'f25'       , 1         , NULL         ),
    (@nomId , NULL  , N'Ограничено удостоверение за летателна годност (F24)', N'Limited airworthiness certificate (F24)' , NULL           , 'f24'       , 1         , NULL         ),
    (@nomId , NULL  , N'Специално удостоверение за летателна годност'       , N'Special airworthiness certificate'       , NULL           , 'special'   , 1         , NULL         ),
    (@nomId , NULL  , N'Удостоверение за летателна годност (Наредба 8)'     , N'Airworthiness certificate (Directive 8)' , NULL           , 'directive8', 1         , NULL         ),
    (@nomId , NULL  , N'Удостоверение за летателна годност (СлВС)'          , N'Airworthiness certificate (VLA)'         , NULL           , 'vla'       , 1         , NULL         )
GO
