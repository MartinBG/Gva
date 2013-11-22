PRINT 'licenceTypeDictionary'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (24,N'Легенда към видове(типове) правоспособност',N'licenceTypeDictionary');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7328,24,N'ATCL-ENDOR',N'Ръководител полети - разрешения',NULL,5592,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7329,24,N'ATCL-ADDIT',N'Ръководител полети - Допълнителни разрешения',NULL,5592,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7330,24,N'ATCL-RAT',N'Ръководител полети - квалификации',NULL,5592,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7331,24,N'SATCL',N'Ученик Ръководител полети',NULL,5592,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7332,24,N'CATML',N'Координатор по УВД',NULL,5592,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7333,24,N'Student',N'Обучаем пилот',NULL,5591,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7334,24,N'PILOT',N'Пилот',NULL,5591,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7335,24,N'STEWARD',N'Стюардеса',NULL,5591,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
