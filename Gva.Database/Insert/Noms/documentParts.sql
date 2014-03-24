PRINT 'documentParts'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (46,N'Типове документи',N'documentParts');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8063,46,N'DocumentId',N'Документ за самоличност',NULL,NULL,N'DocumentId',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8064,46,N'DocumentEducation',N'Образования',NULL,NULL,N'DocumentEducation',1,NULL);

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8065,46,N'DocumentEducation',N'Месторабота',NULL,NULL,N'DocumentEmployment',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8066,46,N'DocumentEducation',N'Медицински',NULL,NULL,N'DocumentMed',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8067,46,N'DocumentEducation',N'Проверка',NULL,NULL,N'DocumentCheck',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8068,46,N'DocumentEducation',N'Обучение',NULL,NULL,N'DocumentTraining',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8069,46,N'DocumentEducation',N'Друг документ',NULL,NULL,N'DocumentOther',1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8070,46,N'DocumentEducation',N'Заявление',NULL,NULL,N'DocumentApplication',1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
