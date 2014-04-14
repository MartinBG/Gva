PRINT 'aircraftModels'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (9901,'Модели ВС','aircraftModels');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(999001,9901,N'M1',N'Aztec E',N'Aztec E',NULL,NULL,1,N'{"categoryId":"108332", "producerId":"108334"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(999002,9901,N'M2',N'Piper PA 23-250',N'Piper PA 23-250',NULL,NULL,1,N'{"categoryId":"108331", "producerId":"108334"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(999003,9901,N'M3',N'Beech Super King Air-200',N'Beech super king air-200',NULL,NULL,1,N'{"categoryId":"108331", "producerId":"100022"}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
