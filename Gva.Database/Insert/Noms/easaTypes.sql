PRINT 'easaTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (50,'EASA Types','easaTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8104,50,N'B',N'Balloon',N'Balloon',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8105,50,N'C',N'Commuter',N'Commuter',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8106,50,N'E',N'Experimental',N'Experimental',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8107,50,N'GL',N'Glider',N'Glider',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8108,50,N'GY',N'Gyroplane',N'Gyroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8109,50,N'LA',N'Large Aeroplane',N'Large Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8110,50,N'MH',N'Motor-hanglider',N'Motor-hanglider',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8111,50,N'PT',N'Paramotor-Trike',N'Paramotor-Trike',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8112,50,N'RC',N'Rotorcraft',N'Rotorcraft',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8113,50,N'SA',N'Small Aeroplane',N'Small Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8114,50,N'SRC',N'Small Rotorcraft',N'Small Rotorcraft',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8115,50,N'VLA',N'Very Light Aeroplane',N'Very Light Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(8116,50,N'VLRC',N'Very Light Rotorcraft',N'Very Light Rotorcraft',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
