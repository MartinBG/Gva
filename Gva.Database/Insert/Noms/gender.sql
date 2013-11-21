PRINT 'gender'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (1,N'Полове',N'gender');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(1,1,N'M',N'Мъж',N'Male',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(2,1,N'W',N'Жена',N'Female',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(3,1,N'U',N'Неизвестен',N'Unknown',NULL,NULL,1,NULL);

GO

SET IDENTITY_INSERT [NomValues] OFF
GO
