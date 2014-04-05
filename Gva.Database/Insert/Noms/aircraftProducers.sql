PRINT 'aircraftProducers'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (48,'Производители на ВС','aircraftProducers');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8087,48,N'NONNE',N'няма избран производител',N'no producer selected',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8088,48,N'DAR',N'ДАР - самолети ООД',N'DAR - Airplains Ltd.',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8089,48,N'TLU',N'TL-Ultralight',N'TL-Ultralight',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8090,48,N'EPACA',N'EPA Costruzioni Aereonautiche',N'EPA Costruzioni Aereonautiche',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8091,48,N'AD-NA',N'ЕТ “Авио Делта - Никола Арнаудов”',N'ЕТ “Avio Delta - Nicola Arnaudov”',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8092,48,N'Cos',N'Cosmos',N'Cosmos',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8093,48,N'TECNAM',N'TECNAM Costruzioni Aeronautiche',N'TECNAM Costruzioni Aeronautiche',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8094,48,N'RI',N'Rans Incorporation',N'Rans Incorporation',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8095,48,N'Ost',N'Ost - West Consulting Ltd',N'Ost - West Consulting Ltd',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8096,48,N'JAP',N'Jabiru Aircraft Pty. Ltd.',N'Jabiru Aircraft Pty. Ltd.',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8097,48,N'AD-L',N'Авио Делта ООД',N'Avio Delta Ltd.',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8098,48,N'FD',N'Flight Design GmbH',N'Flight Design GmbH',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
