PRINT 'easaTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (77750,'EASA Types','easaTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778104,77750,N'B',N'Balloon',N'Balloon',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778105,77750,N'C',N'Commuter',N'Commuter',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778106,77750,N'E',N'Experimental',N'Experimental',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778107,77750,N'GL',N'Glider',N'Glider',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778108,77750,N'GY',N'Gyroplane',N'Gyroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778109,77750,N'LA',N'Large Aeroplane',N'Large Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778110,77750,N'MH',N'Motor-hanglider',N'Motor-hanglider',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778111,77750,N'PT',N'Paramotor-Trike',N'Paramotor-Trike',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778112,77750,N'RC',N'Rotorcraft',N'Rotorcraft',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778113,77750,N'SA',N'Small Aeroplane',N'Small Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778114,77750,N'SRC',N'Small Rotorcraft',N'Small Rotorcraft',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778115,77750,N'VLA',N'Very Light Aeroplane',N'Very Light Aeroplane',NULL,NULL,1);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive) VALUES(7778116,77750,N'VLRC',N'Very Light Rotorcraft',N'Very Light Rotorcraft',NULL,NULL,1);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
