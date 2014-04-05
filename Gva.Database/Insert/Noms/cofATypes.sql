PRINT 'cofATypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (49,'CofA','cofATypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8099,49,N'EA25',N'EASA 25',N'EASA 25',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8100,49,N'EA24',N'EASA 24',N'EASA 24',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8101,49,N'BGF',N'BG Form',N'BG Form',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8102,49,N'TC',N'Tech Cert',N'Tech Cert',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8103,49,N'EXP',N'EXP',N'EXP',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
