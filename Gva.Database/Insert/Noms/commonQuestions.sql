PRINT 'commonQuestions'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (89901,'Статут на част','commonQuestions');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8999001,89901,N'S1',N'1.1',N'1.1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8999002,89901,N'S2',N'1.2',N'1.2',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8999003,89901,N'S2',N'1.3',N'1.3',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
