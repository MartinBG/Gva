PRINT 'registers'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (67,'Регистър','registers');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8163,67,N'R1',N'Регистър 1',N'Register 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8164,67,N'R2',N'Регистър 2',N'Register 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
