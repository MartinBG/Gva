PRINT 'aircraftParts'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (54,'Тип част','aircraftParts');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8137,54,N'EN',N'Двигател',N'Engine',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8138,54,N'Pr',N'Витло',N'Propeller',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
