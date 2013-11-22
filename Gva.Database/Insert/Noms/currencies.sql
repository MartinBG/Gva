PRINT 'currencies'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (44,N'Парични единици',N'currencies');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8058,44,N'BGL',N'Български лев',N'Български лев',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8059,44,N'BGN',N'Нов български лев',N'Нов български лев',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8060,44,N'EUR',N'Евро',N'Евро',NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
