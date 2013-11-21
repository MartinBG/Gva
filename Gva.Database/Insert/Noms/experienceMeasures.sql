PRINT 'experienceMeasures'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (36,N'Видове летателен опит',N'experienceMeasures');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7841,36,N'W',N'Отработени часове',N'Отработени часове',NULL,NULL,1,N'{"codeCA":null}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7842,36,N'H',N'Летателни часове',N'Летателни часове',NULL,NULL,1,N'{"codeCA":null}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7843,36,N'F',N'Брой полети',N'Брой полети',NULL,NULL,1,N'{"codeCA":null}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
