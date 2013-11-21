PRINT 'personRatingLevels'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (38,N'Степени на квалификационен клас на Физическо лице',N'personRatingLevels');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7895,38,N'A',N'За извършване и удостоверяване на общо техническо обслужване, поддръжка и експлоатация на техническите средства за УВД',N'Level A',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7896,38,N'B',N'За извършване и удостоверяване на техническо обслужване, поддръжка, експлоатация и ремонт на техническите средства за УВД',N'Level B',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7897,38,N'C',N'За извършване и удостоверяване на техническо обслужване, поддръжка, експлоатация, ремонт, монтаж, настройка, анализ и контрол на техническите средства за УВД',N'Level C',NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
