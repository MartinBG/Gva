PRINT 'examiners'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (73,'Одитор','examiners');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8175,73,N'IGP',N'Иван Георгиев Петров',N'Ivan Georgiev Petrov',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8176,73,N'TTT',N'Тодор Тодоров Тодорски',N'Todor Todorov Todorsky',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
