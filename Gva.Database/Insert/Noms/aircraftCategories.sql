PRINT 'aircraftCategories'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (47,'Типове ВС','aircraftCategories');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8071,47,N'LA',N'Големи ВС',N'Large aircraft',NULL,NULL,1,N'{"description":"Aeroplanes with a maximum take-off mass of more than 5700 kg, requiring type training and individual type rating."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8072,47,N'A-tr',N'ВС до 5700кг и по малко',N'Aeroplanes of 5700kg and below',NULL,NULL,1,N'{"description":"Requiring type training and individual type rating."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8073,47,N'AMTE',N'Самолети multiple turbine engines of 5700kg and below',N'Aeroplanes multiple turbine engines of 5700kg and below',NULL,NULL,1,N'{"description":"Eligible for type examinations and manufacturer group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8074,47,N'ASTE',N'Самолети single turbine engine of 5700kg and below',N'Aeroplanes single turbine engine of 5700kg and below',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8075,47,N'AMPE-MS',N'Самолети multiple piston engines – metal structure (AMPE-MS)',N'Aeroplane multiple piston engines – metal structure (AMPE-MS)',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8076,47,N'ASPE-MS',N'Самолети single piston engine – metal structure (ASPE-MS)',N'Aeroplane single piston engine – metal structure (ASPE-MS)',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8077,47,N'AMPE-WS',N'Самолети multiple piston engines – wooden structure',N'Aeroplane multiple piston engines – wooden structure',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8078,47,N'ASPE-WS',N'Самолети single piston engine – wooden structure.',N'Aeroplane single piston engine – wooden structure.',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8079,47,N'AMPE-CS',N'Самолети multiple piston engines – composite structure ',N'Aeroplane multiple piston engines – composite structure ',NULL,NULL,1,N'{"description":"Eeligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8080,47,N'ASPE-CS',N'Самолети single piston engine – composite structure',N'Aeroplane single piston engine – composite structure',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8082,47,N'MEH',N'Multi-engine хеликоптери ',N'Multi-engine helicopters ',NULL,NULL,1,N'{"description":"Requiring type training and individual type rating."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8083,47,N'HSTE',N'Хеликоптери  – Single turbine engine',N'Helicopters – Single turbine engine',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8084,47,N'HSPE',N'Хеликоптери – Single piston engines',N'Helicopters – Single piston engines',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8085,47,N'AII',N'ВС по Приложение II на Основния регламент',N'A/C acc. to Annex II to the Basic Regulation',NULL,NULL,1,N'{"description":"ВС по Приложение II на Основния регламент - исторически, аматьорски построени, експериментални, бивши военни."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(8086,47,N'B3',N'Piston-engine non-pressurised aeroplanes of 2 000 kg MTOM and below',N'Piston-engine non-pressurised aeroplanes of 2 000 kg MTOM and below',NULL,NULL,1,N'{"description":"Piston-engine non-pressurised aeroplanes of 2 000 kg MTOM and below"}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
