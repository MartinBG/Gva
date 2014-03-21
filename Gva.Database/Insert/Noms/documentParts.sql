PRINT 'documentParts'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (46,N'Типове документи',N'documentParts');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8063,46,N'documentId',N'Документ за самоличност',NULL,NULL,N'documentId',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8064,46,N'education',N'Образования',NULL,NULL,N'education',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8065,46,N'employment',N'Месторабота',NULL,NULL,N'employment',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8066,46,N'medical',N'Медицински',NULL,NULL,N'medical',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8067,46,N'check',N'Проверка',NULL,NULL,N'check',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8068,46,N'training',N'Обучение',NULL,NULL,N'training',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8069,46,N'other',N'Друг документ',NULL,NULL,N'other',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8070,46,N'application',N'Заявление',NULL,NULL,N'application',1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
