PRINT 'personIdDocumentTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (12,N'Типове документи за самоличност на Физическо лице',N'personIdDocumentTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6442,12,N'3',N'Лична карта',N'Лична карта',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6443,12,N'4',N'Задграничен паспорт',N'Задграничен паспорт',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6444,12,N'5',N'Паспорт',N'Паспорт',NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
