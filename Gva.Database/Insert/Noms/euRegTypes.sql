PRINT 'euRegTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (52,'EASA Reg','euRegTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8122,52,N'EU',N'EU',N'EU',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8123,52,N'EUR',N'EU - Restricted',N'EU - Restricted',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8124,52,N'OD',N'Old Doc',N'Old Doc',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8125,52,N'AII',N'Annex II',N'Annex II',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8126,52,N'A2',N'Article-2',N'Article-2',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8127,52,N'A1',N'Article-1',N'Article-1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8128,52,N'VLA',N'VLA',N'VLA',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8129,52,N'AM',N'Amateur',N'Amateur',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8130,52,N'EXP',N'EXP',N'EXP',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
