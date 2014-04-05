PRINT 'aircraftRelations'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (53,'Тип отношение','aircraftRelations');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8131,53,N'OW',N'Собственик',N'Owner',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8132,53,N'OP',N'Оператор',N'Operator',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8133,53,N'TN',N'Наемател',N'Loanee',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8134,53,N'LS',N'Лизингодател',N'Leaser',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8135,53,N'CAMO',N'Организация за УППЛГ',N'CAMO',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8136,53,N'145',N'Организация за ТО',N'145',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
