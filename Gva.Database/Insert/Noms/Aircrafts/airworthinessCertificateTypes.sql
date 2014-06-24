﻿GO
INSERT INTO Noms (Name, Alias) VALUES ('AC cert type','airworthinessCertificateTypes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]                                                , [NameAlt]                                  , [ParentValueId], [Alias]     , [IsActive], [TextContent])
VALUES
    (@nomId , N'AC' , N'Удостоверение за летателна годност (F25)'           , N'Airworthiness certificate (F25)'         , NULL           , 'f25'       , 1         , NULL         ),
    (@nomId , N'AR' , N'Ограничено удостоверение за летателна годност (F24)', N'Limited airworthiness certificate (F24)' , NULL           , 'f24'       , 1         , NULL         ),
    (@nomId , N'AS' , N'Специално удостоверение за летателна годност'       , N'Special airworthiness certificate'       , NULL           , 'special'   , 1         , NULL         ),
    (@nomId , N'A9' , N'Удостоверение за летателна годност (Наредба 8)'     , N'Airworthiness certificate (Directive 8)' , NULL           , 'directive8', 1         , NULL         ),
    (@nomId , N'AU' , N'Удостоверение за летателна годност (СлВС)'          , N'Airworthiness certificate (ULAC)'        , NULL           , 'ulac'      , 1         , NULL         )
GO