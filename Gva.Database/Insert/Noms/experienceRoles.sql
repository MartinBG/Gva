PRINT 'experienceRoles'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (35,N'Роли в натрупан летателният опит',N'experienceRoles');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7837,35,N'IE',N'Инструктор',N'Инструктор',NULL,NULL,1,N'{"codeCA":null}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7838,35,N'EI',N'Обучаем (с инструктор)',N'Обучаем (с инструктор)',NULL,NULL,1,N'{"codeCA":null}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7839,35,N'CI',N'Под наблюдение на инструктор',N'Под наблюдение на инструктор',NULL,NULL,1,N'{"codeCA":null}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7840,35,N'IN',N'Самостоятелен',N'Самостоятелен',NULL,NULL,1,N'{"codeCA":null}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
