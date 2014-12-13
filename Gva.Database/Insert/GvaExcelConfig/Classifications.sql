print 'Excel Insert Classifications'
GO

SET IDENTITY_INSERT [Classifications] ON
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(1,N'Всички документи',N'',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(2,N'Услуги',N'',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(3,N'Заявления',N'',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(4,N'Физически лица',N'',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(5,N'Общи',N'person',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(6,N'Екипажи',N'flightCrew',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(7,N'ОВД',N'ovd',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(8,N'ТО(AML)',N'to_vs',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(9,N'ТО(СУВД)',N'to_suvd',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(10,N'ПКС',N'security',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(11,N'Проверяващ лица',N'staffExaminer',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(12,N'ВС',N'aircraft',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(13,N'ЛЛП',N'airport',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(14,N'ССНО',N'equipment',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(15,N'Организации',N'',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(16,N'Общи',N'org',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(17,N'ОО',N'approvedOrg',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(18,N'ЛО',N'airportOperator',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(19,N'АУЦ',N'educationOrg',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(20,N'ВП',N'airCarrier',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(21,N'САО',N'airOperator',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(22,N'СИАНО',N'airNavSvcProvider',1);
INSERT INTO [Classifications]([ClassificationId],[Name],[Alias],[IsActive])VALUES(23,N'НСВНК и АСУВД',N'groundSvcOperator',1);
SET IDENTITY_INSERT [Classifications] OFF
GO

