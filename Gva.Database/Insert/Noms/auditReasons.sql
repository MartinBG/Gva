PRINT 'auditReasons'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (62,'Причина за одит','auditReasons');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8153,62,N'R1',N'Причина 1',N'Reason 1',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8154,62,N'R2',N'Причина 2',N'Reason 2',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
