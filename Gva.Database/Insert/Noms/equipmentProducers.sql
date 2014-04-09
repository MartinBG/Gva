PRINT 'equipmentProducers'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (99,'ТПроизводител','equipmentProducers');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(99998,99,N'P1',N'Производител 1',N'Producer 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(99999,99,N'P2',N'Производител 2',N'Producer 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
