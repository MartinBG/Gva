PRINT 'aircraftCreditors'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (58,'Кредитор','aircraftCreditors');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8145,58,N'IGP',N'Иван Георгиев Петров',N'Ivan Georgiev Petrov',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8146,58,N'TTT',N'Тодор Тодоров Тодорски',N'Todor Todorov Todorsky',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
