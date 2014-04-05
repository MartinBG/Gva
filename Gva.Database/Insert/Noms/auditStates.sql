PRINT 'auditStates'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (64,'Състояние на одит','auditStates');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8157,64,N'S1',N'Състояние 1',N'State 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8158,64,N'S2',N'Състояние 2',N'State 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
