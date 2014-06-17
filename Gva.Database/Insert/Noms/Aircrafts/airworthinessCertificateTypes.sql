GO
INSERT INTO Noms (Name, Alias) VALUES ('AC cert type','airworthinessCertificateTypes')

DECLARE @nomId INT = @@IDENTITY

INSERT INTO [NomValues]
    ([NomId], [Code], [Name]                                                , [NameAlt]         , [ParentValueId], [Alias], [IsActive], [TextContent])
VALUES
    (@nomId , N'AC' , N'Удостоверение за летателна годност (F25)'           , N'Experimental'   , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'AR' , N'Ограничено удостоверение за летателна годност (F24)', N'Gyroplane'      , NULL           , NULL   , 1         , NULL         ),
    (@nomId , N'AS' , N'Специално удостоверение за летателна годност'       , N'Motor-hanglider', NULL           , NULL   , 1         , NULL         )
GO
