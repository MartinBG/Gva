PRINT 'personCheckDocumentRoles'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (13,N'Роли документи за проверка на Физическо лице',N'personCheckDocumentRoles');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6445,13,N'49A',N'Проверка на работното място',N'Проверка на работното място',NULL,NULL,1,N'{"direction":6177,"isPersonsOnly":"N","categoryCode":"T"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6446,13,N'15',N'Практическа проверка',N'Практическа проверка',NULL,NULL,1,N'{"isPersonsOnly":"N","categoryCode":"T"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6447,13,N'1',N'Летателна проверка',N'Летателна проверка',NULL,NULL,1,N'{"direction":6173,"isPersonsOnly":"N","categoryCode":"T"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6448,13,N'7',N'Тренажор',N'Тренажор',NULL,NULL,1,N'{"isPersonsOnly":"N","categoryCode":"T"}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
