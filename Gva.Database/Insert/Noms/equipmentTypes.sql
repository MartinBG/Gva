PRINT 'equipmentTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (98,'Тип съоръжение','equipmentTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(99996,98,N'T1',N'Тип 1',N'Type 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(99997,98,N'T2',N'Тип 2',N'Type 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
