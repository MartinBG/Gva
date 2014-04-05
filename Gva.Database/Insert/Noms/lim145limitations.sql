PRINT 'lim145limitations'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (60,'Дейност по част M/F, 145','lim145limitations');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8149,60,N'L1',N'Ограничение 1',N'Limitation 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8150,60,N'L2',N'Ограничение 2',N'Limitation 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
