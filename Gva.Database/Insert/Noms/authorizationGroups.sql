PRINT 'authorizationGroups'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (22,N'Групи Разрешения към квалификация',N'authorizationGroups');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7045,22,N'FC',N'Проверяващи',NULL,5591,NULL,1,N'{}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7046,22,N'T',N'За ОВД',NULL,5592,NULL,1,N'{"ratingClassGroupId":6985}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7047,22,N'G',N'За ТО (AML)',NULL,5590,NULL,1,N'{"ratingClassGroupId":6984}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7048,22,N'F',N'За екипаж на ВС',NULL,5591,NULL,1,N'{}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7049,22,N'FT',N'За провеждане обучение',NULL,5591,NULL,1,N'{}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7050,22,N'M',N'За ТО (СУВД)',NULL,5589,NULL,1,N'{"ratingClassGroupId":6987}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
