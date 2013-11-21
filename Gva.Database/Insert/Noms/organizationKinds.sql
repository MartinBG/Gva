PRINT 'organizationKinds'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (41,N'Видове организации',N'organizationKinds');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7941,41,N'0',N'Няма избрана',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7942,41,N'11',N'АО - търговска дейност',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7943,41,N'12',N'АО - специализирани авиационни работи',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7944,41,N'13',N'АО - друга въздухоплавателна дейност АХР',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7945,41,N'14',N'Други (несертифицирани)',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7946,41,N'15',N'В процес на сертифициране',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7947,41,N'16',N'Само собственици - Не АО (или бивши)',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7948,41,N'21',N'Учебни центрове (извън състава на АО)',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7949,41,N'22',N'Авиотехнически бази (извън състава на АО)',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7950,41,N'31',N'Фирми - ЕТ по смисъла на ТЗ',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7951,41,N'32',N'Частни лица',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7952,41,N'33',N'ЕАД',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7953,41,N'41',N'МТ -летища, РВД и др.',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7954,41,N'42',N'Държавни организации',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7955,41,N'43',N'Банкови, финансови, застрахователни',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7956,41,N'81',N'Архивни записи (променени адреси) !!!!',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7957,41,N'91',N'Архивни записи (отпаднали организации)',NULL,NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
