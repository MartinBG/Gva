PRINT 'organizationTypes'
GO

INSERT INTO Noms (NomId, Name, Alias) VALUES (40,N'Класификатор на организации',N'organizationTypes');
GO

SET IDENTITY_INSERT [NomValues] ON
GO

INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7919,40,N'ET',N'ET - БГ собственици - ЕТ, АД, ООД, ЕООД..',N'ET - BG Owners type ET, AD, OOD, EOOD..',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7920,40,N'LAP',N'LAP - Чуждестранни авиокомпании - ЛАП',N'LAP - Foreign Operators - FCL',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7921,40,N'OLF',N'OLF - Неактивни чуждестранни собственици',N'OLF - Old Foreign Owners',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7922,40,N'EF',N'EF - Чуждестранни собственици',N'EF - Foreign Owners',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7923,40,N'N/A',N'Неизвестно',N'Not applicable',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7924,40,N'AC',N'AC - Оператори -  търговска дейност',N'AC - Commercial Aviation Operators',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7925,40,N'AR',N'AR - Друга въздухоплавателна дейност (АХР)',N'AR - Other Aerial Works',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7926,40,N'AS',N'AS - Специализирани авиационни работи',N'AS - Specialized Aerial Works',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7927,40,N'AT',N'AT - Летателно обучение ( FTO )',N'AT - Flight Training Organisation ( FTO )',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7928,40,N'AX',N'AX - Организации в процес на одобрение',N'AX - Organisation on Approval Process',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7929,40,N'AY',N'AY - Временно спряни организации',N'AY - Temporarily Revoked Organisation',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7930,40,N'AZ',N'AZ - Други авиационни организации (некласирани)',N'AZ - Non Classified Aviation Organisation',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7931,40,N'CF',N'CF - Самостоятелни организации по Част-MF',N'CF - Part-MF Separate Organisations',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7932,40,N'CG',N'CG - Самостоятелни организации по Част-MG',N'CG - Part-MG Separate Organisations',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7933,40,N'CM',N'CM - Самостоятелни организации по Част-145',N'CM - Part-145 Separate Organisations',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7934,40,N'CT',N'CT - Самостоятелни организации по Част-147',N'CT - Part-147 Separate Organisations',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7935,40,N'EP',N'EP - Частни лица - собственици',N'EP - Private Persons - Owners',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7936,40,N'FN',N'FN - Други собственици, кредитори',N'FN - Other Owners, Mortgagee',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7937,40,N'GV',N'GV - Правителствени - министерства, агенции, летища',N'GV - Government Offices, Agencies, Airports',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7938,40,N'INT',N'Чуждестранни - собственици, лизингодатели',N'Чуждестранни - собственици, лизингодатели',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7939,40,N'OLD',N'OLD - Явно стари или грешни записи (без активни ВС)',N'OLD - Old (without aircrafts) or mistakes records ',NULL,NULL,1,NULL);
INSERT INTO NomValues (NomValueId,NomId,Code,Name,NameAlt,ParentValueId,Alias,IsActive,TextContent) VALUES(7940,40,N'AAZ',N'Отпаднали организации - AO, OTO, УЦ, летища',N'Inactive Organizations - AOC, MO, TC, airports',NULL,NULL,1,NULL);
GO

SET IDENTITY_INSERT [NomValues] OFF
GO
