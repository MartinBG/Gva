PRINT 'aircraftTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (28,N'Типове ВС',N'aircraftTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7445,28,N'LA',N'Големи ВС',N'Large aircraft',NULL,NULL,1,N'{"description":"Aeroplanes with a maximum take-off mass of more than 5700 kg, requiring type training and individual type rating."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7446,28,N'A-tr',N'ВС до 5700кг и по малко',N'Aeroplanes of 5700kg and below',NULL,NULL,1,N'{"description":"Requiring type training and individual type rating."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7447,28,N'AMTE',N'Самолети multiple turbine engines of 5700kg and below',N'Aeroplanes multiple turbine engines of 5700kg and below',NULL,NULL,1,N'{"description":"Eligible for type examinations and manufacturer group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7448,28,N'ASTE',N'Самолети single turbine engine of 5700kg and below',N'Aeroplanes single turbine engine of 5700kg and below',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7449,28,N'AMPE-MS',N'Самолети multiple piston engines – metal structure (AMPE-MS)',N'Aeroplane multiple piston engines – metal structure (AMPE-MS)',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7450,28,N'ASPE-MS',N'Самолети single piston engine – metal structure (ASPE-MS)',N'Aeroplane single piston engine – metal structure (ASPE-MS)',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7451,28,N'AMPE-WS',N'Самолети multiple piston engines – wooden structure',N'Aeroplane multiple piston engines – wooden structure',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7452,28,N'ASPE-WS',N'Самолети single piston engine – wooden structure.',N'Aeroplane single piston engine – wooden structure.',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7453,28,N'AMPE-CS',N'Самолети multiple piston engines – composite structure ',N'Aeroplane multiple piston engines – composite structure ',NULL,NULL,1,N'{"description":"Eeligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7454,28,N'ASPE-CS',N'Самолети single piston engine – composite structure',N'Aeroplane single piston engine – composite structure',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7455,28,N'MEH',N'Multi-engine хеликоптери ',N'Multi-engine helicopters ',NULL,NULL,1,N'{"description":"Requiring type training and individual type rating."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7456,28,N'HSTE',N'Хеликоптери  – Single turbine engine',N'Helicopters – Single turbine engine',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings"}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7457,28,N'HSPE',N'Хеликоптери – Single piston engines',N'Helicopters – Single piston engines',NULL,NULL,1,N'{"description":"Eligible for type examinations and group ratings."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7458,28,N'AII',N'ВС по Приложение II на Основния регламент',N'A/C acc. to Annex II to the Basic Regulation',NULL,NULL,1,N'{"description":"ВС по Приложение II на Основния регламент - исторически, аматьорски построени, експериментални, бивши военни."}');
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7459,28,N'B3',N'Piston-engine non-pressurised aeroplanes of 2 000 kg MTOM and below',N'Piston-engine non-pressurised aeroplanes of 2 000 kg MTOM and below',NULL,NULL,1,N'{"description":"Piston-engine non-pressurised aeroplanes of 2 000 kg MTOM and below"}');
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
