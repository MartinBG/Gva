PRINT 'easaCategories'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (51,'EASA Категории','easaCategories');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8117,51,N'AW',N'Aerial Work',N'Aerial Work',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8118,51,N'COM',N'Commercial',N'Commercial',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8119,51,N'COR',N'Corporate',N'Corporate',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8120,51,N'PR',N'Private',N'Private',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8121,51,N'VLA',N'VLA',N'VLA',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
