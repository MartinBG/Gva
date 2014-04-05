PRINT 'aircraftLimitations'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (71,'Ограничение','aircraftLimitations');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8171,71,N'L1',N'Ограничение 1',N'Limitation 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8172,71,N'L2',N'Ограничение 2',N'Limitation 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
