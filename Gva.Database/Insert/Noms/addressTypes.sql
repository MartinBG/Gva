PRINT 'addressTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (4,N'Типове адреси',N'addressTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5582,4,N'TO',N'Адрес за базово ослужване на ВС',N'Адрес за базово ослужване на ВС',NULL,NULL,1,N'{"type":"F"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5583,4,N'PER',N'Постоянен адрес',N'Постоянен адрес',NULL,NULL,1,N'{"type":"P"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5584,4,N'TMP',N'Настоящ адрес',N'Настоящ адрес',NULL,NULL,1,N'{"type":"P"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5585,4,N'COR',N'Адрес за кореспонденция',N'Адрес за кореспонденция',NULL,NULL,1,N'{"type":"P"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5586,4,N'O',N'Седалище',N'Седалище',NULL,NULL,1,N'{"type":"F"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5587,4,N'TOP',N'Данни за ръководител',N'Данни за ръководител',NULL,NULL,1,N'{"type":"F"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5588,4,N'BOS',N'Данни за ръководител TO',N'Данни за ръководител TO',NULL,NULL,1,N'{"type":"F"}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
