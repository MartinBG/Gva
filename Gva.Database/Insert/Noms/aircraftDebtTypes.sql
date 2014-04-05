PRINT 'aircraftDebtTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (57,'Залог/запор','aircraftDebtTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8143,57,N'P',N'Залог',N'Pledge',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8144,57,N'D',N'Запор',N'Distraint',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
