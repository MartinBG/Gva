INSERT INTO Noms (NomId, Name, Alias) VALUES (101,N'Номенклатура Оценка от писмен изпити',N'testScores');
GO
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(101,N'Издържал',N'Издържал',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(101,N'Неиздържал',N'Неиздържал',NULL,NULL,NULL,1,NULL);
GO
