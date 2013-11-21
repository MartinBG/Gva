PRINT 'medDocPublishers'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (17,N'Издатели на документи - Медицински',N'medDocPublishers');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6826,17,NULL,N'КАМО',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6827,17,NULL,N'ROMANIAN CIVIL AERONAUTICAL AUTHORITY-AME No.02',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6828,17,NULL,N'Austro Control ',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6829,17,NULL,N'AME-SWETZERLAND',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6830,17,NULL,N'AMC PRAGUE',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6831,17,NULL,N'FR AMC',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6832,17,NULL,N'FAA',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6833,17,NULL,N'GCAA UAE',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6834,17,NULL,N'CAA France',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6835,17,NULL,N'GR AME',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6836,17,NULL,N'UK AME',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6837,17,NULL,N'АМЦ01',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6838,17,NULL,N'TR-AME-008/2',NULL,NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(6839,17,NULL,N'AMC Latvia',NULL,NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
