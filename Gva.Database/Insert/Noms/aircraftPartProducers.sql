PRINT 'aircraftPartProducers'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (55,'Производител на част','aircraftPartProducers');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8139,55,N'PR1',N'Producer 1',N'Producer 1',8138,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8140,55,N'PR2',N'Producer 2',N'Producer 2',8138,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
