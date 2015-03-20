PRINT 'registers'
GO

SET IDENTITY_INSERT [Noms] ON

INSERT INTO Noms (NomId, Name, Alias) VALUES (94,'Регистър','registers');

SET IDENTITY_INSERT [Noms] OFF
GO


INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(94,N'R1',N'Регистър 1',N'Register 1',NULL,NULL,1);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(94,N'R2',N'Регистър 2',N'Register 2',NULL,NULL,1);
GO
