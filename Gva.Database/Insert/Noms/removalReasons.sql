PRINT 'removalReasons'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (75,'Причина за дерегистрация','removalReasons');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8179,75,N'7',N'7 - Изтичане на срока на договора',N'7 - Contract date expired',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8180,75,N'8',N'8 - Смяна собственост',N'8 - Ownership change',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8181,75,N'9',N'9 - Брак',N'9 - Totaled',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8182,75,N'36',N'36 - Заповед',N'36 - Order',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
