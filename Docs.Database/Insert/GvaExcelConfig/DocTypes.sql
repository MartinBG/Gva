﻿print 'Excel Insert DocTypes'
GO

SET IDENTITY_INSERT [DocTypes] ON
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(1,1,NULL,NULL,N'Резолюция',N'',N'Resolution',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(2,1,NULL,NULL,N'Задача',N'',N'Task',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(3,1,NULL,NULL,N'Забележка',N'',N'Remark',0,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(4,100,NULL,NULL,N'Писмо',N'',N'',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(5,100,NULL,NULL,N'Приемно предавателен протокол',N'',N'',0,N'',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(6,300,NULL,NULL,N'Съобщение, че получаването не се потвърждава',N'',N'ReceiptNotAcknowledgedMessage',1,N'0010-000001',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(7,300,NULL,NULL,N'Потвърждаване за получаване',N'',N'ReceiptAcknowledgedMessage',1,N'0010-000002',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(8,300,NULL,NULL,N'Указания за отстраняване на нередовности',N'',N'RemovingIrregularitiesInstructions',1,N'0010-003010',N'',N'',NULL,NULL,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(9,400,1,1,N'Невалидна услуга',N'',N'InvalidService',1,N'',N'',N'',NULL,NULL,0);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(10,201,4,4,N'Заявление',N'',N'Request',1,N'',N'01',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(11,201,NULL,NULL,N'Допълване на заявление',N'',N'Request supplement',0,N'',N'02',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(12,201,4,4,N'Издаване на оперативен лиценз на въздушен превозвач',N'Заявление за издаване на оперативен лиценз на въздушен превозвач',N'R-4686',1,N'0010-004686',N'03',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(13,201,4,4,N'Издаване на свидетелство за авиационен оператор, извършващ търговски превози на пътници и товари',N'Заявление за издаване на свидетелство за авиационен оператор, извършващ търговски превози на пътници и товари',N'R-5090',1,N'0010-005090',N'04',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(14,201,4,4,N'Издаване на свидетелство за авиационен оператор, извършващ специализирани авиационни работи',N'Заявление за издаване на свидетелство за авиационен оператор, извършващ специализирани авиационни работи',N'R-5000',1,N'0010-005000',N'05',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(15,201,4,4,N'Издаване на свидетелство за професионално обучение на авиационен учебен център',N'Заявление за издаване на свидетелство за професионално обучение на авиационен учебен център',N'R-4834',1,N'0010-004834',N'06',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(16,201,4,4,N'Издаване на свидетелство за правоспособност на авиационен персонал – пилоти',N'Заявление за първоначално издаване на свидетелство за правоспособност на професионален пилот и правоспособност за полет по прибори',N'R-4186',1,N'0010-004186',N'07',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(17,201,4,4,N'Издаване на свидетелство за правоспособност на авиационен персонал – членове на летателния състав от екипажите на ВС, различни от пилоти',N'Заявление за издаване на свидетелство за правоспособност на авиационен персонал – членове на летателния състав от екипажите на ВС, различни от пилоти',N'R-4244',1,N'0010-004244',N'08',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(18,201,4,4,N'Издаване на свидетелство за правоспособност на членовете на кабинния екипаж',N'Заявление за издаване на свидетелство за правоспособност на членовете на кабинния екипаж',N'R-4864',1,N'0010-004864',N'09',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(19,201,4,4,N'Издаване на свидетелство за правоспособност на авиационен персонал – полетни диспечери',N'Заявление за издаване на свидетелство за правоспособност на авиационен персонал – полетни диспечери',N'R-4242',1,N'0010-004242',N'10',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(20,201,4,4,N'Издаване на свидетелство за правоспособност за техническо обслужване на самолети и хеликоптери',N'Заявление за първоначално издаване на лиценз за техническо обслужване на въздухоплавателни средства (AML) по Част 66',N'R-4240',1,N'0010-004240',N'11',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(21,201,4,4,N'Признаване на свидетелство за правоспособност на чужди граждани',N'Заявление за признаване на свидетелство за правоспособност на чужди граждани',N'R-4296',1,N'0010-004296',N'12',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(22,201,4,4,N'Издаване на удостоверение за одобрение за ползване на авиационен тренажор',N'Заявление за издаване на удостоверение за одобрение за ползване на авиационен тренажор',N'R-4900',1,N'0010-004900',N'13',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(23,201,4,4,N'Издаване на разрешително за проверяващ',N'',N'',0,N'',N'14',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(24,201,4,4,N'Издаване на свидетелство за одобрение на организация за обучение',N'Заявление за издаване на свидетелство за одобрение на организация за обучение',N'R-4860',1,N'0010-004860',N'15',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(25,201,4,4,N'Издаване на удостоверение за одобрение на организация за обучение, намираща се в друга държава',N'Заявление за издаване на удостоверение за одобрение на организация за обучение, намираща се в друга държава',N'R-4862',1,N'0010-004862',N'16',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(26,201,4,4,N'Издаване на удостоверение за регистрация на ВС',N'Заявление за издаване на удостоверение за регистрация  на ВС',N'R-4356',1,N'0010-004356',N'17',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(27,201,4,4,N'Издаване на свидетелство за правоспособност на ръководители на полети, на ученик-ръководители на полети, на асистент-координатори на полети и на координатори по УВД',N'Заявление за издаване на свидетелство за правоспособност на ръководители на полети, на ученик-ръководители на полети, на асистент-координатори на полети и на координатори по УВД',N'R-4284',1,N'0010-004284',N'18',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(28,201,4,4,N'Издаване на свидетелство за правоспособност на инженерно-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение',N'Заявление за издаване на свидетелство за правоспособност на инженерно-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение',N'R-4958',1,N'0010-004958',N'19',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(29,201,4,4,N'Издаване на свидетелство за преподавател към авиационни учебни центрове',N'Заявление за издаване на свидетелство за преподавател към авиационни учебни центрове',N'R-4824',1,N'0010-004824',N'20',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(30,201,4,4,N'Издаване на свидетелство на доставчик на аеронавигационно обслужване',N'Заявление за издаване на свидетелство на доставчик на аеронавигационно обслужване',N'R-4738',1,N'0010-004738',N'21',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(31,201,4,4,N'Издаване на свидетелство за експлоатационна годност на автоматизираните системи за УВД',N'Заявление за издаване на свидетелство за експлоатационна годност на автоматизираните системи за УВД',N'R-4764',1,N'0010-004764',N'22',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(32,201,4,4,N'Издаване на свидетелства за експлоатационна годност на навигационните съоръжения за въздушна навигация и кацане',N'Заявление за издаване на свидетелства за експлоатационна годност на навигационните съоръжения за въздушна навигация и кацане',N'R-4766',1,N'0010-004766',N'23',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(33,201,4,4,N'Издаване на удостоверение за експлоатационна годност на граждански летища',N'Заявление за издаване на удостоверение за експлоатационна годност на граждански летища',N'R-4588',1,N'0010-004588',N'24',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(34,201,4,4,N'Издаване на удостоверение за експлоатационна годност на летателни площадки',N'Заявление за издаване на удостоверение за експлоатационна годност на летателни площадки',N'R-4590',1,N'0010-004590',N'25',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(35,201,4,4,N'Издаване на удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване',N'Заявление за издаване на удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване',N'R-4614',1,N'0010-004614',N'26',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(36,201,4,4,N'Издаване на лиценз на летищен оператор',N'Заявление за издаване на лиценз на летищен оператор',N'R-4598',1,N'0010-004598',N'27',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(37,201,4,4,N'Издаване на лиценз на оператор по наземно обслужване или самообслужване на летище',N'Заявление за издаване на лиценз на оператор по наземно обслужване или самообслужване на летище',N'R-4606',1,N'0010-004606',N'28',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(38,201,4,4,N'Издаване на удостоверение  за одобрение на организация за управление на поддържане на постоянна летателна годност (EASA Form 14)',N'',N'',0,N'',N'29',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(39,201,4,4,N'Издаване на удостоверение за одобрение на организация за техническо обслужване (EASA Form 3F)',N'',N'',0,N'',N'30',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(40,201,4,4,N'Издаване на удостоверение за одобрение на организация за техническо обслужване (EASA Form 3)',N'',N'',0,N'',N'31',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(41,201,4,4,N'Издаване на одобрение за производствена организация',N'',N'',0,N'',N'32',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(42,201,4,4,N'Издаване на удостоверение за одобрение на организация за обучение и изпит-ване на персонал за техническо обслужване (EASA Form 11)',N'',N'',0,N'',N'33',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(43,201,4,4,N'Издаване на лиценз за техническо обслужване и ремонт на авиационна техника (за въздухоплавателни средства извън обхвата на Регламент (ЕО) № 216/2008 на Европейския парламент и Съвета от 20 февруари 2008 година относно общи правила в областта на гражданското въздухоплаване, за създаване на Европейска агенция за авиационна безопасност - включени в Приложение ІІ на Регламента)',N'',N'',0,N'',N'34',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(44,201,4,4,N'Издаване на медицинско свидетелство на авиационния персонал',N'',N'',0,N'',N'35',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(45,201,4,4,N'Издаване на удостоверение за летателна годност на ВС',N'Заявление за издаване на удостоверение за летателна годност на ВС',N'R-4470',1,N'0010-004470',N'36',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(46,201,4,4,N'Издаване на удостоверение за преглед на летателна годност (EASA Form 15a)',N'Заявление за издаване на удостоверение за преглед на летателна годност (EASA Form 15a)',N'R-4544',1,N'0010-004544',N'37',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(47,201,4,4,N'Издаване на удостоверение за съответствие с нормите за авиационен шум',N'Заявление за издаване на удостоверение за съответствие с нормите за авиационен шум',N'R-4514',1,N'0010-004514',N'38',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(48,201,4,4,N'Издаване на документ за съгласие за производство от производствена организация без одобрение',N'',N'',0,N'',N'39',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(49,201,4,4,N'Издаване и поддържане на сертификат за компетентност на лица, изпълняващи задачи по проверка и контрол за сигурност в гражданското въздухоплаване',N'Заявление за издаване и поддържане на сертификат за компетентност на лица, изпълняващи задачи по проверка и контрол за сигурност в гражданското въздухоплаване',N'R-4810',1,N'0010-004810',N'40',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(50,201,4,4,N'Издаване на Разрешително за ползване на радиостанция на въздухоплавателно средство',N'Заявление за издаване на Разрешително за ползване на радиостанция на въздухоплавателно средство',N'R-4490',1,N'0010-004490',N'41',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(51,201,4,4,N'Издаване на Удостоверение за отписване от Регистъра на ВС',N'Заявление за издаване на Удостоверение за отписване от Регистъра на ВС',N'R-4396',1,N'0010-004396',N'42',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(52,201,4,4,N'Издаване на експортно удостоверение за летателна годност',N'Заявление за издаване на експортно удостоверение за летателна годност',N'R-4566',1,N'0010-004566',N'43',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(53,201,4,4,N'Издаване на Специално удостоверение за ЛГ за любителски построено ВС',N'Заявление за издаване на Специално удостоверение за ЛГ за любителски построено ВС',N'R-4578',1,N'0010-004578',N'44',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(54,201,4,4,N'Издаване на разрешение за полет',N'Заявление за издаване на разрешение за полет по част 21 (EASA Form20)',N'R-4926',1,N'0010-004926',N'45',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(55,201,4,4,N'Издаване на Одобрение на ръководен персонал     ( EASA Form 4)  за всички одобрени организации',N'',N'',0,N'',N'46',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(56,201,4,4,N'Издаване на Одобрение на описание на организация за управление поддържането на постоянна летателна годност',N'',N'',0,N'',N'47',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(57,201,4,4,N'Издаване на Одобрение на описание на организация за техническо обслужване',N'',N'',0,N'',N'48',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(58,201,4,4,N'Издаване на Одобрение на ръководство за техническо обслужване на организация',N'',N'',0,N'',N'49',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(59,201,4,4,N'Издаване на Одобрение на описание на организация за техническо обслужване (на Организации по Наредба № 145)',N'',N'',0,N'',N'50',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(60,201,4,4,N'Издаване на Одобрение на описание на организация за обучение на персонал за техническо обслужване на ВС',N'',N'',0,N'',N'51',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(61,201,4,4,N'Издаване на Одобрение на технически борден дневник – по типове ВС',N'',N'',0,N'',N'52',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(62,201,4,4,N'Издаване на Одобрение на програма за техническо обслужване на ВС – за всяко ВС',N'',N'',0,N'',N'53',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(63,201,4,4,N'Назначаване на S-Mode код (24 битов адрес на ВС)',N'Заявление за назначаване на S-Mode код (24 битов адрес на ВС)',N'R-4378',1,N'0010-004378',N'54',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(64,201,4,4,N'Вписване и отписване в Регистъра на гражданските въздухоплавателни средства в Република България на залози и запори',N'',N'',0,N'',N'55',N'000000001',14,10,1);
INSERT INTO [DocTypes]([DocTypeId],[DocTypeGroupId],[PrimaryRegisterIndexId],[SecondaryRegisterIndexId],[Name],[ApplicationName],[Alias],[IsElectronicService],[ElectronicServiceFileTypeUri],[ElectronicServiceTypeApplication],[ElectronicServiceProvider],[ExecutionDeadline],[RemoveIrregularitiesDeadline],[IsActive])VALUES(65,201,4,4,N'Продължаване срока на валидност, изменение и анулиране на издаваните лицензи, свидетелства, удостоверения, сертификати и одобрения.',N'',N'',0,N'',N'56',N'000000001',14,10,1);
SET IDENTITY_INSERT [DocTypes] OFF
GO

