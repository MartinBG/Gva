PRINT 'staffTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (5,N'Типове персонал',N'staffTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5589,5,N'M',N'Наземен авиационен персонал за TO на СУВД',N'Наземен авиационен персонал за TO на СУВД',NULL,NULL,1,N'{"codeCA":null}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5590,5,N'G',N'Наземен авиационен персонал за TO на ВС',N'Наземен авиационен персонал за TO',NULL,NULL,1,N'{"codeCA":null}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5591,5,N'F',N'Членове на екипажа',N'Членове на екипажа',NULL,NULL,1,N'{"codeCA":null}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(5592,5,N'T',N'Наземен авиационен персонал за ОВД',N'Наземен авиационен персонал за ОВД',NULL,NULL,1,N'{"codeCA":null}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
