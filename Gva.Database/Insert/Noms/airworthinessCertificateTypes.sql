PRINT 'airworthinessCertificateTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (77770,'AC cert type','airworthinessCertificateTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777770,77770,N'AC',N'Удостоверение за летателна годност (F25)',N'Experimental',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777771,77770,N'AR',N'Ограничено удостоверение за летателна годност (F24)',N'Gyroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777772,77770,N'AS',N'Специално удостоверение за летателна годност',N'Motor-hanglider',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
