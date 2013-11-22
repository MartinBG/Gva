PRINT 'licenceActions'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (39,N'Видове действия относно правоспособност',N'licenceActions');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7898,39,N'18',N'Премахване на ограничения',N'Removal of limitations',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7899,39,N'19',N'Първоначално издаване',N'Initial AML',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7900,39,N'20',N'Добавяне на клас/подклас',N'Change of AML',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7901,39,N'21',N'Конвертиране от национален AML',N'Part-66.A.70 AML conversion',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7902,39,N'5',N'Замяна на старо СП',N'Замяна на старо СП',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7903,39,N'6',N'Вписване на тип ВС',N'Випсване на тип ВС',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7904,39,N'15',N'Подмяна на чуждо СП ',N'Подмяна на чуждо СП ',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7905,39,N'16',N'Преиздаване поради липса на място',N'Преиздаване поради липса на място',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7906,39,N'7',N'Преиздаване -изгубено,унищожено,откраднато',N'Преиздаване -изгубено,унищожено,откраднато',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7907,39,N'13',N'Вписване на квалификационен клас',N'Вписване на квалификационен клас',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7908,39,N'14',N'Потвърждаване на квалификационен клас',N'Потвърждаване на квалификационен клас',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7909,39,N'17',N'Преиздаване поради изтичане срока на валидност на СП',N'Преиздаване поради изтичане срока на валидност на СП',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7910,39,N'RE2',N'Преиздаване на свидетелство поради липса на място в параграф XII',N'Преиздаване на свидетелство поради липса на място в параграф XII',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7911,39,N'RE3',N'Преиздаване на свидетелство поради други административни причини',N'Преиздаване на свидетелство поради други административни причини',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7912,39,N'RE4',N'Вписване на разрешение',N'Вписване на разрешение',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7913,39,N'PUB',N'Издаване на свидетелство',N'Издаване на свидетелство',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7914,39,N'RE1',N'Преиздаване на свидетелство поради подновяване на квалификация',N'Преиздаване на свидетелство поради подновяване на квалификация',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7915,39,N'8',N'Възстановяване на тип ВС',N'Възстановяване на тип ВС',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7916,39,N'9',N'Възстановяване на кв.клас',N'Възстановяване на кв.клас',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7917,39,N'10',N'Възстановяване на разрешение',N'Възстановяване на разрешение',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7918,39,N'11',N'Преиздаване на СП поради повреда',N'Преиздаване на СП поради повреда',NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
