PRINT 'aircraftNewOld'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (68,'Ново ВС','aircraftNewOld');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8165,68,N'N',N'Нов',N'New',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8166,68,N'U',N'Ползван',N'Used',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
