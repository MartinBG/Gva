PRINT 'yesNoOptions'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (45,N'Да/Не номенклатура',N'boolean');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8061,45,N'Y',N'Да',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8062,45,N'N',N'Не',NULL,NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
