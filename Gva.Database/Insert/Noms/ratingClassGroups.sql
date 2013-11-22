PRINT 'ratingClassGroups'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (19,N'Групи Класове ВС за екипажи',N'ratingClassGroups');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6984,19,N'G',N'ТО на ВС',NULL,5590,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6985,19,N'T',N'ОВД',NULL,5592,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6986,19,N'F',N'Екипажи',NULL,5591,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6987,19,N'M',N'ТО на СУВД',NULL,5589,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
