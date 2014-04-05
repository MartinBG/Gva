PRINT 'aircraftCertificateTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (66,'Вид удостоверение','aircraftCertificateTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8161,66,N'T1',N'Вид 1',N'Type 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8162,66,N'T2',N'Вид 2',N'Type 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
