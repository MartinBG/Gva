PRINT 'ownerTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (77772,'ФЛ/ЮЛ','ownerTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777775,77772,N'P',N'ФЛ',N'Person',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777776,77772,N'O',N'ЮЛ',N'Organization',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
