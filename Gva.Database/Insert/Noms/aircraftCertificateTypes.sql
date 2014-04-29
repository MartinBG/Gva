PRINT 'aircraftCertificateTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (77751,'AC cert type','aircraftCertificateTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778117,77751,N'FS',N'Удостоверение за регистрация (стандартно)',N'Balloon',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778118,77751,N'FR',N'Ограничено удостоверение за регистрация',N'Commuter',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778119,77751,N'AC',N'Удостоверение за летателна годност (F25)',N'Experimental',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778120,77751,N'AO',N'Удостоверение за ЛГ (старо)',N'Glider',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778121,77751,N'AR',N'Ограничено удостоверение за летателна годност (F24)',N'Gyroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778122,77751,N'AT',N'Техническо свидетелство',N'Large Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778123,77751,N'AS',N'Специално удостоверение за летателна годност',N'Motor-hanglider',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778124,77751,N'AV',N'Удостоверение за преглед за ЛГ (15a)',N'Paramotor-Trike',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778125,77751,N'AE',N'Експортно удостоверение за летателна годност',N'Rotorcraft',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778126,77751,N'PF',N'Разрешение за полет',N'Small Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778127,77751,N'NC',N'Удостоверение за авиационен шум',N'Small Rotorcraft',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778128,77751,N'FO',N'Анулиране на регистрация',N'Very Light Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778129,77751,N'AW',N'Удостоверение за преглед за ЛГ (15b)',N'Very Light Rotorcraft',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
