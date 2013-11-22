PRINT 'graduations'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (7,N'Степени на образование',N'graduations');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5629,7,N'HM',N'Висше образование (магистър)',N'Висше образование (магистър)',NULL,NULL,1,N'{"rating":81}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5630,7,N'PQ',N'Професионална квалификация',N'Професионална квалификация',NULL,NULL,1,N'{"rating":0}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5631,7,N'ESS',N'Средно специално образование',N'Средно специално образование',NULL,NULL,1,N'{"rating":51}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5632,7,N'HS',N'Висше образование (бакалавър)',N'Висше образование (бакалавър)',NULL,NULL,1,N'{"rating":80}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5633,7,N'ES',N'Средно образование',N'Средно образование',NULL,NULL,1,N'{"rating":50}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5634,7,N'HH',N'Полувисше (специалист)',N'Полувисше (специалист)',NULL,NULL,1,N'{"rating":70}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5635,7,N'PE',N'Основно образование',N'Primary Education',NULL,NULL,1,N'{"rating":1}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
