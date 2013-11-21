PRINT 'aircraftGroup66'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (30,N'Категория за АМЛ (Part-66 Category)',N'aircraftGroup66');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7607,30,N'1',N'Самолети с турбинни двигатели1',N'Aeroplanes Turbine',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7608,30,N'2',N'Самолети с бутални двигатели',N'Aeroplanes Piston',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7609,30,N'3',N'Хеликоптери с турбинни двигатели',N'Helicopters Turbine',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7610,30,N'4',N'Хеликоптери с бутални двигатели',N'Helicopters Piston',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7611,30,N'5',N'Авионикс',N'Avionics',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7612,30,N'6',N'Въздухоплавателно средство',N'Aircraft',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7613,30,N'7',N'Запазено',N'Reserved',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7614,30,N'9',N'Големи ВС',N'Large Aircraft',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7615,30,N'10',N'ВС различни от големи',N'Aircraft other than large',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7616,30,N'11',N'Самолети с бутални двигатели нехерметезирани с максимална излетна маса до 2000кг',N'Piston-engine non pressurised aeroplanes of 2 000 Kg MTOM and below',NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
