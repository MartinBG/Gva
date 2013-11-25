PRINT 'personStatusTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (15,N'Типове състояния на Физичеко лице',N'personStatusTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6588,15,N'BR',N'Брюксел',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6589,15,N'FD',N'Освободен',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6590,15,N'МA',N'Майчинство',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6591,15,N'PS',N'Пенсионер',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6592,15,N'TU',N'Временно негоден',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6593,15,N'UF',N'Негоден',NULL,NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
