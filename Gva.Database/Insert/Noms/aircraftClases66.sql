PRINT 'aircraftClases66'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (31,N'Клас (Part-66 Category)',N'aircraftClases66');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7617,31,N'A 1',N'Квалификационен клас А Удостоверяване на линейно ТО',NULL,7607,NULL,1,N'{"alias":"A"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7618,31,N'A 2',N'Квалификационен клас А Удостоверяване на линейно ТО',NULL,7608,NULL,1,N'{"alias":"A"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7619,31,N'A 3',N'Квалификационен клас А Удостоверяване на линейно ТО',NULL,7609,NULL,1,N'{"alias":"A"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7620,31,N'A 4',N'Квалификационен клас А Удостоверяване на линейно ТО',NULL,7610,NULL,1,N'{"alias":"A"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7621,31,N'B 1.1',N'Квалификационен клас B1 Удостоверяване на ТО (Самолет и системи)',NULL,7607,NULL,1,N'{"alias":"B 1"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7622,31,N'B 1.2',N'Квалификационен клас B1 Удостоверяване на ТО (Самолет и системи)',NULL,7608,NULL,1,N'{"alias":"B 1"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7623,31,N'B 1.3',N'Квалификационен клас B1 Удостоверяване на ТО (Самолет и системи)',NULL,7609,NULL,1,N'{"alias":"B 1"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7624,31,N'B 1.4',N'Квалификационен клас B1 Удостоверяване на ТО (Самолет и системи)',NULL,7610,NULL,1,N'{"alias":"B 1"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7625,31,N'B 2',N'Квалификационен клас B2 Удостоверяване на ТО (Авионикс)',NULL,7611,NULL,1,N'{"alias":"B 2"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7626,31,N'C',N'Квалификационен клас C Удостоверяване на базово ТО',NULL,7612,NULL,1,N'{"alias":"C"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7627,31,N'B 3',N'Квалификационен клас B3 Удостоверяване на ТО (Piston-engine non pressurised aeroplanes of 2 000 Kg MTOM and below)',NULL,7616,NULL,1,N'{"alias":"B 3"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7628,31,N'C 1',N'Квалификационен клас C Удостоверяване на ТО (Large Aircraft)',NULL,7614,NULL,1,N'{"alias":"C"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7629,31,N'C 2',N'Квалификационен клас C Удостоверяване на ТО (Aircraft other than large )',NULL,7615,NULL,1,N'{"alias":"C"}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
