PRINT 'airworthinessReviewTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (77771,'AC cert type','airworthinessReviewTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777773,77771,N'AV',N'Удостоверение за преглед за ЛГ (15a)',N'Airworthiness review certificate (15a)',NULL,'15a',1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777774,77771,N'AW',N'Удостоверение за преглед за ЛГ (15b)',N'Airworthiness review certificate (15b)',NULL,'15b',1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
