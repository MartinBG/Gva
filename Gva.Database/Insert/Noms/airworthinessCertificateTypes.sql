PRINT 'airworthinessCertificateTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (77770,'AC cert type','airworthinessCertificateTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777770,77770,N'AC',N'Удостоверение за летателна годност (F25)',N'Airworthiness certificate (F25)',NULL,'f25',1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777771,77770,N'AR',N'Ограничено удостоверение за летателна годност (F24)',N'Limited airworthiness certificate (F24)',NULL,'f24',1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777772,77770,N'AS',N'Специално удостоверение за летателна годност',N'Special airworthiness certificate',NULL,'special',1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777990,77770,N'A9',N'Удостоверение за летателна годност (Наредба 8)',N'Airworthiness certificate (Directive 8)','directive8','',1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7777991,77770,N'AU',N'Удостоверение за летателна годност (СлВС)',N'Airworthiness certificate (ULAC)','ulac','',1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
