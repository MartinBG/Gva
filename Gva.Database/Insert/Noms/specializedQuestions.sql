PRINT 'specializedQuestions'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (89902,'Статут на част','specializedQuestions');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8999004,89902,N'S1',N'2.1',N'2.1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8999005,89902,N'S2',N'2.2',N'2.2',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8999006,89902,N'S2',N'2.3',N'2.3',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
