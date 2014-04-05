PRINT 'aircraftRegStatuses'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (72,'Статут на регистрация','aircraftRegStatuses');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8173,72,N'S1',N'Статус 1',N'Status 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8174,72,N'S2',N'Статус 2',N'Status 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
