PRINT 'aircraftOccurrenceClasses'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (61,'Клас на инцидент','aircraftOccurrenceClasses');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8151,61,N'C1',N'Клас 1',N'Class 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8152,61,N'C2',N'Клас 2',N'Class 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
