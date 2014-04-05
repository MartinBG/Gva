PRINT 'aircraftPartStatuses'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (56,'Статут на част','aircraftPartStatuses');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8141,56,N'S1',N'Нов',N'New',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8142,56,N'S2',N'Изпозлван',N'Used',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
