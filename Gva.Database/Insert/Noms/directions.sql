PRINT 'directions'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (9,N'Направления',N'directions');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6173,9,N'F',N'Екипажи',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6174,9,N'GR_PERSON',N'ТО (AML)',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6175,9,N'ORGANIZATION',N'Организации',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6176,9,N'AIRCRAFT',N'ВС',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6177,9,N'T',N'ОВД',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6178,9,N'M',N'ТО (СУВД)',NULL,NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
